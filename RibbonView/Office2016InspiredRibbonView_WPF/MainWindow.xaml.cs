using Telerik.Windows;
using Telerik.Windows.Controls;

namespace Office2016InspiredRibbonView_WPF
{
    public partial class MainWindow : RadRibbonWindow
    {
        static MainWindow()
        {
            RadRibbonWindow.IsWindowsThemeEnabled = false;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
