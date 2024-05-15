using System.Windows;

namespace HighlightAddedGridViewRow
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }
    }
}
