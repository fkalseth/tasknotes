using System;
using Cqrs;
using Cqrs.Commands;

namespace TaskNotes.Domain.Commands
{
    public class CompleteTaskCommand : ICommand
    {
        public Guid TaskId { get; set; }

        public CompleteTaskCommand(Guid taskId)
        {
            TaskId = taskId;
        }
    }

    public class CreateTaskCommand : ICommand
    {
        public string Description { get; set; }

        public DateTime Due { get; set; }
        public Guid TaskId { get; set; }

        public CreateTaskCommand(Guid taskId, string description, DateTime due)
        {
            TaskId = taskId;
            Description = description;
            Due = due;
        }
    }
}