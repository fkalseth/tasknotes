using System;
using System.Collections.Generic;
using System.Linq;

namespace Cqrs.Events.Storage
{
    public class InMemoryEventStorage : IEventStorage
    {
        readonly Dictionary<Guid, List<IDomainEvent>> _events = new Dictionary<Guid, List<IDomainEvent>>();

        public void Save(Guid aId, IEnumerable<IDomainEvent> events)
        {
            List<IDomainEvent> eventStoreForAggregate = GetListOfEvents(aId);

            if (null == eventStoreForAggregate)
            {
                eventStoreForAggregate = new List<IDomainEvent>();
                _events.Add(aId, eventStoreForAggregate);
            }

            eventStoreForAggregate.AddRange(events);
        }

        private List<IDomainEvent> GetListOfEvents(Guid aId)
        {
            List<IDomainEvent> eventStoreForAggregate;
            _events.TryGetValue(aId, out eventStoreForAggregate);

            return eventStoreForAggregate;
        }

        public IEnumerable<IDomainEvent> GetEvents(Guid aId)
        {
            return GetListOfEvents(aId) ?? new List<IDomainEvent>();
        }

        public TAggregate Load<TAggregate>(Guid aId)
            where TAggregate : AggregateRoot
        {
            var events = GetEvents(aId);

            if (events.Count() == 0) return null;

            object aggregate = Activator.CreateInstance(typeof(TAggregate), events);
            return aggregate as TAggregate;
        }
    }    
}
