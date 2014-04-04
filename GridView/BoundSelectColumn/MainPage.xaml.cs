using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using System.Windows;
using System.Windows.Data;
using BoundSelectColumn;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void clubsGrid_RowLoaded(object sender, RowLoadedEventArgs e)
        {
            var row = e.Row as GridViewRow;

            if (row == null)
                return;

            var selectedBinding = new Binding("IsSelected");
            selectedBinding.Source = row.Item;
            selectedBinding.Mode = BindingMode.TwoWay;

            row.SetBinding(GridViewRow.IsSelectedProperty, selectedBinding);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            (this.clubsGrid.Items[3] as Club).IsSelected = true;
            (this.clubsGrid.Items[2] as Club).IsSelected = true;
        }
    }
}