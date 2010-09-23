using System;
using Cqrs.Commands;

namespace Cqrs.Tests
{
    public class ActionCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly Action<TCommand> _action;

        public ActionCommandHandler(Action<TCommand> action)
        {
            _action = action;
        }

        public void Execute(TCommand command)
        {
            _action(command);
        }
    }
}