using System;
using System.IO;

namespace Granta.MaterialsWall.DataAccess
{
    public interface IDataFilePathProvider
    {
        string GetPath();
    }

    public sealed class DataFilePathProvider : IDataFilePathProvider
    {
        private const string DataFileAppSettingName = "DataFile";

        private readonly IAppSettingsProvider appSettingsProvider;

        public DataFilePathProvider(IAppSettingsProvider appSettingsProvider)
        {
            if (appSettingsProvider == null)
            {
                throw new ArgumentNullException("appSettingsProvider");
            }
            
            this.appSettingsProvider = appSettingsProvider;
        }

        public string GetPath()
        {
            string dataFilePath = appSettingsProvider.GetSetting(DataFileAppSettingName);
            string dataFileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataFilePath);
            return dataFileFullPath;
        }
    }
}