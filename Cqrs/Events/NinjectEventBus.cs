using System;
using System.Collections.Generic;
using Cqrs.Events.Handlers;
using Ninject;

namespace Cqrs.Events
{
    public class NinjectEventBus : IEventPublisher
    {
        private readonly IKernel _kernel;

        public NinjectEventBus(IKernel kernel)
        {
            _kernel = kernel;
        }

        public virtual void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            Type handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());

            foreach (object eventHandler in _kernel.GetAll(handlerType))
            {
                handlerType.GetMethod("Handle").Invoke(eventHandler, new object[]{ @event});
            }
        }

        public void Publish(IEnumerable<IDomainEvent> events)
        {
            foreach(var @event in events)
            {
                Publish(@event);
            }
        }
    }
}