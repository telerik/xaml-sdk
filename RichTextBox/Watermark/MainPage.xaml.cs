using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Documents.Model;

namespace Watermark
{
    public partial class MainPage : UserControl
    {
        private string SampleImagePath = @"Images/telerik-ninja.jpg";

        public MainPage()
        {
            InitializeComponent();
        }

        private void addPredefined_Click(object sender, RoutedEventArgs e)
        {
            this.editor.radRichTextBox.SetWatermark(Telerik.Windows.Documents.Model.PredefinedWatermarkType.Confidential);
        }

        private void addText_Click(object sender, RoutedEventArgs e)
        {
            WatermarkTextSettings textSettings = new WatermarkTextSettings();
            textSettings.Text = "Custom Watermark";
            textSettings.RotateAngle = 30;
            textSettings.Opacity = 1;
            textSettings.ForegroundColor = Colors.Purple;

            this.editor.radRichTextBox.SetWatermarkText(textSettings);
        }

        private void addImage_Click(object sender, RoutedEventArgs e)
        {
            WatermarkImageSettings imageSettings = new WatermarkImageSettings();

            imageSettings.UriSource = GetResourceUri(SampleImagePath);
            imageSettings.Size = new Size(500, 200);

            this.editor.radRichTextBox.SetWatermarkImage(imageSettings);
        }

        private Uri GetResourceUri(object resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(MainPage).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}
