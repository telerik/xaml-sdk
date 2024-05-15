using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Data;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void clubsGrid2_Filtered(object sender, GridViewFilteredEventArgs e)
        {
            foreach (var item in e.ColumnFilterDescriptor.DistinctFilter.FilterDescriptors)
            {
                item.IsCaseSensitive = false;
            }
        }

        private void clubsGrid2_DistinctValuesLoading(object sender, GridViewDistinctValuesLoadingEventArgs e)
        {
            if (e.Column.UniqueName == "Name")
            {
                e.ItemsSource = ((Telerik.Windows.Controls.RadGridView)sender).GetDistinctValues(e.Column, false).OfType<string>().Select(x => x.ToLower()).Distinct();
            }
        }
    }
}
