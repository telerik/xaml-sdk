using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Telerik.Windows.Controls;

namespace HandleToolCommit
{
    public class ImageImporter
    {
        private static string SampleImageFolder = "SampleImages/";

        public static void LoadSampleImage(RadImageEditorUI imageEditorUI, string image)
        {
            using (Stream stream = Application.GetResourceStream(GetResourceUri(SampleImageFolder + image)).Stream)
            {
                imageEditorUI.Image = new Telerik.Windows.Media.Imaging.RadBitmap(stream);
                imageEditorUI.ApplyTemplate();
                imageEditorUI.ImageEditor.ScaleFactor = 0;
            }
        }

        public static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(ImageImporter).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}
