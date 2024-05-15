using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.FormatProviders;

namespace CustomImageFormatProvider.ImageFormatProviders
{
    public abstract class DrawingImageFormatProviderBase : ImageFormatProviderBase
    {
        public override bool CanExport
        {
            get
            {
                return false;
            }
        }

        public override bool CanImport
        {
            get
            {
                return true;
            }
        }

        public override RadBitmap Import(Stream stream)
        {
            Image image = Image.FromStream(stream);
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);

                return base.Import(ms);
            }
        }
    }
}