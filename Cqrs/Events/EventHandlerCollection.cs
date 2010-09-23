using System;
using System.Collections.Generic;
using Cqrs.Events.Handlers;

namespace Cqrs.Events
{
    public class EventHandlerCollection
    {
        readonly Dictionary<Type, object> handlers = new Dictionary<Type, object>();

        public void Register<TEvent>(Action<TEvent> handler)
            where TEvent : IDomainEvent
        {
            Register(new ActionEventHandler<TEvent>(handler));
        }

        public void Register<TEvent>(IDomainEventHandler<TEvent> handler) where TEvent : IDomainEvent
        {
            if(handlers.ContainsKey(typeof(TEvent))) throw new InvalidOperationException("Cannot register multiple handlers for same event: " + typeof(TEvent).Name);

            handlers.Add(typeof (TEvent), handler);
        }

        public IDomainEventHandler<TEvent> GetHandlerForEvent<TEvent>(TEvent @event)
            where TEvent : IDomainEvent
        {
            object handler;

            handlers.TryGetValue(typeof (TEvent), out handler);

            return handler as IDomainEventHandler<TEvent>;
        }
    }
}