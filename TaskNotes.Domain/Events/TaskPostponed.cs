using System;
using Cqrs;
using Cqrs.Events;

namespace TaskNotes.Domain.Events
{
    public class TaskPostponed : IDomainEvent
    {
        public Guid TaskId { get; set; }
        
        public DateTime OldDueDate { get; set; }
        public DateTime NewDueDate { get; set; }

        public TaskPostponed() { }

        public TaskPostponed(Guid taskId, DateTime oldDue, DateTime newDue)
        {
            TaskId = taskId;

            OldDueDate = oldDue;
            NewDueDate = newDue;
        }
    }
}