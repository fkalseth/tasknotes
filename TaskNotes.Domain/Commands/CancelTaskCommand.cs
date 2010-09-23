using System;
using Cqrs;
using Cqrs.Commands;

namespace TaskNotes.Domain.Commands
{
    public class CancelTaskCommand : ICommand
    {
        public Guid TaskId { get; set; }

        public CancelTaskCommand(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}