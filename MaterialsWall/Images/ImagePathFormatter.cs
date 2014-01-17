using System.IO;
using System.Web;

namespace Granta.MaterialsWall.Images
{
    public interface IImagePathFormatter
    {
        string GetImagePath(HttpServerUtilityBase server, string imageName);
    }

    public sealed class ImagePathFormatter : IImagePathFormatter
    {
        private const string MissingImageName = "unknown.jpg";

        public string GetImagePath(HttpServerUtilityBase server, string imageName)
        {
            string imageDirectory = server.MapPath("~/App_Data/MaterialImages");
            string imagePath = Path.Combine(imageDirectory, imageName);

            if (!File.Exists(imagePath))
            {
                imagePath = Path.Combine(imageDirectory, MissingImageName);
            }

            return imagePath;
        }
    }
}