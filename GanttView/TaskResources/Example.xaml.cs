using System.Windows.Controls;

namespace TaskResources
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
