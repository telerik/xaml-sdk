using System.Windows.Controls;

namespace DragChartAnnotation
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }        
    }
}
