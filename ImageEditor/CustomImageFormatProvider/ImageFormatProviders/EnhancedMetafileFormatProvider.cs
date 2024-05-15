using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomImageFormatProvider.ImageFormatProviders
{
    public class EnhancedMetafileFormatProvider : DrawingImageFormatProviderBase
    {
        private static readonly string[] supportedExtensions = new string[] { ".emf" };

        public override IEnumerable<string> SupportedExtensions
        {
            get
            {
                return supportedExtensions;
            }
        }

        public override string FilesDescription
        {
            get
            {
                return "Enhanced Metafile Files";
            }
        }
    }
}