using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace RadContextMenuAndRadGridViewMVVM
{
    public class ViewModel
    {
        public ViewModel()
        {
            this.Items = LoadData();

            this.SortAscendingCommand = new DelegateCommand(OnSortAscending, CanSortAscending);
            this.SortDescendingCommand = new DelegateCommand(OnSortDescending, CanSortDescending);
            this.ClearSortCommand = new DelegateCommand(OnClearSort, CanClearSort);
            this.GroupbyCommand = new DelegateCommand(OnGroupby, CanGroupby);
            this.UngroupCommand = new DelegateCommand(OnUngroup, CanUngroup);
        }

        public DelegateCommand SortAscendingCommand { get; private set; }
        public DelegateCommand SortDescendingCommand { get; private set; }
        public DelegateCommand ClearSortCommand { get; private set; }
        public DelegateCommand GroupbyCommand { get; private set; }
        public DelegateCommand UngroupCommand { get; private set; }

        public IEnumerable Items
        {
            get;
            private set;
        }

        private static void Sort(GridViewHeaderCell cell, ListSortDirection sortDirection)
        {
            RadGridView grid = cell.Column.DataControl as RadGridView;
            ColumnSortDescriptor sd = (from d in grid.SortDescriptors.OfType<ColumnSortDescriptor>()
                                       where object.Equals(d.Column, cell.Column)
                                       select d).FirstOrDefault();

            if (sd != null)
            {
                grid.SortDescriptors.Remove(sd);
            }

            ColumnSortDescriptor newDescriptor = new ColumnSortDescriptor();
            newDescriptor.Column = cell.Column;
            newDescriptor.SortDirection = sortDirection;

            grid.SortDescriptors.Add(newDescriptor);
        }

        private void OnSortAscending(object parameter)
        {
            GridViewHeaderCell cell = parameter as GridViewHeaderCell;
            if (cell != null && cell.Column != null && cell.Column.DataControl != null && cell.Column.SortingState != SortingState.Ascending)
            {
                Sort(cell, ListSortDirection.Ascending);
            }
        }

        private bool CanSortAscending(object parameter)
        {
            GridViewHeaderCell cell = parameter as GridViewHeaderCell;
            if (cell != null && cell.Column != null && cell.Column.CanSort() && cell.Column.DataControl != null && cell.Column.SortingState != SortingState.Ascending)
            {
                return true;
            }

            return false;
        }

        private void OnSortDescending(object parameter)
        {
            GridViewHeaderCell cell = parameter as GridViewHeaderCell;
            if (cell != null && cell.Column != null && cell.Column.CanSort() && cell.Column.DataControl != null && cell.Column.SortingState != SortingState.Descending)
            {
                Sort(cell, ListSortDirection.Descending);
            }
        }

        private bool CanSortDescending(object parameter)
        {
            GridViewHeaderCell cell = parameter as GridViewHeaderCell;
            if (cell != null && cell.Column != null && cell.Column.CanSort() && cell.Column.DataControl != null && cell.Column.SortingState != SortingState.Descending)
            {
                return true;
            }

            return false;
        }

        private void OnClearSort(object parameter)
        {
            GridViewHeaderCell cell = parameter as GridViewHeaderCell;
            if (cell != null && cell.Column != null && cell.Column.CanSort() && cell.Column.DataControl != null && cell.Column.SortingState != SortingState.None)
            {
                RadGridView grid = cell.Column.DataControl as RadGridView;
                ColumnSortDescriptor sd = (from d in grid.SortDescriptors.OfType<ColumnSortDescriptor>()
                                           where object.Equals(d.Column, cell.Column)
                                           select d).FirstOrDefault();

                if (sd != null)
                {
                    grid.SortDescriptors.Remove(sd);
                }
            }
        }

        private bool CanClearSort(object parameter)
        {
            GridViewHeaderCell cell = parameter as GridViewHeaderCell;
            if (cell != null && cell.Column != null && cell.Column.CanSort() && cell.Column.DataControl != null && cell.Column.SortingState != SortingState.None)
            {
                return true;
            }

            return false;
        }

        private void OnGroupby(object parameter)
        {
            GridViewHeaderCell cell = parameter as GridViewHeaderCell;
            if (cell != null && cell.Column != null && cell.Column.DataControl != null && cell.Column.CanGroup())
            {
                RadGridView grid = cell.Column.DataControl as RadGridView;

                ColumnGroupDescriptor gd = (from d in grid.GroupDescriptors.OfType<ColumnGroupDescriptor>()
                                            where object.Equals(d.Column, cell.Column)
                                            select d).FirstOrDefault();

                if (gd == null)
                {
                    ColumnGroupDescriptor newDescriptor = new ColumnGroupDescriptor();
                    newDescriptor.Column = cell.Column;
                    newDescriptor.SortDirection = ListSortDirection.Ascending;
                    grid.GroupDescriptors.Add(newDescriptor);
                }
            }
        }

        private bool CanGroupby(object parameter)
        {
            GridViewHeaderCell cell = parameter as GridViewHeaderCell;
            if (cell != null && cell.Column != null && cell.Column.DataControl != null && cell.Column.CanGroup())
            {
                RadGridView grid = cell.Column.DataControl as RadGridView;

                ColumnGroupDescriptor gd = (from d in grid.GroupDescriptors.OfType<ColumnGroupDescriptor>()
                                            where object.Equals(d.Column, cell.Column)
                                            select d).FirstOrDefault();

                if (gd == null)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnUngroup(object parameter)
        {
            GridViewHeaderCell cell = parameter as GridViewHeaderCell;
            if (cell != null && cell.Column != null && cell.Column.DataControl != null && cell.Column.CanGroup())
            {
                RadGridView grid = cell.Column.DataControl as RadGridView;

                ColumnGroupDescriptor gd = (from d in grid.GroupDescriptors.OfType<ColumnGroupDescriptor>()
                                            where object.Equals(d.Column, cell.Column)
                                            select d).FirstOrDefault();

                if (gd != null)
                {
                    grid.GroupDescriptors.Remove(gd);
                }
            }
        }

        private bool CanUngroup(object parameter)
        {
            GridViewHeaderCell cell = parameter as GridViewHeaderCell;
            if (cell != null && cell.Column != null && cell.Column.DataControl != null && cell.Column.CanGroup())
            {
                RadGridView grid = cell.Column.DataControl as RadGridView;

                ColumnGroupDescriptor gd = (from d in grid.GroupDescriptors.OfType<ColumnGroupDescriptor>()
                                            where object.Equals(d.Column, cell.Column)
                                            select d).FirstOrDefault();

                if (gd != null)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<Order> LoadData()
        {
            List<string> advertisments = new List<string>(3);
            List<string> products = new List<string>(4);
            List<string> promotions = new List<string>(2);

            advertisments.Add("Direct Mail");
            advertisments.Add("Magazine");
            advertisments.Add("Newspaper");

            products.Add("Copy Holder");
            products.Add("Glare Filter");
            products.Add("Mouse Pad");
            products.Add("Printer Stand");

            promotions.Add("1 Free with 10");
            promotions.Add("Extra Discount");

            List<Order> orders = new List<Order>();
            Random r = new Random(1);
            for (int i = 0; i < 100; i++)
            {
                Order order = new Order() { Net = i % (30 * 365), Date = new DateTime(r.Next(2000, 2032), r.Next(1, 13), r.Next(1, 28)), Advertisement = advertisments[r.Next(0, 3)], Product = products[r.Next(0, 4)], Promotion = promotions[r.Next(0, 2)], Quantity = r.Next(10, 120) };
                orders.Add(order);
            }

            return orders;
        }
    }
}
