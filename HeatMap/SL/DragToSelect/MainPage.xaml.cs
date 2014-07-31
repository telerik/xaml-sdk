using System.Windows.Controls;
using DragToSelect.ViewModels;

namespace DragToSelect
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
