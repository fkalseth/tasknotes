using Cqrs.Test;
using NUnit.Framework;

namespace Cqrs.Tests
{
    [TestFixture]
    public class RepositoryTests : TestableEventPublisherAndEventStore
    {
        [Test]
        public void Loads_aggregate_from_session_if_tracked()
        {
            var trackedAggregate = new SomeDomainEntity();

            var session = new Session(this, this);
            session.Track(trackedAggregate);

            var repository = new Repository<SomeDomainEntity>(this, session);

            var loadedAggregate = repository.GetById(trackedAggregate.Id);

            loadedAggregate.ShouldBeSameAs(trackedAggregate);
        }

        [Test]
        public void Loads_aggregate_From_eventstore_if_not_tracked()
        {
            var untrackedAggregate = new SomeDomainEntity();
            var session = new Session(this, this);
            _aggregateToLoad = untrackedAggregate;

            var repository = new Repository<SomeDomainEntity>(this, session);

            var loadedAggregate = repository.GetById(untrackedAggregate.Id);

            loadedAggregate.ShouldBeSameAs(_aggregateToLoad);
            session.GetAggregateIfTracked<SomeDomainEntity>(untrackedAggregate.Id).ShouldNotBeNull("Aggregate should have been added to session");
        }
    }
}