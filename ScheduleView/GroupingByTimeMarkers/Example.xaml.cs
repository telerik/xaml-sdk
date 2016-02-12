using System.Windows.Controls;

namespace GroupingByTimeMarkers
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            this.DataContext = new MyViewModel();
            InitializeComponent();
        }
    }
}
