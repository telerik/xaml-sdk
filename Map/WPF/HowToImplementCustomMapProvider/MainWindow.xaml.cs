using System.Net;
using System.Windows;

namespace HowToImplementCustomMapProvider
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // ArcGis rest services require TLS 1.2 protocol.
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            InitializeComponent();
        }
    }
}
