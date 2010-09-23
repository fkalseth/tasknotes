using Cqrs.Events;

namespace Cqrs.Tests
{
    public class SomeDomainEntity : AggregateRoot
    {
        public void Apply(IDomainEvent ev)
        {
            base.Apply(ev);
        }
    }
}