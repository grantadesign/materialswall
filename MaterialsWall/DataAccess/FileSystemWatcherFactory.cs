using System.IO;
using Antlr.Runtime.Misc;

namespace Granta.MaterialsWall.DataAccess
{
    public interface IFileSystemWatcherFactory
    {
        FileSystemWatcher Create(string fileToWatch, Action fileChanged);
    }

    public sealed class FileSystemWatcherFactory : IFileSystemWatcherFactory
    {
        public FileSystemWatcher Create(string fileToWatch, Action fileChanged)
        {
            string dataFileName = Path.GetFileName(fileToWatch);
            string dataFileDirectory = Path.GetDirectoryName(fileToWatch);
            var watcher = new FileSystemWatcher(dataFileDirectory)
                      {
                            NotifyFilter = NotifyFilters.Attributes | NotifyFilters.LastWrite 
                                            | NotifyFilters.Security | NotifyFilters.Size,
                            Filter = Path.GetFileName(dataFileName)
                      };
            watcher.Changed += (sender, e) => fileChanged();
            watcher.EnableRaisingEvents = true;
            return watcher;
        }
    }
}
