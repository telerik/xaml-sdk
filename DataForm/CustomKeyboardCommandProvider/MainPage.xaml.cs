using System;
using System.Linq;
using System.Windows.Controls;
using CustomKeyboardCommandProvider;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataForm1.ItemsSource = Employee.GetEmployees();
            this.DataForm1.CommandProvider = new KeyboardCommandProvider(this.DataForm1);
        }
    }
}