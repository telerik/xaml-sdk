using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomImageFormatProvider.ImageFormatProviders
{
    public class ExifFormatProvider : DrawingImageFormatProviderBase
    {
        private static readonly string[] supportedExtensions = new string[] { ".exif" };

        public override IEnumerable<string> SupportedExtensions
        {
            get
            {
                return supportedExtensions;
            }
        }
    }
}