using System.Windows.Controls;

namespace CustomBoxesItemTemplate
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();

            this.DataContext = new ViewModel();
        }
    }
}
