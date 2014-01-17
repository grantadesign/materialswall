using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Granta.MaterialsWall.Images
{
    public interface IImageToRawDataConverter
    {
        byte[] GetImageStream(Bitmap image);
    }

    public sealed class ImageToRawDataConverter : IImageToRawDataConverter
    {
        public byte[] GetImageStream(Bitmap image)
        {
            var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            return stream.ToArray();
        }
    }
}
