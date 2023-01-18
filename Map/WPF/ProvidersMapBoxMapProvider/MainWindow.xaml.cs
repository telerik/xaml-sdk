using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace ProvidersMapBoxMapProvider
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new FluentTheme();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string accessToken = this.mapBoxTokenInput.Text;
            MapBoxMapProvider provider = new MapBoxMapProvider();
            provider.AccessToken = accessToken;
            this.map.Provider = provider;            
        }
    }
}
