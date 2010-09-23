using System;
using Cqrs;
using Cqrs.Events;

namespace TaskNotes.Domain.Events
{
    public class TaskCompleted : IDomainEvent
    {
        public Guid TaskId { get; set; }

        public DateTime CompletionDate { get; set; }

        public TaskCompleted() { }

        public TaskCompleted(Guid taskId, DateTime completionDate)
        {
            TaskId = taskId;
            CompletionDate = completionDate;
        }
    }
}
