using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace DropDownWithHeaders
{
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as RadComboBox).IsDropDownOpen = false;
        }
    }
}
