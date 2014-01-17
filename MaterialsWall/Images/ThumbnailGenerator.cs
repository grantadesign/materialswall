using System;
using System.Drawing;

namespace Granta.MaterialsWall.Images
{
    public interface IThumbnailGenerator
    {
        Bitmap Scale(Bitmap image, double scale);
    }

    public sealed class ThumbnailGenerator : IThumbnailGenerator
    {
        public Bitmap Scale(Bitmap image, double scale)
        {
            var originalSize = image.Size;
            int newWidth = (int) Math.Floor(originalSize.Width * scale);
            int newHeight = (int) Math.Floor(originalSize.Height * scale);

            Bitmap scaledImage = new Bitmap(image, newWidth, newHeight);
            image.Dispose();
            return scaledImage;
        }
    }
}