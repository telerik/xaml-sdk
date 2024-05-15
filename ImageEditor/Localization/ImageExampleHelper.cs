namespace Localization
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows;

    using Telerik.Windows.Controls;

    public class ImageExampleHelper
    {
        private static readonly string SampleImageFolder = "SampleImages/";

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
            AssemblyName assemblyName = new AssemblyName(typeof(ImageExampleHelper).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}
