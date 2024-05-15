using System;
#if NETCOREAPP
using Telerik.Windows.Documents.Extensibility;
using Telerik.Documents.ImageUtils;
#endif

namespace HtmlGenerator
{
    class Program
    {
        static void Main()
        {
#if NETCOREAPP
            JpegImageConverter jpegImageConverter = new JpegImageConverter();
            FixedExtensibilityManager.JpegImageConverter = jpegImageConverter;
#endif
            DocumentGenerator.Generate();

            Console.Read();
        }
    }
}
