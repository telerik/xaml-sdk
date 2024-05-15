using System;
using System.Linq;
using System.Windows.Controls;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void clubsGrid2_Filtered(object sender, Telerik.Windows.Controls.GridView.GridViewFilteredEventArgs e)
        {
            foreach (var item in e.ColumnFilterDescriptor.DistinctFilter.FilterDescriptors)
            {
                item.IsCaseSensitive = false;
            }
        }

        private void clubsGrid2_DistinctValuesLoading(object sender, Telerik.Windows.Controls.GridView.GridViewDistinctValuesLoadingEventArgs e)
        {
            if (e.Column.UniqueName == "Name")
            {
                e.ItemsSource = ((Telerik.Windows.Controls.RadGridView)sender).GetDistinctValues(e.Column, false).OfType<string>().Select(x => x.ToLower()).Distinct();
            }
        }
    }
}