using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace ExportToHighQualityImage
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Office2013Theme();
            DiagramConstants.MinimumZoom = 0.25;

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.png";
            if (dialog.ShowDialog() == true)
            {
                using (var stream = File.Create(dialog.FileName))
                {
                    CustomExportExtensions.ExportToImage(this.diagram, stream, backgroundBrush: Brushes.White, dpi: 500, margin: new Thickness(20));
                }
            }
        }        
    }
}
