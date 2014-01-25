using System.IO;
using System.Web;

namespace Granta.MaterialsWall.Images
{
    public interface IImagePathFormatter
    {
        string GetImagePath(HttpServerUtilityBase server, string materialId);
    }

    public sealed class ImagePathFormatter : IImagePathFormatter
    {
        private const string MissingImageName = "unknown.png";

        public string GetImagePath(HttpServerUtilityBase server, string materialId)
        {
            string imageDirectory = server.MapPath("~/App_Data/MaterialImages");
            
            if (materialId == null)
            {
                return GetMissingImagePath(imageDirectory);
            }

            string imageName = string.Format("mat{0}.jpg", materialId);
            string imagePath = Path.Combine(imageDirectory, imageName);
            return !File.Exists(imagePath) ? GetMissingImagePath(imageDirectory) : imagePath;
        }

        private string GetMissingImagePath(string imageDirectory)
        {
            string imagePath = Path.Combine(imageDirectory, MissingImageName);
            return imagePath;
        }
    }
}