using System;
using System.Drawing;
using System.Net.Mime;
using System.Web.Mvc;
using Granta.MaterialsWall.Images;

namespace Granta.MaterialsWall.Controllers
{
    public sealed class ImageController : Controller
    {
        private const int ThumbnailWidth = 240;

        private readonly IImagePathFormatter imagePathFormatter;
        private readonly IImageToRawDataConverter imageToRawDataConverter;
        private readonly IThumbnailGenerator thumbnailGenerator;

        public ImageController(IImagePathFormatter imagePathFormatter, IImageToRawDataConverter imageToRawDataConverter, IThumbnailGenerator thumbnailGenerator)
        {
            if (imagePathFormatter == null)
            {
                throw new ArgumentNullException("imagePathFormatter");
            }

            if (imageToRawDataConverter == null)
            {
                throw new ArgumentNullException("imageToRawDataConverter");
            }

            if (thumbnailGenerator == null)
            {
                throw new ArgumentNullException("thumbnailGenerator");
            }
            
            this.imagePathFormatter = imagePathFormatter;
            this.imageToRawDataConverter = imageToRawDataConverter;
            this.thumbnailGenerator = thumbnailGenerator;
        }

        public ActionResult Index(Guid identifier)
        {
            string imagePath = imagePathFormatter.GetImagePath(Server, identifier);
            var image = new Bitmap(imagePath);
            return GetImageStream(image);
        }

        public ActionResult Thumbnail(Guid identifier)
        {
            string imagePath = imagePathFormatter.GetImagePath(Server, identifier);
            var image = new Bitmap(imagePath);
            var originalSize = image.Size;

            if (originalSize.Width > ThumbnailWidth)
            {
                double scale = ((double) ThumbnailWidth) / originalSize.Width;
                image = thumbnailGenerator.Scale(image, scale);
            }

            return GetImageStream(image);
        }

        private FileResult GetImageStream(Bitmap image)
        {
            var bytes = imageToRawDataConverter.GetImageStream(image);

            var contentDisposition = new ContentDisposition
            {
                FileName = "image.jpg", 
                Inline = true
            };

            Response.AppendHeader("Content-Disposition", contentDisposition.ToString());
            return File(bytes, "image/jpeg");
        }
    }
}