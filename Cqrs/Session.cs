using System;
using System.Collections.Generic;
using Cqrs.Events;
using Cqrs.Events.Storage;

namespace Cqrs
{
    public class Session : ISession
    {
        readonly IEventPublisher _eventPublisher;
        readonly Dictionary<Guid, AggregateRoot> _trackedAggregates = new Dictionary<Guid, AggregateRoot>();
        private readonly IEventStorage _eventStorage;

        public Session(IEventPublisher eventPublisher, IEventStorage eventStorage)
        {
            _eventPublisher = eventPublisher;
            _eventStorage = eventStorage;
        }

        public TAggregate GetAggregateIfTracked<TAggregate>(Guid aId)
            where TAggregate : AggregateRoot
        {
            AggregateRoot aggregate;
            _trackedAggregates.TryGetValue(aId, out aggregate);
            return (TAggregate)aggregate;
        }

        public void Track(AggregateRoot aggregate)
        {
            _trackedAggregates.Add(aggregate.Id, aggregate);
        }

        public void CommitChanges()
        {
            foreach (var aggregate in _trackedAggregates.Values)
            {
                var newEvents = aggregate.GetChanges();
                _eventStorage.Save(aggregate.Id, newEvents);
                aggregate.MarkChangesAsCommitted();
                _eventPublisher.Publish(newEvents);
            }
        }
    }
}