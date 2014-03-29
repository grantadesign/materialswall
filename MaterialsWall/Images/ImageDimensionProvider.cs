using System;

namespace Granta.MaterialsWall.Images
{
    public interface IImageDimensionProvider
    {
        int GetThumbnailWidth();
        int GetFullsizeWidth();
    }

    public sealed class ImageDimensionProvider : IImageDimensionProvider
    {
        private const int DefaultThumbnailWidth = 240;
        private const int DefaultFullSizeWidth = 800;

        private readonly IAppSettingsProvider appSettingsProvider;

        public ImageDimensionProvider(IAppSettingsProvider appSettingsProvider)
        {
            if (appSettingsProvider == null)
            {
                throw new ArgumentNullException("appSettingsProvider");
            }
            
            this.appSettingsProvider = appSettingsProvider;
        }

        public int GetThumbnailWidth()
        {
            return appSettingsProvider.GetIntegerSetting("Images:ThumbnailWidth", DefaultThumbnailWidth);
        }

        public int GetFullsizeWidth()
        {
            return appSettingsProvider.GetIntegerSetting("Images:FullSizeWidth", DefaultFullSizeWidth);
        }
    }
}
