using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Documents.Model;

namespace Watermark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string SampleImagePath = @"Images/telerik-ninja.jpg";

        public MainWindow()
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
            AssemblyName assemblyName = new AssemblyName(typeof(MainWindow).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}
