using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Data;
using System.Collections;
using BoundSelectColumn;
using Telerik.Windows.Controls.GridView;
using System.Windows.Data;

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
