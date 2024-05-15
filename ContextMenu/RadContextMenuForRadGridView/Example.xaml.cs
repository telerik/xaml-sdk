using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace RadContextMenuForRadGridView
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        private ObservableCollection<MenuItem> HeaderContextMenuItems
        {
            get;
            set;
        }
        private ObservableCollection<MenuItem> RowContextMenuItems
        {
            get;
            set;
        }

        private GridViewHeaderCell ClickedHeader
        {
            get
            {
                return this.GridContextMenu.GetClickedElement<GridViewHeaderCell>();
            }
        }
        private GridViewRow ClickedRow
        {
            get
            {
                return this.GridContextMenu.GetClickedElement<GridViewRow>();
            }
        }

        public Example()
        {
            InitializeComponent();
        
            this.radGridView.ItemsSource = RadGridViewSampleData.GetEmployees();

            this.InitializeHeaderContextMenuItems();
            this.InitializeRowContextMenuItems();
        }

        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<MenuItem> items = new ObservableCollection<MenuItem>();
            MenuItem addItem = new MenuItem();
            addItem.Text = "Add";
            items.Add( addItem );
            MenuItem editItem = new MenuItem();
            editItem.Text = "Edit";
            items.Add( editItem );
            MenuItem deleteItem = new MenuItem();
            deleteItem.Text = "Delete";
            items.Add( deleteItem );
            this.RowContextMenuItems = items;
        }

        private void InitializeHeaderContextMenuItems()
        {
            ObservableCollection<MenuItem> headerItems = new ObservableCollection<MenuItem>();
            ObservableCollection<MenuItem> sortItems = new ObservableCollection<MenuItem>();
            MenuItem sortAscItem = new MenuItem();
            sortAscItem.Text = "Ascending";
            sortItems.Add( sortAscItem );
            MenuItem sortDescItem = new MenuItem();
            sortDescItem.Text = "Descending";
            sortItems.Add( sortDescItem );
            MenuItem sortNoneItem = new MenuItem();
            sortNoneItem.Text = "None";
            sortItems.Add( sortNoneItem );
            MenuItem sortItem = new MenuItem();
            sortItem.Text = "Sort";
            sortItem.SubItems = sortItems;
            headerItems.Add( sortItem );
            MenuItem moveLeftItem = new MenuItem();
            moveLeftItem.Text = "Move Left";
            headerItems.Add( moveLeftItem );
            MenuItem moveRightItem = new MenuItem();
            moveRightItem.Text = "Move Right";
            headerItems.Add( moveRightItem );
            this.HeaderContextMenuItems = headerItems;
        }

        private void GridContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (this.ClickedHeader != null)
            {
                this.GridContextMenu.ItemsSource = this.HeaderContextMenuItems;
            }
            else if (this.ClickedRow != null)
            {
                this.radGridView.SelectedItem = this.ClickedRow.DataContext;
                foreach (var item in this.RowContextMenuItems)
                {
                    item.IsEnabled = true;
                }
                this.GridContextMenu.ItemsSource = this.RowContextMenuItems;
            }
            else
            {
                foreach (var item in this.RowContextMenuItems)
                {
                    if (!item.Text.Equals("Add"))
                    {
                        item.IsEnabled = false;
                    }
                }
                this.GridContextMenu.ItemsSource = this.RowContextMenuItems;
            }
        }

        private void GridContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            MenuItem item = (e.OriginalSource as RadMenuItem).DataContext as MenuItem;
            switch (item.Text)
            {
                case "Add":
                    this.radGridView.BeginInsert();
                    break;
                case "Edit":
                    this.radGridView.BeginEdit();
                    break;
                case "Delete":
                    this.radGridView.Items.Remove(this.radGridView.SelectedItem);
                    break;
                case "Ascending":
                    this.radGridView.SortDescriptors.Clear();
                    this.radGridView.SortDescriptors.Add(new SortDescriptor()
                    {
                        Member = this.ClickedHeader.Column.UniqueName,
                        SortDirection = ListSortDirection.Ascending
                    });
                    break;
                case "Descending":
                    this.radGridView.SortDescriptors.Clear();
                    this.radGridView.SortDescriptors.Add(new SortDescriptor()
                    {
                        Member = this.ClickedHeader.Column.UniqueName,
                        SortDirection = ListSortDirection.Descending
                    });
                    break;
                case "None":
                    this.radGridView.SortDescriptors.Clear();
                    break;
                case "Move Left":
                    if (this.ClickedHeader.Column.DisplayIndex > 0)
                        this.ClickedHeader.Column.DisplayIndex -= 1;
                    break;
                case "Move Right":
                    if (this.ClickedHeader.Column.DisplayIndex < this.radGridView.Columns.Count - 1)
                        this.ClickedHeader.Column.DisplayIndex += 1;
                    break;
            }
        }
    }
}
