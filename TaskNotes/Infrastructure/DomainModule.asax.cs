using Cqrs;
using Cqrs.Commands;
using Ninject.Modules;
using TaskNotes.Domain.CommandHandlers;
using TaskNotes.Domain.Commands;

namespace TaskNotes.Infrastructure
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandHandler<PostponeTaskCommand>>().To<PostponeTaskCommandHandler>();
            Bind<ICommandHandler<PrioritiseTaskCommand>>().To<PrioritiseTaskCommandHandler>();
            Bind<ICommandHandler<CancelTaskCommand>>().To<CancelTaskCommandHandler>();
            Bind<ICommandHandler<CompleteTaskCommand>>().To<CompleteTaskCommandHandler>();
            Bind<ICommandHandler<CreateTaskCommand>>().To<CreateTaskCommandHandler>();
        }
    }
}