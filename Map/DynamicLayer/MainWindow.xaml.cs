using System.Windows;

namespace DynamicLayer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.dynamicLayer.DynamicSource = new MapDynamicSource();
        }
    }
}
