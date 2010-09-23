using System.Linq;
using Cqrs.Test;
using NUnit.Framework;

namespace Cqrs.Tests
{
    public class SessionTests : TestableEventPublisherAndEventStore
    {
        [Test]
        public void Can_track_aggregate()
        {
            var session = new Session(this, this);
            var aggregate = new SomeDomainEntity();

            session.Track(aggregate);
        }

        [Test]
        public void Can_get_tracked_aggregate()
        {
            var session = new Session(this, this);
            var aggregate = new SomeDomainEntity();

            session.Track(aggregate);

            var loadedAggregate = session.GetAggregateIfTracked<SomeDomainEntity>(aggregate.Id);

            loadedAggregate.ShouldNotBeNull();
            loadedAggregate.ShouldBeSameAs(aggregate);
        }

        [Test]
        public void Can_commit_changes()
        {
            var session = new Session(this, this);
            var aggregate = new SomeDomainEntityWithEvents();

            session.Track(aggregate);

            var domainEvent = new SomeDomainEvent();
            aggregate.Apply(domainEvent);

            session.CommitChanges();

            _savedId.ShouldBeEqualTo(aggregate.Id);
            _savedEvents.Count().ShouldBeEqualTo(1);
            _savedEvents.First().ShouldBeSameAs(domainEvent);
        }

        [Test]
        public void Events_are_published_to_bus_on_commit()
        {
            var session = new Session(this, this);
            var aggregate = new SomeDomainEntityWithEvents();

            session.Track(aggregate);

            var domainEvent = new SomeDomainEvent();
            aggregate.Apply(domainEvent);

            session.CommitChanges();

            PublishedEvents.Count().ShouldBeEqualTo(1);
            PublishedEvents.Pop().ShouldBeSameAs(domainEvent);
        }
    }
}