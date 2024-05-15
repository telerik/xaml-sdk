using System;
using System.Collections.Generic;
using System.Linq;
using FreeImageAPI;

namespace CustomImageFormatProvider.ImageFormatProviders
{
    public class Jpeg2000FormatProvider : FreeImageFormatProviderBase
    {
        private static readonly string[] supportedExtensions = new string[] { ".jpf", ".j2k", ".jpx", ".jp2" };

        public override string FilesDescription
        {
            get
            {
                return "JPEG 2000 Files";
            }
        }

        public override IEnumerable<string> SupportedExtensions
        {
            get
            {
                return supportedExtensions;
            }
        }

        protected override FREE_IMAGE_FORMAT GetImageFormat()
        {
            return FREE_IMAGE_FORMAT.FIF_JP2;
        }
    }
}