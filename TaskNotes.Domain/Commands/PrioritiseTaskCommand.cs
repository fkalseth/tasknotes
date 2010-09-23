using System;

namespace TaskNotes.Domain.Commands
{
    public class PrioritiseTaskCommand :NewDueDateCommand
    {
        public PrioritiseTaskCommand(Guid taskId, DateTime newDueDate) 
            : base(taskId, newDueDate)
        {}
    }
}