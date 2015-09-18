using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomImageFormatProvider.ImageFormatProviders
{
    public class WindowsMetafileFormatProvider : DrawingImageFormatProviderBase
    {
        private static readonly string[] supportedExtensions = new string[] { ".wmf" };
       
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
                return "Windows Metafile Files";
            }
        }
    }
}