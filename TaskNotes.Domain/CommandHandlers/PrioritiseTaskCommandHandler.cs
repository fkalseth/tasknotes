using Cqrs;
using Cqrs.Commands;
using TaskNotes.Domain.Commands;

namespace TaskNotes.Domain.CommandHandlers
{
    public class PrioritiseTaskCommandHandler : ICommandHandler<PrioritiseTaskCommand>
    {
        private readonly IRepository<Task> _taskRepository;

        public PrioritiseTaskCommandHandler(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void Execute(PrioritiseTaskCommand command)
        {
            var task = _taskRepository.GetById(command.TaskId);
            task.Prioritise(command.NewDueDate);
        }
    }
}