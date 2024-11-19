using System.Windows;
using Telerik.Windows.Controls;

namespace SelectedItemsBinding
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Windows11Theme();
            InitializeComponent();
            this.DataContext = new AgencyViewModel();
        }
    }
}
