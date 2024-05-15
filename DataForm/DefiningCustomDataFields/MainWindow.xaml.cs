using System;
using System.Linq;
using System.Windows;
using DefiningCustomDataFields;

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
            this.DataForm1.ItemsSource = Employee.GetEmployees();           
        }
    }
}
