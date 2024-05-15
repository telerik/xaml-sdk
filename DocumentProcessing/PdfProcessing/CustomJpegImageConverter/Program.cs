using System;
using System.IO;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model.Resources;

namespace CustomJpegImageConverter
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Choose a value for image quality (1 - High, 2 - Medium, 3 - Low): ");
            string inputQuality = Console.ReadLine();

            FixedExtensibilityManager.JpegImageConverter = new CustomJpegImageConverter();

            ImageSource imageSource;

            string imageName = "Resources/Progress_Logo.png";
            using (Stream imageData = File.OpenRead(imageName))
            {
                imageSource = new ImageSource(imageData);
            }

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

            DocumentGenerator generator = new DocumentGenerator(imageSource, imageQuality);
            generator.SaveFileAndPreview();
        }
    }
}
