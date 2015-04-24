using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace SortGroupByAggregate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Club.GetClubs();
        }

        private void RadGridView_Grouping(object sender, Telerik.Windows.Controls.GridViewGroupingEventArgs e)
        {
            if (e.Action == GroupingEventAction.Place)
            {
                e.Cancel = true;
                ColumnGroupDescriptor currentDescriptor = e.GroupDescriptor as ColumnGroupDescriptor;

                SortingGroupDescriptor sortingDescriptor = new SortingGroupDescriptor();
                sortingDescriptor.Member = (currentDescriptor.Column as GridViewDataColumn).DataMemberBinding.Path.Path;
                foreach (AggregateFunction function in (currentDescriptor.Column as GridViewDataColumn).AggregateFunctions)
                {
                    sortingDescriptor.AggregateFunctions.Add(function);
                }
                (sender as RadGridView).GroupDescriptors.Add(sortingDescriptor);
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var descriptor = this.grid.GroupDescriptors[0] as SortingGroupDescriptor;
            descriptor.SortFunction = descriptor.AggregateFunctions[0];
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            var descriptor = this.grid.GroupDescriptors[0] as SortingGroupDescriptor;
            descriptor.SortFunction = descriptor.AggregateFunctions[1];
        }
    }
}
