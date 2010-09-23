using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs;
using Cqrs.Events;
using Cqrs.Test;
using NUnit.Framework;
using TaskNotes.Domain;
using TaskNotes.Domain.CommandHandlers;
using TaskNotes.Domain.Commands;
using TaskNotes.Domain.Events;

namespace TaskNotes.Tests
{
    public class When_completing_task : CommandTestFixture<Task, CompleteTaskCommand, CompleteTaskCommandHandler>
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly DateTime _due = DateTime.Now;

        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return new TaskCreated(_id, "description", _due);
        }

        protected override CompleteTaskCommand When()
        {
            return new CompleteTaskCommand(_id);
        }

        [Test]
        public void TaskCancelled_event_is_published()
        {
            var ev = PublishedEvents.LastOrDefault() as TaskCompleted;

            ev.ShouldNotBeNull();
            ev.TaskId.ShouldBeEqualTo(_id);
        }
    }
}