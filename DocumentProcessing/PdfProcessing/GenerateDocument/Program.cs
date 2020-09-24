using System;
#if NETCOREAPP
using Telerik.Windows.Documents.Extensibility;
#endif

namespace GenerateDocument
{
    public class Program
    {
        private static readonly string ResultDirName = AppDomain.CurrentDomain.BaseDirectory + "Demo results/";

        public static void Main()
        {
#if NETCOREAPP
            FontsProviderBase fontsProvider = new FontsProvider();
            FixedExtensibilityManager.FontsProvider = fontsProvider;
#endif

            DocumentGenerator generator = new DocumentGenerator();
            generator.Export(ResultDirName);
        }
    }
}