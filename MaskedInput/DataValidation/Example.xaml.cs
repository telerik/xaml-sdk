using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataValidation
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
            this.firstInput.DataContext = new DataErrorViewModel(false, 0);
            this.secondInput.DataContext = new DataErrorViewModel(true, 0);
            this.thirdInput.DataContext = this.fourthInput.DataContext = new ValidationExceptionsViewModel() { Temperature = 0 };
        }
    }
}
