using System;
using Cqrs.Commands;
using Cqrs.Test;
using Ninject;
using NUnit.Framework;

namespace Cqrs.Tests
{
    [TestFixture]
    public class CommandBusTests
    {
        private StandardKernel _kernel;
        private NinjectCommandBus _bus;

        [SetUp]
        public void Setup()
        {
            _kernel = new StandardKernel();
            _bus = new NinjectCommandBus(_kernel);
        }

        [Test]
        public void Sending_command_when_no_handler_throws()
        {
            var command = new SomeCommand();

            AssertionHelpers.ShouldThrow<InvalidOperationException>(() => _bus.Send(command));
        }

        [Test]
        public void Sending_command_when_multiple_handlers_throws()
        {
            var command = new SomeCommand();

            _kernel.Bind<ICommandHandler<SomeCommand>>().ToConstant(new ActionCommandHandler<SomeCommand>(c => { }));
            _kernel.Bind<ICommandHandler<SomeCommand>>().ToConstant(new ActionCommandHandler<SomeCommand>(c => { }));

            AssertionHelpers.ShouldThrow<InvalidOperationException>(() => _bus.Send(command));
        }

        [Test]
        public void Sending_command_executes_handler()
        {
            ICommand handledCommand = null;

            Action<SomeCommand> handler = c => handledCommand = c;
            _kernel.Bind<ICommandHandler<SomeCommand>>().ToConstant(new ActionCommandHandler<SomeCommand>(handler));

            var command = new SomeCommand();

            _bus.Send(command);

            handledCommand.ShouldNotBeNull();
            handledCommand.ShouldBeSameAs(command);
        }
    }
}