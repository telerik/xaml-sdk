using System.Windows.Controls;

namespace AsyncData
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.DataContext = new MainVM();
        }
    }
}
