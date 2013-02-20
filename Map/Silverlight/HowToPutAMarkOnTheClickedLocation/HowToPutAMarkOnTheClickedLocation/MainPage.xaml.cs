using System.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace HowToPutAMarkOnTheClickedLocation
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void radMap_MapMouseClick(object sender, MapMouseRoutedEventArgs eventArgs)
        {
            this.informationLayer.Items.Clear();
            this.informationLayer.Items.Add(eventArgs.Location);
        }
    }
}
