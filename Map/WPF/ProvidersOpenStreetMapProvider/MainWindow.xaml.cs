using System.Windows;
using Telerik.Windows.Controls.Map;

namespace ProvidersOpenStreetMapProvider
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OpenStreetMapProvider openStreetMap = new OpenStreetMapProvider()
            {
                // This user agent should be set per application.
                // Please specify different string in your application.
                StandardModeUserAgent = "Telerik UI for WPF SDK samples"
            };
            this.radMap.Provider = openStreetMap;
        }
    }
}
