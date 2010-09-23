using System.Collections.Generic;
using Cqrs.Events;

namespace Cqrs.Tests
{
    public class SomeDomainEntityWithEvents : AggregateRoot
    {
        public SomeDomainEntityWithEvents(){}

        public SomeDomainEntityWithEvents(IEnumerable<IDomainEvent> events)
            : base(events)
        {}

        public void Apply<TEvent>(TEvent ev) where TEvent : IDomainEvent
        {
            base.Apply(ev);
        }

        protected override void RegisterDomainEvents(EventHandlerCollection handlerCollection)
        {
            handlerCollection.Register<SomeDomainEvent>(OnSomeDomainEvent);
        }

        public bool DomainEventHandlerInvoked { get; private set; }

        private void OnSomeDomainEvent(SomeDomainEvent ev)
        {
            DomainEventHandlerInvoked = true;
        }
    }
}