using System.Windows;
using Telerik.Windows.Controls.Map;

namespace ClickModesCustomBehavior
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void radMap_MapMouseClick(object sender, MapMouseRoutedEventArgs e)
        {
            //implement logic regarding single click here
        }

        private void radMap_MapMouseDoubleClick(object sender, MapMouseRoutedEventArgs e)
        {
            //implement logic regarding double click here
        }
    }
}
