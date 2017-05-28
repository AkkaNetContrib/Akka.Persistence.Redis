﻿using Akka.Actor;
using Akka.Configuration;
using Akka.Persistence.Query;
using Akka.Streams;
using Akka.Streams.TestKit;
using System;
using FluentAssertions;
using Xunit;

namespace Akka.Persistence.TestKit.Query
{
    public abstract class CurrentEventsByTagSource : Akka.TestKit.Xunit2.TestKit
    {
        protected ActorMaterializer Materializer { get; }

        protected IReadJournal ReadJournal { get; set; }

        protected CurrentEventsByTagSource(Config config) : base(config)
        {
            Materializer = Sys.Materializer();
        }

        [Fact]
        public void ReadJournal_should_implement_ICurrentEventsByTagQuery()
        {
            ReadJournal.Should().BeAssignableTo<ICurrentEventsByTagQuery>();
        }

        [Fact]
        public void ReadJournal_query_EventsByTag_should_find_existing_events()
        {
            var queries = ReadJournal as ICurrentEventsByTagQuery;
            var a = Sys.ActorOf(Query.TestActor.Props("a"));
            var b = Sys.ActorOf(Query.TestActor.Props("b"));

            a.Tell("hello");
            ExpectMsg("hello-done");
            a.Tell("a green apple");
            ExpectMsg("a green apple-done");
            b.Tell("a black car");
            ExpectMsg("a black car-done");
            a.Tell("a green banana");
            ExpectMsg("a green banana-done");
            b.Tell("a green leaf");
            ExpectMsg("a green leaf-done");

            var greenSrc = queries.CurrentEventsByTag("green", offset: 0L);
            var probe = greenSrc.RunWith(this.SinkProbe<EventEnvelope>(), Materializer);
            probe.Request(2)
                .ExpectNext(new EventEnvelope(0L, "a", 2L, "a green apple"))
                .ExpectNext(new EventEnvelope(1L, "a", 3L, "a green banana"));
            probe.ExpectNoMsg(TimeSpan.FromMilliseconds(500));
            probe.Request(2)
                .ExpectNext(new EventEnvelope(2L, "b", 2L, "a green leaf"))
                .ExpectComplete();

            var blackSrc = queries.CurrentEventsByTag("black", offset: 0L);
            probe = blackSrc.RunWith(this.SinkProbe<EventEnvelope>(), Materializer);
            probe.Request(5)
                .ExpectNext(new EventEnvelope(0L, "b", 1L, "a black car"))
                .ExpectComplete();
        }

        [Fact]
        public void ReadJournal_query_EventsByTag_should_not_see_new_events_after_complete()
        {
            var queries = ReadJournal as ICurrentEventsByTagQuery;
            ReadJournal_query_EventsByTag_should_find_existing_events();

            var c = Sys.ActorOf(Query.TestActor.Props("c"));

            var greenSrc = queries.CurrentEventsByTag("green", offset: 0L);
            var probe = greenSrc.RunWith(this.SinkProbe<EventEnvelope>(), Materializer);
            probe.Request(2)
                .ExpectNext(new EventEnvelope(0L, "a", 2L, "a green apple"))
                .ExpectNext(new EventEnvelope(1L, "a", 3L, "a green banana"))
                .ExpectNoMsg(TimeSpan.FromMilliseconds(100));

            probe.ExpectNoMsg(TimeSpan.FromMilliseconds(100));
            probe.Request(5)
                .ExpectNext(new EventEnvelope(2L, "b", 2L, "a green leaf"))
                .ExpectComplete(); // green cucumber not seen

            c.Tell("a green cucumber");
            ExpectMsg("a green cucumber-done");
        }

        [Fact]
        public void ReadJournal_query_EventsByTag_should_find_events_from_offset_inclusive()
        {
            var queries = ReadJournal as ICurrentEventsByTagQuery;
            ReadJournal_query_EventsByTag_should_not_see_new_events_after_complete();

            var greenSrc = queries.CurrentEventsByTag("green", offset: 2L);
            var probe = greenSrc.RunWith(this.SinkProbe<EventEnvelope>(), Materializer);
            probe.Request(10)
                .ExpectNext(new EventEnvelope(2L, "b", 2L, "a green leaf"))
                .ExpectNext(new EventEnvelope(3L, "c", 1L, "a green cucumber"))
                .ExpectComplete();
        }
    }
}
