using System.Collections.Generic;

namespace Cqrs.Events
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;

        void Publish(IEnumerable<IDomainEvent> events);
    }
}
