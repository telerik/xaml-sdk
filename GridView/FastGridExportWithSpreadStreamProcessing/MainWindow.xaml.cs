using System;
using System.Linq;
using System.Windows;

namespace FastGridExportWithSpreadStreamProcessing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ExampleViewModel viewModel = new ExampleViewModel();
            viewModel.GridView = this.RadGridView;
            this.DataContext = viewModel;
        }
    }
}
