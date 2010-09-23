using System;
using Cqrs;
using Cqrs.Commands;
using TaskNotes.Domain.Commands;

namespace TaskNotes.Domain.CommandHandlers
{
    public class CancelTaskCommandHandler : ICommandHandler<CancelTaskCommand>
    {
        private readonly IRepository<Task> _taskRepository;

        public CancelTaskCommandHandler(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void Execute(CancelTaskCommand command)
        {
            var task = _taskRepository.GetById(command.TaskId);
            task.Cancel();
        }
    }
}