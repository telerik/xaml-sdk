using System;
using System.Collections.Generic;
using System.Linq;
using FreeImageAPI;

namespace CustomImageFormatProvider.ImageFormatProviders
{
    public class TargaFormatProvider : FreeImageFormatProviderBase
    {
        private static readonly string[] supportedExtensions = new string[] { ".tga" };

        public override IEnumerable<string> SupportedExtensions
        {
            get
            {
                return supportedExtensions;
            }
        }

        protected override FREE_IMAGE_FORMAT GetImageFormat()
        {
            return FREE_IMAGE_FORMAT.FIF_TARGA;
        }
    }
}