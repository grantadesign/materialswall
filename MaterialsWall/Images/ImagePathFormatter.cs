using System;
using System.IO;
using System.Web;

namespace Granta.MaterialsWall.Images
{
    public interface IImagePathFormatter
    {
        string GetImagePath(HttpServerUtilityBase server, Guid identifier);
    }

    public sealed class ImagePathFormatter : IImagePathFormatter
    {
        public string GetImagePath(HttpServerUtilityBase server, Guid identifier)
        {
            string imageDirectory = server.MapPath("~/App_Data/MaterialImages");
            string imagePath = Path.Combine(imageDirectory, "tree.jpg");
            return imagePath;
        }
    }
}