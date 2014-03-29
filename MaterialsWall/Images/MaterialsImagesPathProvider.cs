using System;

namespace Granta.MaterialsWall.Images
{
    public interface IMaterialsImagesPathProvider
    {
        string GetMaterialsImagesPath();
    }

    public sealed class MaterialsImagesPathProvider : IMaterialsImagesPathProvider
    {
        private readonly IAppSettingsProvider appSettingsProvider;

        public MaterialsImagesPathProvider(IAppSettingsProvider appSettingsProvider)
        {
            if (appSettingsProvider == null)
            {
                throw new ArgumentNullException("appSettingsProvider");
            }
            
            this.appSettingsProvider = appSettingsProvider;
        }

        public string GetMaterialsImagesPath()
        {
            return appSettingsProvider.GetSetting("Images:MaterialsImagesPath");
        }
    }
}
