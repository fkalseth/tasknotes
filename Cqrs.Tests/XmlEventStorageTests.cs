using System;
using System.IO;
using System.Linq;
using System.Text;
using Cqrs.Events.Storage;
using Cqrs.Test;
using NUnit.Framework;

namespace Cqrs.Tests
{
    [TestFixture]
    public class XmlEventStorageTests : IEventStorageStreams
    {
        [Test]
        public void Can_load_aggregate_from_xml()
        {
            var storage = new XmlEventStorage(this);

            var aggregate = storage.Load<SomeDomainEntityWithEvents>(Guid.Empty); // fake id

            aggregate.ShouldNotBeNull();
            aggregate.GetChanges().Count().ShouldBeEqualTo(0,  "events should be replayed, not marked as new");
            aggregate.DomainEventHandlerInvoked.ShouldBeTrue("event handler for replayed event should be invoked");
        }

        [Test]
        public void can_write_aggregate_to_xml()
        {
            var storage = new XmlEventStorage(this);

            var aggregate = new SomeDomainEntityWithEvents();
            aggregate.Apply(new SomeDomainEvent());

            storage.Save(aggregate.Id, aggregate.GetChanges());

            _writtenXml.ShouldNotBeNull();
            _writtenXml.Length.ShouldNotBeEqualTo(0);

            var ev = DomainEventSerializer.Deserialize(_writtenXml.ToString());

            ev.ShouldNotBeNull();
            ev.ShouldBeOfType<SomeDomainEvent>();
        }

        public TextReader GetReader(Guid aId)
        {
            var ev = new SomeDomainEvent();
            
            var stream = new MemoryStream();

            var writer = new StreamWriter(stream);
            
            writer.Write(DomainEventSerializer.Serialize(ev));
            writer.Flush();
            
            stream.Position = 0;

            return new StreamReader(stream);
        }

        private StringBuilder _writtenXml;

        public TextWriter GetWriter(Guid aId)
        {
            _writtenXml = new StringBuilder();
            var writer = new StringWriter(_writtenXml);
            return writer;
        }
    }
}