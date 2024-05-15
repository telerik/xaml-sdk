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
            // This will register the WindowsMetafileFormatImageDecoder. You should use this if you use DevCraft versions earlier than R1 2017.
            // From R1 2017 this decoder will be built-in for ImageCodedManager class which is used internally.
            ImageCodecManager.RegisterDecoder<WindowsMetafileFormatImageDecoder>(new WindowsMetafileFormatImageDecoder());

            InitializeComponent();

            this.DataContext = new ExampleViewModel();
        }
    }
}