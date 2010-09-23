using System;

namespace Cqrs.Events.Handlers
{
    public class ActionEventHandler<TEvent> : IDomainEventHandler<TEvent>
        where TEvent : IDomainEvent
    {
        private readonly Action<TEvent> _eventHandlingAction;

        public ActionEventHandler(Action<TEvent> eventHandlingAction)
        {
            _eventHandlingAction = eventHandlingAction;
        }

        public void Handle(TEvent @event)
        {
            _eventHandlingAction(@event);
        }
    }
}