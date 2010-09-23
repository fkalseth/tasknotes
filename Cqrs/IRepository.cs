using System;

namespace Cqrs
{
    public interface IRepository<out TAggregate>       
        where TAggregate : AggregateRoot
    {
        TAggregate GetById(Guid aId);
    }
}