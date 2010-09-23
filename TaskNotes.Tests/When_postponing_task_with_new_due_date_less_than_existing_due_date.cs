using System;
using System.Collections.Generic;
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
    public class When_postponing_task_with_new_due_date_less_than_existing_due_date : CommandTestFixture<Task, PostponeTaskCommand, PostponeTaskCommandHandler>
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly DateTime _due = DateTime.Now.AddDays(2);
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
        public void Exception_is_thrown()
        {
            CaughtException.ShouldNotBeNull();
            CaughtException.ShouldBeOfType<ArgumentException>();
        }
    }
}