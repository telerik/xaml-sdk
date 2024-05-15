using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CustomImageFormatProvider.Utilities;
using FreeImageAPI;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.FormatProviders;

namespace CustomImageFormatProvider.ImageFormatProviders
{
    public abstract class FreeImageFormatProviderBase : IImageFormatProvider
    {
        public abstract IEnumerable<string> SupportedExtensions { get; }

        public virtual bool CanExport
        {
            get
            {
                return true;
            }
        }

        public virtual bool CanImport
        {
            get
            {
                return true;
            }
        }

        public virtual string FilesDescription
        {
            get
            {
                return SupportedExtensions.First().Trim('.').ToUpper() + " Files";
            }
        }

        public byte[] Export(RadBitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                this.Export(image, stream);
                return stream.ToArray();
            }
        }

        public void Export(RadBitmap image, Stream stream)
        {
            Bitmap bitmap = image.ToBitmap();
            FreeImageBitmap freeImageBitmap = new FreeImageBitmap(bitmap);
         
            freeImageBitmap.Save(stream, this.GetImageFormat());
        }

        protected abstract FREE_IMAGE_FORMAT GetImageFormat();

        public RadBitmap Import(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return this.Import(stream);
            }
        }

        public RadBitmap Import(Stream stream)
        {
            FreeImageBitmap freeImageBitmap;
            using (stream)
            {
                freeImageBitmap = FreeImageBitmap.FromStream(stream);
            }

            return freeImageBitmap.ToBitmap().ToRadBitmap();
        }
    }
}