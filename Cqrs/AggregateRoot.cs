using System;
using System.Collections.Generic;
using Cqrs.Events;
using Cqrs.Events.Handlers;

namespace Cqrs
{
    public abstract class AggregateRoot
    {
        private readonly EventHandlerCollection _events = new EventHandlerCollection();

        protected AggregateRoot()
        {
            RegisterDomainEvents(_events);
        }

        protected AggregateRoot(IEnumerable<IDomainEvent> events)
        {
            RegisterDomainEvents(_events);

            foreach (dynamic ev in events)
            {
                Apply(ev, false);
            }
        }

        protected virtual void RegisterDomainEvents(EventHandlerCollection handlerCollection)
        { }

        public Guid Id { get; protected set; }
        
        protected void Apply<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            Apply(@event, true);
        }

        private void Apply<TEvent>(TEvent @event, bool isNew) where TEvent : IDomainEvent
        {
            HandleEvent(@event);

            if(isNew)
            {
                RecordEvent(@event);
            }
        }

        private void HandleEvent<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            IDomainEventHandler<TEvent> handler = _events.GetHandlerForEvent(@event);
            if (null == handler) throw new UnknownDomainEventException("Aggregate root has no known handler for " + @event.GetType().FullName);
            handler.Handle(@event);
        }

        private void RecordEvent(IDomainEvent @event)
        {
            _appliedEvents.Add(@event);
        }

        readonly List<IDomainEvent> _appliedEvents = new List<IDomainEvent>();

        public IEnumerable<IDomainEvent> GetChanges()
        {
            return _appliedEvents.ToArray();
        }

        public void MarkChangesAsCommitted()
        {
            _appliedEvents.Clear();
        }
    }
}
