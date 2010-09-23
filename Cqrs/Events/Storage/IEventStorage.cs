using System;
using System.Collections.Generic;

namespace Cqrs.Events.Storage
{
    public interface IEventStorage
    {
        TAggregate Load<TAggregate>(Guid aId) where TAggregate : AggregateRoot;
        void Save(Guid aId, IEnumerable<IDomainEvent> events);
    }
}