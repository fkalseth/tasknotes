using System;

namespace TaskNotes.Domain.Commands
{
    public class PostponeTaskCommand : NewDueDateCommand
    {
        public PostponeTaskCommand(Guid taskId, DateTime newDueDate) 
            : base(taskId, newDueDate)
        {}
    }

}