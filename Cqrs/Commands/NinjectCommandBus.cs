using System;
using System.Linq;
using Ninject;

namespace Cqrs.Commands
{
    public class NinjectCommandBus : ICommandSender
    {
        private readonly IKernel _kernel;

        public NinjectCommandBus(IKernel kernel)
        {
            _kernel = kernel;
        }

        public virtual void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlerType = typeof (ICommandHandler<>).MakeGenericType(command.GetType());

            var handlers = _kernel.GetAll(handlerType);

            if(handlers.Count() == 0) throw new InvalidOperationException("No handler exists for the command " + typeof(TCommand).Name);
            if(handlers.Count() > 1) throw new InvalidOperationException("Ambigious handler for the command " + typeof(TCommand).Name + " - more than one handler exists for this command.");

            (handlers.First() as dynamic).Execute(command as dynamic);
        }
    }
}