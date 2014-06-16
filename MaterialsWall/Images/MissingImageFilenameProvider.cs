using System;

namespace Granta.MaterialsWall.Images
{
    public interface IMissingImageFilenameProvider
    {
        string GetMissingImageName();
    }

    public sealed class MissingImageFilenameProvider : IMissingImageFilenameProvider
    {
        private readonly IAppSettingsProvider appSettingsProvider;

        public MissingImageFilenameProvider(IAppSettingsProvider appSettingsProvider)
        {
            if (appSettingsProvider == null)
            {
                throw new ArgumentNullException("appSettingsProvider");
            }
            
            this.appSettingsProvider = appSettingsProvider;
        }

        public string GetMissingImageName()
        {
            return appSettingsProvider.GetSetting("Images:MissingImageFilename");
        }
    }
}
