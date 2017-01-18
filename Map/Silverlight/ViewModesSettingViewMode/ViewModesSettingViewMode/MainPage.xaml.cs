using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace ViewModesSettingViewMode
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string bingMapsKey = this.BingMapsKey.Text;

            BingRestMapProvider bingMap = new BingRestMapProvider();

            bingMap.Mode = MapMode.Aerial;
            bingMap.IsLabelVisible = true;
            bingMap.ApplicationId = bingMapsKey;

            radMap.Provider = bingMap;
        }
    }
}
