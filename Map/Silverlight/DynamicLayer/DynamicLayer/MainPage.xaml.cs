using System.Windows.Controls;

namespace DynamicLayer
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.dynamicLayer.DynamicSource = new MapDynamicSource();
        }
    }
}
