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
    public class When_prioritising_task : CommandTestFixture<Task, PrioritiseTaskCommand, PrioritiseTaskCommandHandler>
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly DateTime _due = DateTime.Now.AddDays(2);
        private readonly DateTime _newDue = DateTime.Now.AddDays(1);

        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return new TaskCreated(_id, "description", _due);
        }

        protected override PrioritiseTaskCommand When()
        {
            return new PrioritiseTaskCommand(Aggregate.Id, _newDue);
        }

        [Test]
        public void TaskPrioritised_event_is_published()
        {
            var ev = PublishedEvents.LastOrDefault() as TaskPrioritised;

            ev.ShouldNotBeNull();
            ev.TaskId.ShouldBeEqualTo(_id);
            ev.OldDueDate.ShouldBeEqualTo(_due);
            ev.NewDueDate.ShouldBeEqualTo(_newDue);
        }
    }
}