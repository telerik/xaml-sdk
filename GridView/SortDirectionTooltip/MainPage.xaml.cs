using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using SortDirectionTooltip;
using System.ComponentModel;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        MyViewModel model;
        public MainPage()
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