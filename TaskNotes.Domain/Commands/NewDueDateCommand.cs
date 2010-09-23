using System;
using Cqrs;
using Cqrs.Commands;

namespace TaskNotes.Domain.Commands
{
    public abstract class NewDueDateCommand : ICommand
    {
        public Guid TaskId { get; set; }
        public DateTime NewDueDate { get; set; }

        public NewDueDateCommand(Guid taskId, DateTime newDueDate)
        {
            TaskId = taskId;
            NewDueDate = newDueDate;
        }
    }
}