using System;
using System.Drawing;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Granta.MaterialsWall.Images
{
    public interface IQRCodeGenerator
    {
        Bitmap Generate(string baseUrl, Guid identifier, int size);
    }

    public sealed class QRCodeGenerator : IQRCodeGenerator
    {
        public Bitmap Generate(string baseUrl, Guid identifier, int size)
        {
            string url = string.Format("{0}QR/{1}", baseUrl, identifier);
            var writer = new BarcodeWriter {Format = BarcodeFormat.QR_CODE};
            var options = new QrCodeEncodingOptions {ErrorCorrection = ErrorCorrectionLevel.L, Height = size, Width = size};
            writer.Options = options;
            return writer.Write(url);
        }
    }
}
