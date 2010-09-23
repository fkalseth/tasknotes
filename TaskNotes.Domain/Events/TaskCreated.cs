using System;
using Cqrs;
using Cqrs.Events;

namespace TaskNotes.Domain.Events
{
    public class TaskCreated : IDomainEvent
    {
        public Guid TaskId { get; set; }
        public string Description { get; set; }
        public DateTime Due { get; set; }

        public TaskCreated() { }

        public TaskCreated(Guid taskId, string description, DateTime due)
        {
            TaskId = taskId;
            Description = description;
            Due = due;
        }
    }
}