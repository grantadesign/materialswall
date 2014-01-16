using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using System.Web.Mvc;

namespace Granta.MaterialsWall.Controllers
{
    public sealed class ImageController : Controller
    {
        private const int ThumbnailWidth = 240;

        public ActionResult Index(Guid identifier)
        {
            string imagePath = GetImagePath(identifier);
            var image = new Bitmap(imagePath);
            return GetImageStream(image);
        }

        public ActionResult Thumbnail(Guid identifier)
        {
            string imagePath = GetImagePath(identifier);
            var image = new Bitmap(imagePath);
            var originalSize = image.Size;

            if (originalSize.Width > ThumbnailWidth)
            {
                double scale = ((double) ThumbnailWidth) / originalSize.Width;
                int newHeight = (int) Math.Floor(originalSize.Height * scale);

                Bitmap scaledImage = new Bitmap(image, ThumbnailWidth, newHeight);
                image.Dispose();
                image = scaledImage;
            }

            return GetImageStream(image);
        }

        private string GetImagePath(Guid identifier)
        {
            string imageDirectory = Server.MapPath("~/App_Data/MaterialImages");
            string imagePath = Path.Combine(imageDirectory, "tree.jpg");
            return imagePath;
        }

        private FileResult GetImageStream(Bitmap image)
        {
            var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);

            var contentDisposition = new ContentDisposition
            {
                FileName = "image.jpg", 
                Inline = true
            };

            Response.AppendHeader("Content-Disposition", contentDisposition.ToString());
            return File(stream.ToArray(), "images/jpeg");
        }
    }
}