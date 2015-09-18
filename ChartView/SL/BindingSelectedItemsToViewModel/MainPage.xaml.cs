using System.Windows.Controls;

namespace BindingSelectedItemsToViewModel
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}
