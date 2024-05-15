using System;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;

namespace CreateDocumentWithImages
{
    public class Program
    {
        private static readonly string ResultDirName = AppDomain.CurrentDomain.BaseDirectory + "Demo results/";

        private static void Main()
        {
#if NETCOREAPP
            Console.Write("Libraries built against .Net Standard can not export ImageQuality different than High. Exporting an image with high quality. ");
            string inputQuality = "1";
#else
            Console.Write("Choose a value for image quality (1 - High, 2 - Medium, 3 - Low): ");
            string inputQuality = Console.ReadLine();
#endif

            ImageQuality imageQuality;

            switch (inputQuality)
            {
                case "1":
                    imageQuality = ImageQuality.High;
                    break;
                case "2":
                    imageQuality = ImageQuality.Medium;
                    break;
                case "3":
                    imageQuality = ImageQuality.Low;
                    break;
                default:
                    imageQuality = ImageQuality.High;
                    break;
            }

            DocumentGenerator generator = new DocumentGenerator(imageQuality);
            generator.SaveFile(ResultDirName);

            Console.Read();
        }
    }
}
