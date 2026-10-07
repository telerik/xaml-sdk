using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls.Map;

namespace VisualizationLayerClustering
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExpandClusterToPolygon(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            if (element != null)
            {
                ClusterData data = element.DataContext as ClusterData;
                if (data != null)
                {
                    if (data.ClusterState != ClusterState.ExpandedToPolygon)
                    {
                        data.HideExpanded = false;
                        data.ClusterState = ClusterState.ExpandedToPolygon;
                    }
                    else
                    {
                        data.ClusterState = ClusterState.Collapsed;
                    }
                }
            }

            e.Handled = true;
        }

        private void ExpandCluster(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            if (element != null)
            {
                ClusterData data = element.DataContext as ClusterData;
                if (data != null)
                {
                    if (data.ClusterState != ClusterState.Expanded)
                    {
                        data.HideExpanded = false;
                        data.ClusterState = ClusterState.Expanded;
                    }
                    else
                    {
                        data.ClusterState = ClusterState.Collapsed;
                    }
                }
            }

            e.Handled = true;
        }
    }
}
