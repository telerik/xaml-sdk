using System;
using System.Windows;
using Telerik.Windows.Controls.Map;

namespace InformationLayerInitializeCompleted
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void radMap_InitializeCompleted(object sender, EventArgs e)
        {
            this.informationLayer.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
