using SelectedItemsBinding;
using System.Windows;

namespace SelectedItemsBinding
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new AgencyViewModel();
        }
    }
}
