﻿using System;
using Akka.Streams.Stage;
using System.Collections.Generic;
using Akka.Persistence.Redis.Journal;
using Akka.Streams;
using Akka.Util.Internal;
using StackExchange.Redis;

namespace Akka.Persistence.Redis.Query.Stages
{
    internal class PersistenceIdsSource : GraphStage<SourceShape<string>>
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly int _database;

        public PersistenceIdsSource(ConnectionMultiplexer redis, int database)
        {
            _redis = redis;
            _database = database;
        }

        public Outlet<string> Outlet { get; } = new Outlet<string>(nameof(PersistenceIdsSource));

        public override SourceShape<string> Shape => new SourceShape<string>(Outlet);

        protected override GraphStageLogic CreateLogic(Attributes inheritedAttributes)
        {
            return new PersistenceIdsLogic(_redis, _database, Outlet, Shape);
        }

        private class PersistenceIdsLogic : GraphStageLogic
        {
            private bool _start = true;
            private long _index = 0;
            private readonly Queue<string> _buffer = new Queue<string>();
            private bool _downstreamWaiting = false;
            private ISubscriber _subscription;

            private readonly Outlet<string> _outlet;
            private readonly ConnectionMultiplexer _redis;
            private readonly int _database;

            public PersistenceIdsLogic(ConnectionMultiplexer redis, int database, Outlet<string> outlet, Shape shape) : base(shape)
            {
                _redis = redis;
                _database = database;

                _outlet = outlet;

                SetHandler(outlet, onPull: () =>
                {
                    _downstreamWaiting = true;
                    if (_buffer.Count == 0 && (_start || _index > 0))
                    {
                        var callback = GetAsyncCallback<IEnumerable<RedisValue>>(data =>
                        {
                            // save the index for further initialization if needed
                            _index = data.AsInstanceOf<IScanningCursor>().Cursor;

                            // it is not the start anymore
                            _start = false;

                            // enqueue received data
                            try
                            {
                                foreach (var item in data)
                                {
                                    _buffer.Enqueue(item);
                                }
                            }
                            catch (Exception e)
                            {
                                // TODO: log.Error(e, "Error while querying persistence identifiers")
                                FailStage(e);
                            }

                            // deliver element
                            Deliver();
                        });

                        callback(_redis.GetDatabase(_database).SetScan(RedisUtils.GetIdentifiersKey(), cursor: _index));
                    }
                    else if (_buffer.Count == 0)
                    {
                        // wait for asynchornous notification and mark dowstream
                        // as waiting for data
                    }
                    else
                    {
                        Deliver();
                    }
                });
            }

            public override void PreStart()
            {
                var callback = GetAsyncCallback<(RedisChannel channel, string bs)>(data =>
                {
                    if (data.channel.Equals(RedisUtils.GetIdentifiersChannel()))
                    {
                        // TODO: log.Debug("Message received")

                        // enqueue the element
                        _buffer.Enqueue(data.bs);

                        // deliver element
                        Deliver();
                    }
                    else
                    {
                        // TODO: if (log.IsDebugEnabled) log.Debug($"Message from unexpected channel: {channel}")
                    }
                });

                _subscription = _redis.GetSubscriber();
                _subscription.Subscribe(RedisUtils.GetIdentifiersChannel(), (channel, value) =>
                {
                    callback.Invoke((channel, value));
                });
            }

            public override void PostStop()
            {
                _subscription?.UnsubscribeAll();
            }

            private void Deliver()
            {
                if (_downstreamWaiting)
                {
                    _downstreamWaiting = false;
                    var elem = _buffer.Dequeue();
                    Push(_outlet, elem);
                }
            }
        }
    }
}