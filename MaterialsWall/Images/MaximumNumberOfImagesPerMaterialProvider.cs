using System;

namespace Granta.MaterialsWall.Images
{
    public interface IMaximumNumberOfImagesPerMaterialProvider
    {
        int GetMaximumNumberOfImagesPerMaterial();
    }

    public sealed class MaximumNumberOfImagesPerMaterialProvider : IMaximumNumberOfImagesPerMaterialProvider
    {
        private const int DefaultMaximumNumberOfImages = 3;

        private readonly IAppSettingsProvider appSettingsProvider;

        public MaximumNumberOfImagesPerMaterialProvider(IAppSettingsProvider appSettingsProvider)
        {
            if (appSettingsProvider == null)
            {
                throw new ArgumentNullException("appSettingsProvider");
            }
            
            this.appSettingsProvider = appSettingsProvider;
        }

        public int GetMaximumNumberOfImagesPerMaterial()
        {
            return appSettingsProvider.GetIntegerSetting("Images:MaximumNumberOfImagesPerMaterial", DefaultMaximumNumberOfImages);
        }
    }
}
