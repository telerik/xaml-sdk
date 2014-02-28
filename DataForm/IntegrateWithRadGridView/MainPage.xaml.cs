using System;
using System.Linq;
using System.Windows.Controls;
using IntegrateWithRadGridView;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.RadGridView1.ItemsSource = Employee.GetEmployees();
        }
    }
}