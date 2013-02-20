using System;
using System.Windows.Controls;

namespace InformationLayerInitializeCompleted
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void radMap_InitializeCompleted(object sender, EventArgs e)
        {
            this.informationLayer.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
