using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cqrs
{
    public interface ISession
    {
        TAggregate GetAggregateIfTracked<TAggregate>(Guid aId)
            where TAggregate : AggregateRoot;

        void Track(AggregateRoot aggregate);

        void CommitChanges();
    }    
}
