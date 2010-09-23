using Cqrs.Commands;
using Cqrs.Events;
using Cqrs.Events.Storage;
using Ninject.Modules;

namespace Cqrs.Configuration
{
    public class CqrsAspNetModule : NinjectModule
    {
        private readonly string _eventStoragePath;

        public CqrsAspNetModule(string eventStoragePath)
        {
            _eventStoragePath = eventStoragePath;
        }

        public override void Load()
        {
            Bind<IEventPublisher>().To<NinjectEventBus>().InSingletonScope();
            Bind<ICommandSender>().To<NinjectCommandBus>().InSingletonScope();
            
            Bind<IEventStorageStreams>()
                .To<FileEventStorageStreams>()
                .InSingletonScope()
                .WithConstructorArgument("path", _eventStoragePath);

            Bind<IEventStorage>().To<XmlEventStorage>().InSingletonScope();

            Bind<ISession>().To<Session>().InRequestScope();
            Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
        }
    }
}