using System;

namespace Granta.MaterialsWall.Images
{
    public interface IImagePresenceChecker
    {
        bool DoesImageExist(string materialId, int imageIndex);
    }

    public sealed class ImagePresenceChecker : IImagePresenceChecker
    {
        private readonly IImagePathFormatter imagePathFormatter;
        private readonly IMissingImageFilenameProvider missingImageFilenameProvider;

        public ImagePresenceChecker(IImagePathFormatter imagePathFormatter, IMissingImageFilenameProvider missingImageFilenameProvider)
        {
            if (imagePathFormatter == null)
            {
                throw new ArgumentNullException("imagePathFormatter");
            }

            if (missingImageFilenameProvider == null)
            {
                throw new ArgumentNullException("missingImageFilenameProvider");
            }
            
            this.imagePathFormatter = imagePathFormatter;
            this.missingImageFilenameProvider = missingImageFilenameProvider;
        }

        public bool DoesImageExist(string materialId, int imageIndex)
        {
            string path = imagePathFormatter.GetImagePath(materialId, imageIndex);
            return !path.EndsWith(missingImageFilenameProvider.GetMissingImageName());
        }
    }
}
