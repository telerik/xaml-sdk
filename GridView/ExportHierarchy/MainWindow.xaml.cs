using System.Windows;

namespace ExportHierarchy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.clubsGrid.Export("xlsx");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.clubsGrid.Export("pdf");
        }
    }
}
