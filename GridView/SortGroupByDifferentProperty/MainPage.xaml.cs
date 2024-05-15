using System;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using System.Linq;

namespace SortGroupByDifferentProperty
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = Club.GetClubs();
        }
        private void RadGridView_Grouping(object sender, Telerik.Windows.Controls.GridViewGroupingEventArgs e)
        {
            if (e.Action == GroupingEventAction.Place)
            {
                e.Cancel = true;
                var descriptor = new GroupDescriptor<Club, string, int>
                {
                    GroupingExpression = i => i.Name,
                    GroupSortingExpression = group => group.ElementAt(0).StadiumCapacity
                };
                descriptor.DisplayContent = ((Telerik.Windows.Data.GroupDescriptorBase)(e.GroupDescriptor)).DisplayContent;
                descriptor.SortDirection = e.GroupDescriptor.SortDirection;
                this.grid.GroupDescriptors.Add(descriptor);
            }
        }
    }
}
