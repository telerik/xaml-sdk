using System.Windows;

namespace Spreadsheet_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeComponent();
            this.radSpreadsheet.Loaded += radSpreadsheet_Loaded;
        }

        void radSpreadsheet_Loaded(object sender, RoutedEventArgs e)
        {
            this.radSpreadsheet.Focus();
        }
    }
}
