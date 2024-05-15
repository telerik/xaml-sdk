using System.Windows.Controls;

namespace SingleSelectionModeWithClearButton
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();

            this.DataContext = new MyViewModel();
        }
    }
}
