using Cqrs;
using Cqrs.Events.Handlers;
using Ninject;
using Ninject.Modules;
using TaskNotes.Domain;
using TaskNotes.Domain.Events;

namespace TaskNotes.Infrastructure
{
    public class GuiModule : NinjectModule
    {
        public override void Load()
        {
            var repository = new TaskDtoRepository();
            Bind<TaskDtoRepository>().ToConstant(repository);

            Bind<IDomainEventHandler<TaskCreated>>().ToConstant(repository);
            Bind<IDomainEventHandler<TaskCancelled>>().ToConstant(repository);
            Bind<IDomainEventHandler<TaskCompleted>>().ToConstant(repository);
            Bind<IDomainEventHandler<TaskPostponed>>().ToConstant(repository);
            Bind<IDomainEventHandler<TaskPrioritised>>().ToConstant(repository);
        }
    }
}