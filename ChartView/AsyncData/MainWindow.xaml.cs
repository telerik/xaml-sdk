using System.Windows;

namespace AsyncData
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainVM();
        }
    }
}
