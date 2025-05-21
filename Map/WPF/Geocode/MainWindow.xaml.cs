using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls.Map;

namespace Geocode
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AzureRoutingHelper.AzureMapsSubscriptionKey = this.AzureMapsKey.Text;
            this.radMap.Provider = new AzureMapProvider(this.AzureMapsKey.Text, AzureTileSet.Road);
            this.GeocodeButton.IsEnabled = true;
        }

        private async void GeocodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Location location = await AzureRoutingHelper.GetGeoCode(this.InputBox.Text);

                this.informationLayer.Items.Clear();
                this.informationLayer.Items.Add(location);
                this.radMap.Center = location;
            }
            catch (Exception)
            {
                MessageBox.Show("Please, enter a another location");
            }
        }

        private async void radMap_MapMouseDoubleClick(object sender, MapMouseRoutedEventArgs e)
        {
            try
            {
                Location location = await AzureRoutingHelper.GetGeoCode(e.Location.ToString());

                this.informationLayer.Items.Clear();
                this.informationLayer.Items.Add(location);
                this.radMap.Center = location;
            }
            catch (Exception)
            {
                MessageBox.Show("Please, enter a another location");
            }
        }
    }
}
