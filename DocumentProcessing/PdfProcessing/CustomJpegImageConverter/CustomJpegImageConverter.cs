using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;

namespace CustomJpegImageConverter
{
    internal class CustomJpegImageConverter : JpegImageConverterBase
    {
        public override bool TryConvertToJpegImageData(byte[] imageData, ImageQuality imageQuality, out byte[] jpegImageData)
        {
            string[] imageSharpImageFormats = new string[] { "jpeg", "bmp", "png", "gif" };
            string imageFormat;

            if (this.TryGetImageFormat(imageData, out imageFormat) && imageSharpImageFormats.Contains(imageFormat.ToLower()))
            {
                using (Image imageSharp = Image.Load(imageData))
                {
                    imageSharp.Mutate(x => x.BackgroundColor(Color.White));

                    JpegEncoder options = new JpegEncoder
                    {
                        Quality = (int)imageQuality,
                    };

                    MemoryStream ms = new MemoryStream();
                    imageSharp.SaveAsJpeg(ms, options);

                    jpegImageData = ms.ToArray();
                }

                return true;
            }

            jpegImageData = null;
            return false;
        }
    }
}