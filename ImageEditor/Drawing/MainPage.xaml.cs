using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Drawing
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.LoadSampleImage(this.ImageEditorUI, "RadImageEditor.png");
        }

        private string SampleImageFolder = "Images/";

        public void LoadSampleImage(RadImageEditorUI imageEditorUI, string image)
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
            AssemblyName assemblyName = new AssemblyName(typeof(MainPage).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}
