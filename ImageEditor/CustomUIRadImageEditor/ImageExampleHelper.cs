using System;
using System.IO;
using System.Reflection;
using System.Windows;
using Telerik.Windows.Controls;

namespace CustomUIRadImageEditor
{
    public class ImageExampleHelper
    {
        private static string SampleImageFolder = "SampleImages/";

        public static void LoadSampleImage(RadImageEditor imageEditor, string image)
        {
            using (Stream stream = Application.GetResourceStream(GetResourceUri(SampleImageFolder + image)).Stream)
            {
                imageEditor.Image = new Telerik.Windows.Media.Imaging.RadBitmap(stream);
                imageEditor.ScaleFactor = 0;
                imageEditor.ApplyTemplate();
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
