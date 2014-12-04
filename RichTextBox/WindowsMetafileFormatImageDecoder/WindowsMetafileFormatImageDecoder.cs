using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging;

namespace WindowsMetafileFormatDecoder
{
    public class WindowsMetafileFormatImageDecoder : IImageDecoder
    {
        private static readonly int maxPixelSize = 4096;
        private static readonly string name = "WindowsMetafileFormatImageDecoder";
        private static readonly string pngMimeType = "image/png";
        private static readonly IEnumerable<string> supportedExtensions = new string[] { ".wmf", ".emf" };

        private readonly ImageCodecInfo encoder;

        public string Name
        {
            get
            {
                return WindowsMetafileFormatImageDecoder.name;
            }
        }

        public IEnumerable<string> SupportedExtensions
        {
            get
            {
                return supportedExtensions;
            }
        }

        public WindowsMetafileFormatImageDecoder()
        {
            this.encoder = ImageCodecInfo.GetImageEncoders().FirstOrDefault(enc => enc.MimeType == pngMimeType);
        }

        public RadBitmapData Decode(Stream stream)
        {
            Metafile metaFile = new Metafile(stream);

            int width = metaFile.Width;
            int height = metaFile.Height;
            float scaleFactor = 1f;

            if (metaFile.Width > maxPixelSize || 
                metaFile.Height > maxPixelSize)
            {
                scaleFactor = Math.Max((float)metaFile.Width / (float)maxPixelSize, (float)metaFile.Height / (float)maxPixelSize);
                width = (int)(width / scaleFactor);
                height = (int)(height / scaleFactor);
            }

            // Create a PictureBox control and load the metafile
            PictureBox box = new PictureBox();
            box.Width = width;
            box.Height = height;
            box.BackColor = Color.White;
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            box.Image = metaFile;

            // Create snapshot of the PictureBox and save it as a bitmap
            Bitmap bmp = new Bitmap(width, height);
            box.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));

            //load the image in WPF
            RadBitmap result = null;
            RadBitmapData data = null;
            using (MemoryStream output = new MemoryStream())
            {
                BitmapImage image = new BitmapImage();

                image.BeginInit();
                bmp.Save(output, this.encoder, null);
                output.Seek(0, SeekOrigin.Begin);
                image.StreamSource = output;
                image.EndInit();

                result = new RadBitmap(image);
                data = new RadBitmapData(result.Width, result.Height, result.GetPixels());
            }

            return data;
        }
    }
}
