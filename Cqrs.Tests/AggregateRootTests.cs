using System.Linq;
using Cqrs.Events;
using Cqrs.Test;
using NUnit.Framework;

namespace Cqrs.Tests
{
    [TestFixture]
    public class AggregateRootTests
    {
        [Test]
        public void Can_create_domain_entity()
        {
            new SomeDomainEntity();
        }

        [Test, ExpectedException(typeof(UnknownDomainEventException))]
        public void Throws_exception_when_applying_unknown_event_on_aggregate_root()
        {
            var entity = new SomeDomainEntity();
            var domainEvent = new SomeDomainEvent();
           
            entity.Apply(domainEvent);
        }

        [Test]
        public void Can_apply_domain_events_to_entity()
        {
            var entity = new SomeDomainEntityWithEvents();
            var domainEvent = new SomeDomainEvent();
            
            entity.Apply(domainEvent);

            var changes = entity.GetChanges();

            changes.ShouldNotBeNull();
            changes.Count().ShouldBeEqualTo(1, "Only one event should have been applied to aggregate");
            changes.First().ShouldBeSameAs(domainEvent, "The event applied to aggregate was not the one expected");
        }

        [Test]
        public void Event_handler_invoked_when_applying_domain_event_to_aggregate_root()
        {

            var entity = new SomeDomainEntityWithEvents();
            var domainEvent = new SomeDomainEvent();

            entity.Apply(domainEvent);

            entity.DomainEventHandlerInvoked.ShouldBeTrue("the eventhandler for the event was not invoked");
        }
    }
}
