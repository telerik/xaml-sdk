using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Map;

namespace VisualizationLayerClustering
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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
