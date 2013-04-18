using System;
using System.Linq;
using System.Windows;
using ChangingThemesRuntime;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MyDataContext();
        }
    }
}
