﻿// -----------------------------------------------------------------------
// <copyright file="RedisJournal.cs" company="Petabridge, LLC">
//      Copyright (C) 2013-2021 .NET Foundation <https://github.com/akkadotnet/akka.net>
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Persistence.Journal;
using Akka.Persistence.Redis.Query;
using Akka.Util.Internal;
using StackExchange.Redis;

namespace Akka.Persistence.Redis.Journal
{
    public class RedisJournal : AsyncWriteJournal
    {
        private readonly HashSet<IActorRef> _newEventsSubscriber = new HashSet<IActorRef>();

        private readonly RedisSettings _settings;
        private readonly JournalHelper _journalHelper;
        private readonly Lazy<IDatabase> _database;
        private readonly ActorSystem _system;
        private readonly CancellationTokenSource _pendingRequestsCancellation;

        public IDatabase Database => _database.Value;
        public bool IsClustered { get; private set; }

        /// <summary>
        /// TBD
        /// </summary>
        protected bool HasNewEventSubscribers => _newEventsSubscriber.Count != 0;

        public RedisJournal()
        {
            _settings = RedisPersistence.Get(Context.System).JournalSettings;
            _journalHelper = new JournalHelper(Context.System, _settings.KeyPrefix);
            _system = Context.System;
            _database = new Lazy<IDatabase>(() =>
            {
                var redisConnection = ConnectionMultiplexer.Connect(_settings.ConfigurationString);
                IsClustered = redisConnection.IsClustered();
                return redisConnection.GetDatabase(_settings.Database);
            });
            _pendingRequestsCancellation = new CancellationTokenSource();
        }

        protected override bool ReceivePluginInternal(object message)
        {
            switch (message)
            {
                case SubscribeNewEvents _:
                    _newEventsSubscriber.Add(Sender);
                    Context.Watch(Sender);
                    return true;
            }

            return false;
        }

        public override async Task<long> ReadHighestSequenceNrAsync(string persistenceId, long fromSequenceNr)
        {
            var highestSequenceNr =
                await Database.StringGetAsync(_journalHelper.GetHighestSequenceNrKey(persistenceId, IsClustered));
            return highestSequenceNr.IsNull ? 0L : (long) highestSequenceNr;
        }

        public override async Task ReplayMessagesAsync(
            IActorContext context,
            string persistenceId,
            long fromSequenceNr,
            long toSequenceNr,
            long max,
            Action<IPersistentRepresentation> recoveryCallback)
        {
            var journals = await Database.SortedSetRangeByScoreAsync(
                _journalHelper.GetJournalKey(persistenceId, IsClustered), 
                fromSequenceNr, 
                toSequenceNr,
                skip: 0L,
                take: max);

            foreach (var journal in journals)
                recoveryCallback(_journalHelper.PersistentFromBytes(journal));
        }

        protected override async Task DeleteMessagesToAsync(string persistenceId, long toSequenceNr)
        {
            await Database.SortedSetRemoveRangeByScoreAsync(
                _journalHelper.GetJournalKey(persistenceId, IsClustered), 
                -1, 
                toSequenceNr);
        }

        protected override async Task<IImmutableList<Exception>> WriteMessagesAsync(IEnumerable<AtomicWrite> messages)
        {
            var writeTasks = messages.Select(WriteBatchAsync).ToArray();

            var result = await Task<IImmutableList<Exception>>
                .Factory
                .ContinueWhenAll(
                    writeTasks,
                    tasks => tasks.Select(t => t.IsFaulted ? TryUnwrapException(t.Exception) : null)
                        .ToImmutableList());

            if (HasNewEventSubscribers)
                foreach (var subscriber in _newEventsSubscriber)
                    subscriber.Tell(NewEventAppended.Instance);

            return result;
        }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        private async Task WriteBatchAsync(AtomicWrite aw)
        {
            var eventList = new List<SortedSetEntry>();
            /*
            var persistenceIdPublishList = new List<(string, long)>();
            var eventIdList = new List<string>();
            var eventTagList = new List<(string, string)>();
            var tagList = new List<string>();
            */

            var payloads = aw.Payload.AsInstanceOf<IImmutableList<IPersistentRepresentation>>();
            foreach (var payload in payloads)
            {
                // var tags = payload.Payload is Tagged tag ? tag.Tags : ImmutableHashSet<string>.Empty;
                var bytes = _journalHelper.PersistentToBytes(payload.WithTimestamp(DateTime.UtcNow.Ticks));

                // save the payload
                eventList.Add(new SortedSetEntry(bytes, payload.SequenceNr));

                /*
                persistenceIdPublishList.Add((_journalHelper.GetJournalChannel(payload.PersistenceId, IsClustered),
                    payload.SequenceNr));

                var journalEventIdentifier = $"{payload.SequenceNr}:{payload.PersistenceId}";
                eventIdList.Add(journalEventIdentifier);

                foreach (var tag in tags)
                {
                    eventTagList.Add((_journalHelper.GetTagKey(tag, IsClustered), journalEventIdentifier));
                    tagList.Add(tag);
                }
                */
            }

            var transaction = Database.CreateTransaction();
            transaction.SortedSetAddAsync(_journalHelper.GetJournalKey(aw.PersistenceId, IsClustered),
                eventList.ToArray());

            // set highest sequence number key
            transaction.StringSetAsync(_journalHelper.GetHighestSequenceNrKey(aw.PersistenceId, IsClustered),
                aw.HighestSequenceNr);
            if (!await transaction.ExecuteAsync())
                throw new Exception(
                    $"{nameof(WriteMessagesAsync)}: failed to write {nameof(IPersistentRepresentation)} to redis");

            #region Query support

            /*
            //save events sequenceNr and persistenceId so that we can read all events 
            //with it starting from a given sequenceNr
            var key = _journalHelper.GetEventsKey();
            transaction = Database.CreateTransaction();
            foreach (var evt in eventIdList)
            {
                transaction.ListRightPushAsync(key, evt);
            }
            if (!await transaction.ExecuteAsync())
                throw new Exception($"{nameof(WriteMessagesAsync)}: failed to write {nameof(IPersistentRepresentation)} to redis");

            // save tags
            transaction = Database.CreateTransaction();
            foreach (var (k, e) in eventTagList)
            {
                transaction.ListRightPushAsync(k, e);
            }
            if (!await transaction.ExecuteAsync())
                throw new Exception($"{nameof(WriteMessagesAsync)}: failed to write {nameof(IPersistentRepresentation)} to redis");

            // notify about a new event being appended for this persistence id
            transaction = Database.CreateTransaction();
            foreach (var (id, nr) in persistenceIdPublishList)
            {
                transaction.PublishAsync(id, nr);
            }
            if (!await transaction.ExecuteAsync())
                throw new Exception($"{nameof(WriteMessagesAsync)}: failed to write {nameof(IPersistentRepresentation)} to redis");

            // notify about all event
            key = _journalHelper.GetEventsChannel();
            transaction = Database.CreateTransaction();
            foreach (var evt in eventIdList)
            {
                transaction.PublishAsync(key, evt);
            }
            if (!await transaction.ExecuteAsync())
                throw new Exception($"{nameof(WriteMessagesAsync)}: failed to write {nameof(IPersistentRepresentation)} to redis");

            // publish tags
            key = _journalHelper.GetTagsChannel();
            transaction = Database.CreateTransaction();
            foreach (var tag in tagList)
            {
                transaction.PublishAsync(key, tag);
            }
            if (!await transaction.ExecuteAsync())
                throw new Exception($"{nameof(WriteMessagesAsync)}: failed to write {nameof(IPersistentRepresentation)} to redis");

            // add persistenceId
            transaction = Database.CreateTransaction();
            transaction.SetAddAsync(_journalHelper.GetIdentifiersKey(), aw.PersistenceId).ContinueWith(task =>
            {
                if (task.Result)
                {
                    // notify about a new persistenceId
                    Database.Publish(_journalHelper.GetIdentifiersChannel(), aw.PersistenceId);
                }
            });
            if (!await transaction.ExecuteAsync())
                throw new Exception($"{nameof(WriteMessagesAsync)}: failed to write {nameof(IPersistentRepresentation)} to redis");
            */

            #endregion
        }
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }
}