using System;
#if NETCOREAPP
using Telerik.Windows.Documents.Extensibility;
using Telerik.Documents.ImageUtils;
#endif

namespace GenerateDocument
{
    internal class Program
    {
        private static void Main()
        {
#if NETCOREAPP
            FontsProviderBase fontsProvider = new FontsProvider();
            FixedExtensibilityManager.FontsProvider = fontsProvider;

            JpegImageConverter jpegImageConverter = new JpegImageConverter();
            FixedExtensibilityManager.JpegImageConverter = jpegImageConverter;
#endif
            Console.Write("Choose the format you would like to export to (docx/html/rtf/txt/pdf): ");

            string input = Console.ReadLine();

            DocumentGenerator generator = new DocumentGenerator();

            if (!string.IsNullOrEmpty(input))
            {
                generator.SelectedExportFormat = input;
            }

            generator.Generate();

            Console.Read();
        }
    }
}
