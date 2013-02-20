using System.Windows;
using Telerik.Windows.Controls.Map;

namespace ProvidersOpenStreetMapProvider
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OpenStreetMapProvider openStreetMap = new OpenStreetMapProvider();
            this.radMap.Provider = openStreetMap;
        }
    }
}
