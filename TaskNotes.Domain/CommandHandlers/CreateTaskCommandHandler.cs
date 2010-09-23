using System;
using Cqrs;
using Cqrs.Commands;
using TaskNotes.Domain.Commands;

namespace TaskNotes.Domain.CommandHandlers
{
    public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand>
    {
        private readonly ISession _session;

        public CreateTaskCommandHandler(ISession session)
        {
            _session = session;
        }

        public void Execute(CreateTaskCommand command)
        {
            var task = new Task(command.TaskId, command.Description, command.Due);
            _session.Track(task);
        }
    }
}