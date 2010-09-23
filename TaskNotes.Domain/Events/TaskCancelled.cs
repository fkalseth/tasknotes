using System;
using Cqrs;
using Cqrs.Events;

namespace TaskNotes.Domain.Events
{
    public class TaskCancelled : IDomainEvent
    {
        public Guid TaskId { get; set; }

        public TaskCancelled() { }

        public TaskCancelled(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
