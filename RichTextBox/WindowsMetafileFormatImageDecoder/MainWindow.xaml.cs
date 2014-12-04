using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Media.Imaging;
using WindowsMetafileFormatDecoder.ViewModel;

namespace WindowsMetafileFormatDecoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ImageCodecManager.RegisterDecoder<WindowsMetafileFormatImageDecoder>(new WindowsMetafileFormatImageDecoder());

            InitializeComponent();

            this.DataContext = new ExampleViewModel();
        }
    }
}