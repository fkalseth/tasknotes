using System;
using System.Collections.Generic;
using Cqrs.Events;
using Cqrs.Events.Storage;
using NUnit.Framework;

namespace Cqrs.Tests
{
    [TestFixture]
    public abstract class TestableEventPublisherAndEventStore : IEventPublisher, IEventStorage
    {
        [SetUp]
        public void Setup()
        {
            PublishedEvents.Clear();
        }

        IEventPublisher EventPublisher { get { return this; } }

        public Stack<IDomainEvent> PublishedEvents = new Stack<IDomainEvent>();

        public void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            PublishedEvents.Push(@event);
            OnDomainEventPublished(@event);
        }

        protected virtual void OnDomainEventPublished<TEvent>(TEvent @event)
        {}

        public void Publish(IEnumerable<IDomainEvent> events)
        {
            foreach(var ev in events) Publish(ev as dynamic);
        }


        protected IEnumerable<IDomainEvent> _savedEvents;
        protected Guid _savedId;
        protected object _aggregateToLoad;

        public TAggregate Load<TAggregate>(Guid aId) where TAggregate : AggregateRoot
        {
            return _aggregateToLoad as TAggregate;
        }

        public void Save(Guid aId, IEnumerable<IDomainEvent> events)
        {
            _savedId = aId;
            _savedEvents = events;
        }
    }
}