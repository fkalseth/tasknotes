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
    public class When_postponing_task : CommandTestFixture<Task, PostponeTaskCommand, PostponeTaskCommandHandler>
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly DateTime _due = DateTime.Now;
        private readonly DateTime _newDue = DateTime.Now.AddDays(1);
        
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return new TaskCreated(_id, "description", _due);
        }

        protected override PostponeTaskCommand When()
        {
            return new PostponeTaskCommand(Aggregate.Id, _newDue);
        }

        [Test]
        public void TaskPostponed_event_is_published()
        {
            var ev = PublishedEvents.LastOrDefault() as TaskPostponed;

            ev.ShouldNotBeNull();
            ev.TaskId.ShouldBeEqualTo(_id);
            ev.OldDueDate.ShouldBeEqualTo(_due);
            ev.NewDueDate.ShouldBeEqualTo(_newDue);
        }
    }
}
