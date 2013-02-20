using System.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace ProvidersOpenStreetMapProvider
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            OpenStreetMapProvider openStreetMap = new OpenStreetMapProvider();
            this.radMap.Provider = openStreetMap;
        }
    }
}
