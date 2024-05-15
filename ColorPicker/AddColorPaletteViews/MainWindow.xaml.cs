using System.Windows;

namespace AddColorPaletteViews_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CustomColorViewModel();
        }
    }
}
