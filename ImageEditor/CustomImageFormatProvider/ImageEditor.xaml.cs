using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CustomImageFormatProvider.ImageFormatProviders;
using CustomImageFormatProvider.Utilities;
using Telerik.Windows.Media.Imaging.FormatProviders;

namespace CustomImageFormatProvider
{
    /// <summary>
    /// Interaction logic for ImageEditor.xaml
    /// </summary>
    public partial class ImageEditor : UserControl
    {
        public ImageEditor()
        {
            ImageFormatProviderManager.RegisterFormatProvider(new Jpeg2000FormatProvider());
            ImageFormatProviderManager.RegisterFormatProvider(new TargaFormatProvider());
            ImageFormatProviderManager.RegisterFormatProvider(new WindowsMetafileFormatProvider());
            ImageFormatProviderManager.RegisterFormatProvider(new EnhancedMetafileFormatProvider());
            ImageFormatProviderManager.RegisterFormatProvider(new ExifFormatProvider());

            InitializeComponent();
        }

        private void Jpeg2000Button_Click(object sender, RoutedEventArgs e)
        {
            ImageExampleHelper.LoadSampleImage(this.ImageEditorUI.ImageEditor, ".jpf");
        }

        private void TargaButton_Click(object sender, RoutedEventArgs e)
        {
            ImageExampleHelper.LoadSampleImage(this.ImageEditorUI.ImageEditor, ".tga");
        }
    }
}