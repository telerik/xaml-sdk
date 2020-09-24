using System;
#if NETCOREAPP
using Telerik.Windows.Documents.Extensibility;
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
#endif

            Console.Write("Press Enter for converting a sample document or paste a path to a file you would like to convert: ");
            string input = Console.ReadLine();


            Console.Write("Choose output format (xlsx/csv/txt/pdf): ");
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
