using ImageMagick;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;

namespace CustomJpegImageConverter
{
    internal class CustomJpegImageConverter : JpegImageConverterBase
    {
        public override bool TryConvertToJpegImageData(byte[] imageData, ImageQuality imageQuality, out byte[] jpegImageData)
        {
            MagickFormatInfo formatInfo = MagickFormatInfo.Create(imageData);
            if (formatInfo != null && formatInfo.IsReadable)
            {
                using (MagickImage magickImage = new MagickImage(imageData))
                {
                    magickImage.Alpha(AlphaOption.Remove);
                    magickImage.Quality = (int)imageQuality;

                    jpegImageData = magickImage.ToByteArray(MagickFormat.Jpeg);
                }

                return true;
            }

            jpegImageData = null;
            return false;
        }
    }
}