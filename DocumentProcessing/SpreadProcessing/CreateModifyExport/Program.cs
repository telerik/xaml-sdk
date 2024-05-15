using System;
#if NETCOREAPP
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Core.Imaging;
#endif
namespace CreateModifyExport
{
    internal class Program
    {
        private static void Main()
        {
#if NETCOREAPP
            ImagePropertiesResolverBase imagePropertiesResolver = new ImageInfo();
            SpreadExtensibilityManager.ImagePropertiesResolver = imagePropertiesResolver;

            FontsProviderBase fontsProvider = new FontsProvider();
            FixedExtensibilityManager.FontsProvider = fontsProvider;
#endif

            Console.Write("Choose destination for export or press Enter for the default one: ");
            string input = Console.ReadLine();

            ReportGenerator generator = new ReportGenerator();

            if (!string.IsNullOrEmpty(input))
            {
                generator.ExportDirectory = input;
            }

            generator.ExportReports();
            Console.ReadKey();
        }
    }
}
