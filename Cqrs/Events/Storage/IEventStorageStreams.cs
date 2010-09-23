using System;
using System.IO;

namespace Cqrs.Events.Storage
{
    public interface IEventStorageStreams
    {
        TextReader GetReader(Guid aId);
        TextWriter GetWriter(Guid aId);
    }
}