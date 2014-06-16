using System;
using System.IO;

namespace Granta.MaterialsWall.Images
{
    public interface IImagePathFormatter
    {
        string GetImagePath(string materialId, int imageIndex);
    }

    public sealed class ImagePathFormatter : IImagePathFormatter
    {
        private readonly IServerPathMapper serverPathMapper;
        private readonly IMaterialsImagesPathProvider materialsImagesPathProvider;
        private readonly IMissingImageFilenameProvider missingImageFilenameProvider;

        public ImagePathFormatter(IServerPathMapper serverPathMapper, IMaterialsImagesPathProvider materialsImagesPathProvider, IMissingImageFilenameProvider missingImageFilenameProvider)
        {
            if (serverPathMapper == null)
            {
                throw new ArgumentNullException("serverPathMapper");
            }
            
            if (materialsImagesPathProvider == null)
            {
                throw new ArgumentNullException("missingImageFilenameProvider");
            }

            if (materialsImagesPathProvider == null)
            {
                throw new ArgumentNullException("missingImageFilenameProvider");
            }

            this.serverPathMapper = serverPathMapper;
            this.materialsImagesPathProvider = materialsImagesPathProvider;
            this.missingImageFilenameProvider = missingImageFilenameProvider;
        }

        public string GetImagePath(string materialId, int imageIndex)
        {
            string pathToMaterialsImages = materialsImagesPathProvider.GetMaterialsImagesPath();
            string imageDirectory = serverPathMapper.MapPath(pathToMaterialsImages);
            
            if (materialId == null)
            {
                return GetMissingImagePath(imageDirectory);
            }

            // the first image's file name is mat[id].jpg, whereas for subsequent images it is mat[id]-[index].jpg
            string indexSegment = imageIndex == 1 ? string.Empty : "-" + imageIndex;
            string imageName = string.Format("mat{0}{1}.jpg", materialId, indexSegment);
            string imagePath = Path.Combine(imageDirectory, imageName);
            return !File.Exists(imagePath) ? GetMissingImagePath(imageDirectory) : imagePath;
        }

        private string GetMissingImagePath(string imageDirectory)
        {
            string missingImageFilename = missingImageFilenameProvider.GetMissingImageName();
            string imagePath = Path.Combine(imageDirectory, missingImageFilename);
            return imagePath;
        }
    }
}
