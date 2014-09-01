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

namespace VisualizationLayerShapeSelection
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ShowSelectedItems(object sender, RoutedEventArgs e)
        {
            string condition = this.filter.Text.Trim();
            double minSqkm;
            if (double.TryParse(condition, out minSqkm))
            {
                IEnumerable<object> itemsToSelect = from MapShapeData item in this.visualizationLayer.Items
                                                    where ((double)item.ExtendedData.GetValue("SQKM")) > minSqkm * 1000d
                                                    select item;
                this.visualizationLayer.Select(itemsToSelect, true);
            }
        }

        private void ReverseSelection(object sender, RoutedEventArgs e)
        {
            this.visualizationLayer.ReverseSelection(this.visualizationLayer.Items);
        }
    }
}
