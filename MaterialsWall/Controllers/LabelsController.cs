using System;
using System.Drawing;
using System.Net.Mime;
using System.Web.Mvc;
using Granta.MaterialsWall.DataAccess;
using Granta.MaterialsWall.Images;

namespace Granta.MaterialsWall.Controllers
{
    public sealed class LabelsController : Controller
    {
        private const int QRCodeSize = 125;

        private readonly ICardRepository cardRepository;
        private readonly IQRCodeGenerator codeGenerator;
        private readonly IImageToRawDataConverter imageToRawDataConverter;

        public LabelsController(ICardRepository cardRepository, IQRCodeGenerator codeGenerator, IImageToRawDataConverter imageToRawDataConverter)
        {
            if (cardRepository == null)
            {
                throw new ArgumentNullException("cardRepository");
            }

            if (codeGenerator == null)
            {
                throw new ArgumentNullException("codeGenerator");
            }

            if (imageToRawDataConverter == null)
            {
                throw new ArgumentNullException("imageToRawDataConverter");
            }
            
            this.cardRepository = cardRepository;
            this.codeGenerator = codeGenerator;
            this.imageToRawDataConverter = imageToRawDataConverter;
        }

        public ActionResult Index()
        {
            var cards = cardRepository.GetCards();
            return View(cards);
        }

        public FileResult Image(Guid identifier)
        {
            var qrCode = codeGenerator.Generate(identifier, QRCodeSize);
            return GetImageStream(qrCode);
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
