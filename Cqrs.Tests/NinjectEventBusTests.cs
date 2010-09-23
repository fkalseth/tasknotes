using System;
using System.Linq;
using Cqrs.Events;
using Cqrs.Events.Handlers;
using Cqrs.Test;
using Ninject;
using NUnit.Framework;

namespace Cqrs.Tests
{
    [TestFixture]
    public class NinjectEventBusTests
    {
        private StandardKernel _kernel;
        private NinjectEventBus _bus;

        [SetUp]
        public void Setup()
        {
            _kernel = new StandardKernel();
            _bus = new NinjectEventBus(_kernel);
        }

        [Test]
        public void Can_publish_event_when_no_handlers_registered()
        {
            _bus.Publish(new SomeDomainEvent());
        }

        [Test]
        public void Can_publish_multiple_events_when_no_handlers_registered()
        {
            _bus.Publish(new IDomainEvent[]{new SomeDomainEvent(), new SomeDomainEvent()}.AsEnumerable());
        }

        [Test]
        public void Handler_invoked_when_publishing_event()
        {
            SomeDomainEvent handledEvent = null;

            Action<SomeDomainEvent> actionHandler = e => { handledEvent = e; };
            
            _kernel.Bind<IDomainEventHandler<SomeDomainEvent>>().ToConstant(new ActionEventHandler<SomeDomainEvent>(actionHandler));
            
            var domainEvent = new SomeDomainEvent();

            _bus.Publish(domainEvent);

            handledEvent.ShouldNotBeNull("The event handler was not invoked");
            handledEvent.ShouldBeSameAs(domainEvent, "The event passed to the event handler was not the one expected");
        }

        [Test]
        public void Multiple_handlers_invoked_when_publishing_event()
        {
            SomeDomainEvent handledEvent = null;
            SomeDomainEvent secondHandledEvent = null;

            Action<SomeDomainEvent> handler = e => { handledEvent = e; };
            Action<SomeDomainEvent> secondHandler = e => { secondHandledEvent = e; };
           
            var domainEvent = new SomeDomainEvent();

            _kernel.Bind<IDomainEventHandler<SomeDomainEvent>>().ToConstant(new ActionEventHandler<SomeDomainEvent>(handler));
            _kernel.Bind<IDomainEventHandler<SomeDomainEvent>>().ToConstant(new ActionEventHandler<SomeDomainEvent>(secondHandler));

            _bus.Publish(domainEvent);

            handledEvent.ShouldNotBeNull("The first event handler was not invoked");
            secondHandledEvent.ShouldNotBeNull("The second event handler was not invoked");

            handledEvent.ShouldBeSameAs(domainEvent, "The event passed to the first event handler was not the one expected");
            secondHandledEvent.ShouldBeSameAs(domainEvent, "The event passed to the second event handler was not the one expected");
        }

        [Test]
        public void Other_handlers_not_invoked_when_publishing_event()
        {
            SomeOtherDomainEvent handledEvent = null;

            Action<SomeOtherDomainEvent> handler = e => { handledEvent = e; };
            var domainEvent = new SomeDomainEvent();

            _kernel.Bind<IDomainEventHandler<SomeOtherDomainEvent>>().ToConstant(new ActionEventHandler<SomeOtherDomainEvent>(handler));
            
            _bus.Publish(domainEvent);

            handledEvent.ShouldBeNull("The event handler was unexpectedly invoked");
        }
    }
}