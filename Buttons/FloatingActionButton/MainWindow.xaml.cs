using System.Windows;
using Telerik.Windows.Controls;

namespace FloatingActionButton_WPF
{
    public partial class MainWindow : Window
    {
        static MainWindow()
        {
            StyleManager.ApplicationTheme = new MaterialTheme();
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
