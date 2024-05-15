using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using SortDirectionTooltip;
using Telerik.Windows.Controls.GridView;
using System.ComponentModel;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyViewModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = this.Resources["MyViewModel"] as MyViewModel;

            ColumnSortDescriptor csd = new ColumnSortDescriptor()
            {
                Column = this.clubsGrid.Columns["StadiumCapacity"],
                SortDirection = ListSortDirection.Descending
            };
            this.clubsGrid.SortDescriptors.Add(csd);
            model.MySortingToolTip = "Descending Order";

        }

        private void clubsGrid_Sorting(object sender, GridViewSortingEventArgs e)
        {
            if (e.NewSortingState == SortingState.Ascending)
            {
                model.MySortingToolTip = "Ascending Order";
            }
            else if (e.NewSortingState == SortingState.Descending)
            {
                model.MySortingToolTip = "Descending Order";
            }
            else
            {
                model.MySortingToolTip = "";
            }
        }

    }
}
