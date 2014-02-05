using System;
using System.IO;

namespace Granta.MaterialsWall.DataAccess
{
    public interface IDataFileWatcher
    {
        bool DataFileHasChanged{get;}
    }

    public sealed class DataFileWatcher : IDataFileWatcher
    {
        private readonly FileSystemWatcher fileSystemWatcher;
        
        public bool DataFileHasChanged{get {return isDirty;}}
        private bool isDirty = true;

        public DataFileWatcher(IAppSettingsProvider appSettingsProvider, IFileSystemWatcherFactory fileSystemWatcherFactory)
        {
            if (appSettingsProvider == null)
            {
                throw new ArgumentNullException("appSettingsProvider");
            }

            if (fileSystemWatcherFactory == null)
            {
                throw new ArgumentNullException("fileSystemWatcherFactory");
            }
            
            string dataFilePath = appSettingsProvider.GetSetting("DataFile");
            string dataFileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataFilePath);
            fileSystemWatcher = fileSystemWatcherFactory.Create(dataFileFullPath, () => isDirty = true);
        }
    }
}
