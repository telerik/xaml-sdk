using System.Windows.Controls;

namespace ChangeResizeCursorAtRuntime
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            this.DataContext = new ViewModel();
            InitializeComponent();
        }
    }
}
