using System.Windows;
using Telerik.Windows.Controls.Map;

namespace HowToBringALocationIntoView
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BringLocationIntoView(Location desiredLocation)
        {
            this.radMap.Center = desiredLocation;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.BringLocationIntoView(new Location(42.6957539183824, 23.3327663758679));
        }
    }
}
