using System.Windows;

namespace ShowTooltipWhenNodeIsClipped
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new TreeViewModel();
        }
    }
}
