using System;
using System.Linq;
using System.Windows.Controls;
using DefiningCustomDataFields;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataForm1.ItemsSource = Employee.GetEmployees();
        }
    }
}