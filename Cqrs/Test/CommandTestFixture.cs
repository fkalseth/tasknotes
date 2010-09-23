using System;
using System.Collections.Generic;
using Cqrs.Commands;
using Cqrs.Events;
using Cqrs.Events.Storage;
using Ninject;
using NUnit.Framework;

namespace Cqrs.Test
{
    [TestFixture]
    public abstract class CommandTestFixture<TAggregate, TCommand, TCommandHandler> : IEventPublisher 
        where TAggregate : AggregateRoot
        where TCommand : ICommand
        where TCommandHandler : ICommandHandler<TCommand>
    {
        public TAggregate Aggregate { get; private set; }

        public IKernel Kernel { get; private set; }
        
        public IEventPublisher EventPublisher { get { return Kernel.Get<IEventPublisher>(); } }

        public ISession Session { get { return Kernel.Get<ISession>(); } }

        public Exception CaughtException { get; private set; }

        [SetUp]
        public void Setup()
        {
            BuildKernel();
            BuildAggregate();

            var handler = BuildHandler();
            var command = When();

            ExecuteHandler(handler, command);
        }

        private void ExecuteHandler(TCommandHandler handler, TCommand command)
        {
            try
            {
                handler.Execute(command);
                Session.CommitChanges();
            }
            catch(Exception ex)
            {
                CaughtException = ex;
            }
        }

        private void BuildAggregate()
        {
            Aggregate = (TAggregate)Activator.CreateInstance(typeof (TAggregate), Given());
            Session.Track(Aggregate);
        }

        private void BuildKernel()
        {
            Kernel = new StandardKernel();

            Kernel.Bind<IEventPublisher>().ToConstant(this);
            Kernel.Bind<ISession>().To<Session>().InSingletonScope();
            Kernel.Bind<IEventStorage>().To<InMemoryEventStorage>().InSingletonScope();
            Kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InSingletonScope();

            //  todo: automocking?
        }

        private TCommandHandler BuildHandler()
        {
            return Kernel.Get<TCommandHandler>();
        }

        protected abstract IEnumerable<IDomainEvent> Given();
        
        protected abstract TCommand When();
        
        public readonly Stack<IDomainEvent> PublishedEvents = new Stack<IDomainEvent>();

        public virtual void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            PublishedEvents.Push(@event);
        }

        public void Publish(IEnumerable<IDomainEvent> events)
        {
            foreach(var ev in events) Publish(ev as dynamic);
        }
    } 
}
