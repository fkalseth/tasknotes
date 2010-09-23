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
    public class When_cancelling_task_that_has_already_been_completed : CommandTestFixture<Task, CancelTaskCommand, CancelTaskCommandHandler>
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly DateTime _due = DateTime.Now;
        private readonly DateTime _completionDate = DateTime.Now;

        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return new TaskCreated(_id, "description", _due);
            yield return new TaskCompleted(_id, _completionDate);
        }

        protected override CancelTaskCommand When()
        {
            return new CancelTaskCommand(_id);
        }

        [Test]
        public void Exception_is_thrown()
        {
            CaughtException.ShouldNotBeNull();
            CaughtException.ShouldBeOfType<InvalidOperationException>();
        }
    }
}