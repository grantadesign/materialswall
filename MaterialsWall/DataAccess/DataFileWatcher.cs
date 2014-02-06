using System;
using System.IO;
using Ninject.Extensions.Logging;

namespace Granta.MaterialsWall.DataAccess
{
    public interface IDataFileWatcher
    {
        bool FileHasChanged{get;}
        void FileReloaded();
    }

    public sealed class DataFileWatcher : IDataFileWatcher
    {
        private readonly ILogger logger;
        private readonly FileSystemWatcher fileSystemWatcher;
        
        public bool FileHasChanged{get {return isDirty;}}
        private bool isDirty = true;

        public DataFileWatcher(ILogger logger, IDataFilePathProvider dataFilePathProvider, IFileSystemWatcherFactory fileSystemWatcherFactory)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            
            if (dataFilePathProvider == null)
            {
                throw new ArgumentNullException("dataFilePathProvider");
            }

            if (fileSystemWatcherFactory == null)
            {
                throw new ArgumentNullException("fileSystemWatcherFactory");
            }
            
            this.logger = logger;

            string dataFilePath = dataFilePathProvider.GetPath();
            fileSystemWatcher = fileSystemWatcherFactory.Create(dataFilePath, () => OnFileChanged(dataFilePath));
        }

        private void OnFileChanged(string dataFilePath)
        {
            logger.Debug("Data file ({0}) change event triggered", dataFilePath);
            isDirty = true;
        }

        public void FileReloaded()
        {
            isDirty = false;
        }
    }
}
