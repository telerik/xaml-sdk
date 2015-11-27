using System.Windows.Controls;

namespace DataBinding
{
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
            this.DataContext = new BankViewModel();
        }
    }
}
