using System.Windows.Controls;

namespace BindingSelectedItemsFromViewModel
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MyDataContext();
        }
    }
}
