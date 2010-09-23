using System;
using Cqrs.Events.Storage;

namespace Cqrs
{
    public class Repository<TAggregate> : IRepository<TAggregate> where TAggregate : AggregateRoot
    {
        private readonly IEventStorage _eventStore;
        private readonly ISession _session;
        
        public Repository(IEventStorage eventStore, ISession session)
        {
            _eventStore = eventStore;
            _session = session;
        }

        public TAggregate GetById(Guid aId)
        {
            var aggregate = _session.GetAggregateIfTracked<TAggregate>(aId);
            
            if(FirstTimeAggregateIsLoaded(aggregate))
            {
                aggregate = LoadAggregateFromEventStore(aId);
            }

            return aggregate;
        }

        private TAggregate LoadAggregateFromEventStore(Guid aId)
        {
            var aggregate = _eventStore.Load<TAggregate>(aId);   
            _session.Track(aggregate);
            return aggregate;
        }

        private bool FirstTimeAggregateIsLoaded(TAggregate aggregate)
        {
            return null == aggregate;
        }
    }
}