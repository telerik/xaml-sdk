using System;
#if NETCOREAPP
using Telerik.Windows.Documents.Extensibility;
using Telerik.Documents.ImageUtils;
#endif
namespace ConvertDocuments
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
            Console.Write("Press Enter for converting a sample document or paste a path to a file you would like to convert: ");
            string input = Console.ReadLine();

            Console.Write("Choose output format (docx/html/rtf/txt/pdf): ");
            string format = Console.ReadLine().ToLower();

            DocumentConverter converter = new DocumentConverter();
            if (string.IsNullOrEmpty(input))
            {
                converter.ConvertSampleDocument(format);
            }
            else
            {
                converter.ConvertCustomDocument(input, format);
            }

            Console.ReadKey();
        }
    }
}
