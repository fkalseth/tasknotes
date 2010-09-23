using System;
using Cqrs;
using Cqrs.Commands;
using TaskNotes.Domain.Commands;

namespace TaskNotes.Domain.CommandHandlers
{
    public class PostponeTaskCommandHandler : ICommandHandler<PostponeTaskCommand>
    {
        private readonly IRepository<Task> _taskRepository;

        public PostponeTaskCommandHandler(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void Execute(PostponeTaskCommand command)
        {
            var task = _taskRepository.GetById(command.TaskId);
            task.Postpone(command.NewDueDate);
        }
    }
}