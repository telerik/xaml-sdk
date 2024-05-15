using System;
using System.Linq;
using System.Windows;
using IntegrateWithRadGridView;

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
            this.RadGridView1.ItemsSource = Employee.GetEmployees();           
        }
    }
}
