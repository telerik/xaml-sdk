using System.Windows.Controls;
using Telerik.Windows.Controls;
#if SILVERLIGHT
using SelectionChangedEventArgs = Telerik.Windows.Controls.SelectionChangedEventArgs;
#endif

namespace DropDownWithHeaders
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
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
