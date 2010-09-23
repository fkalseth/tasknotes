using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Cqrs.Events.Storage
{
    public static class DomainEventSerializer
    {
        public static IDomainEvent Deserialize(string evString)
        {
            var typeAndEvent = evString.Split(new[] { '|' }, 2);

            var serializer = new XmlSerializer(Type.GetType(typeAndEvent[0]));

            using (var reader = new StringReader(typeAndEvent[1]))
            {
                var ev = serializer.Deserialize(reader);
                return ev as IDomainEvent;
            }
        }

        public static string Serialize(IDomainEvent ev)
        {
            var serializer = new XmlSerializer(ev.GetType());

            var serializedEvent = new StringBuilder();

            using (var writer = new StringWriter(serializedEvent))
            {
                serializer.Serialize(writer, ev);
            }

            return ev.GetType().AssemblyQualifiedName + "|" +
                   serializedEvent.ToString().Replace(Environment.NewLine, "");
        }
    }
}