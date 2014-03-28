using System.IO;
using System.Web;

namespace Granta.MaterialsWall.Images
{
    public interface IImagePathFormatter
    {
        string GetImagePath(string materialId, int imageIndex);
        bool DoesImageExist(string materialId, int imageIndex);
    }

    public sealed class ImagePathFormatter : IImagePathFormatter
    {
        private const string MissingImageName = "unknown.png";

        public string GetImagePath(string materialId, int imageIndex)
        {
            string imageDirectory = HttpContext.Current.Server.MapPath("~/App_Data/MaterialImages");
            
            if (materialId == null)
            {
                return GetMissingImagePath(imageDirectory);
            }

            string indexSegment = imageIndex == 1 ? string.Empty : "-" + imageIndex;
            string imageName = string.Format("mat{0}{1}.jpg", materialId, indexSegment);
            string imagePath = Path.Combine(imageDirectory, imageName);
            return !File.Exists(imagePath) ? GetMissingImagePath(imageDirectory) : imagePath;
        }

        public bool DoesImageExist(string materialId, int imageIndex)
        {
            string path = GetImagePath(materialId, imageIndex);
            return !path.EndsWith(MissingImageName);
        }

        private string GetMissingImagePath(string imageDirectory)
        {
            string imagePath = Path.Combine(imageDirectory, MissingImageName);
            return imagePath;
        }
    }
}
