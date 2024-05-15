using System.Windows.Controls;
namespace CustomCurrentTimeIndicatorStyle
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
