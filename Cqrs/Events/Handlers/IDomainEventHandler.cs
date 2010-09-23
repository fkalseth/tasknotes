﻿namespace Cqrs.Events.Handlers
{
    public interface IDomainEventHandler<in TEvent>
        where TEvent : IDomainEvent
    {
        void Handle(TEvent @event);
    }
}