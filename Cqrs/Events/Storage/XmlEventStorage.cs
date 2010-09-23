using System;
using System.Collections.Generic;

namespace Cqrs.Events.Storage
{
    public class XmlEventStorage : IEventStorage
    {
        private readonly IEventStorageStreams _streams;

        public XmlEventStorage(IEventStorageStreams streams)
        {
            _streams = streams;
        }

        public TAggregate Load<TAggregate>(Guid aId)
            where TAggregate : AggregateRoot
        {
            var deSerializedEvents = new List<IDomainEvent>();

            using (var stream = _streams.GetReader(aId))
            {
                string evString;

                while((evString = stream.ReadLine()) != null)
                {
                    IDomainEvent deserializedEvent = DomainEventSerializer.Deserialize(evString);
                    deSerializedEvents.Add(deserializedEvent);
                }
            }

            var aggregate = Activator.CreateInstance(typeof(TAggregate), deSerializedEvents);
            return aggregate as TAggregate;
        }
        
        public void Save(Guid aId, IEnumerable<IDomainEvent> events)
        {
            using (var stream = _streams.GetWriter(aId))
            {
                foreach (var ev in events)
                {
                    string serializedEvent = DomainEventSerializer.Serialize(ev);
                    stream.WriteLine(serializedEvent);
                }
            }
        }
    }
}
