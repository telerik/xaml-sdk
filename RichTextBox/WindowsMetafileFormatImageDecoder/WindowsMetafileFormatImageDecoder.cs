using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging;

namespace WindowsMetafileFormatDecoder
{
    public class WindowsMetafileFormatImageDecoder : IImageDecoder
    {
        private const string DecoderName = "WindowsMetafileFormatImageDecoder";
        
        private static readonly IEnumerable<string> supportedExtensions = new string[] { ".wmf", ".emf" };

        public string Name
        {
            get
            {
                return DecoderName;
            }
        }

        public IEnumerable<string> SupportedExtensions
        {
            get
            {
                return supportedExtensions;
            }
        }

        public RadBitmapData Decode(Stream stream)
        {
            Image image = Image.FromStream(stream);
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);

                return this.DecodeInternal(ms);
            }
        }

        private RadBitmapData DecodeInternal(MemoryStream stream)
        {
            BitmapImage bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.CreateOptions = BitmapCreateOptions.None;
            bitmap.StreamSource = stream;
            bitmap.EndInit();

            RadBitmap result = new RadBitmap(bitmap);

            return new RadBitmapData(result.Width, result.Height, result.GetPixels());
        }
    }
}