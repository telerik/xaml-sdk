using System;
using System.IO;
using System.Reflection;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Media.Imaging.FormatProviders;

namespace CustomImageFormatProvider.Utilities
{
    public class ImageExampleHelper
    {
        private const string SampleImageFolder = "SampleImages/telerik";

        public static void LoadSampleImage(RadImageEditor imageEditor, string extension)
        {
            IImageFormatProvider formatProvider = ImageFormatProviderManager.GetFormatProviderByExtension(extension);

            using (Stream stream = Application.GetResourceStream(GetResourceUri(SampleImageFolder + extension)).Stream)
            {
                imageEditor.Image = formatProvider.Import(stream);

                imageEditor.ApplyTemplate();
                imageEditor.ScaleFactor = 0;
            }
        }

        public static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(ImageExampleHelper).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}