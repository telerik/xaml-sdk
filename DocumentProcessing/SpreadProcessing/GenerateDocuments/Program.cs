using System;
#if NETCOREAPP
using Telerik.Windows.Documents.Extensibility;
#endif

namespace GenerateDocuments
{
    internal class Program
    {
        private static void Main()
        {
#if NETCOREAPP
            FontsProviderBase fontsProvider = new FontsProvider();
            FixedExtensibilityManager.FontsProvider = fontsProvider;
#endif

            Console.Write("Choose the format you would like to export to (xlsx/csv/txt/pdf): ");

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
