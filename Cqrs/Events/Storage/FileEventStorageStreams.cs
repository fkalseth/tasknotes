using System;
using System.IO;

namespace Cqrs.Events.Storage
{
    public class FileEventStorageStreams : IEventStorageStreams
    {
        private readonly string _path;

        public FileEventStorageStreams(string path)
        {
            _path = path;
        }

        public TextReader GetReader(Guid aId)
        {
            string path = Path.Combine(_path, aId + ".events");

            return new StreamReader(File.OpenRead(path));
        }

        public TextWriter GetWriter(Guid aId)
        {
            string path = Path.Combine(_path, aId + ".events");
            
            if (File.Exists(path)) return new StreamWriter(File.Open(path, FileMode.Append));
            return new StreamWriter(File.Create(path));
        }
    }
}