using System.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace ClickModesCustomBehavior
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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
