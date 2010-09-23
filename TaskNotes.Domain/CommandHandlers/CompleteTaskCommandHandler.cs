using Cqrs;
using Cqrs.Commands;
using TaskNotes.Domain.Commands;

namespace TaskNotes.Domain.CommandHandlers
{
    public class CompleteTaskCommandHandler : ICommandHandler<CompleteTaskCommand>
    {
        private readonly IRepository<Task> _taskRepository;

        public CompleteTaskCommandHandler(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void Execute(CompleteTaskCommand command)
        {
            var task = _taskRepository.GetById(command.TaskId);
            task.Complete();
        }
    }
}