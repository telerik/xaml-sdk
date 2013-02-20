using System.Windows;
using Telerik.Windows.Controls.Map;

namespace HowToPutAMarkOnTheClickedLocation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
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
