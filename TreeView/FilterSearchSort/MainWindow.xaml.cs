using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using FilterSearchSort.ViewModels;
using System.Collections.ObjectModel;

namespace FilterSearchSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isFiltered = false;
        string filterText = null;
        string searchText = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        //whenever an item is prepared by the RadTreeView for display, we make sure to expand it if it is part of a filtered path
        private void radTreeView_ItemPrepared(object sender, Telerik.Windows.Controls.RadTreeViewItemPreparedEventArgs e)
        {
            if (isFiltered && !string.IsNullOrEmpty(filterText) && e.PreparedItem.Item is Category)
            {
                e.PreparedItem.IsExpanded = true;
            }
            else
            {
                e.PreparedItem.IsExpanded = false;
            }
        }

        //When the Sorting ComboBox selection is changed, the SampleDataSource collection is sorted accordingly
        private void sortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sortingComboBox.SelectedItem as RadComboBoxItem).Content.Equals("Category name"))
            {
                if (filterText != null)
                {
                    radTreeView.ItemsSource = FilterCollection(new SampleDataSource(), filterText).OrderBy(c => c.Name);
                }
                else
                {
                    radTreeView.ItemsSource = new SampleDataSource().OrderBy(c => c.Name);
                }
            }
            else
            {
                if (filterText != null)
                {
                    radTreeView.ItemsSource = FilterCollection(new SampleDataSource(), filterText).OrderBy(c => c.Products.Count);
                }
                else
                {
                    radTreeView.ItemsSource = new SampleDataSource().OrderBy(c => c.Products.Count);
                }
            }

            isFiltered = false;
        }

        //the SampleDataSource collection is dynamically filtered to display only those items matching the filter criteria
        private void filterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(filterTextBox.Text))
            {
                filterText = filterTextBox.Text == " Enter product name" ? "" : filterTextBox.Text;
                radTreeView.ItemsSource = FilterCollection(new SampleDataSource(), filterText);
            }
            else
            {
                radTreeView.ItemsSource = new SampleDataSource();
                filterText = null;
            }

            isFiltered = true;
        }

        //this method filters a business collection 
        private ObservableCollection<Category> FilterCollection(ObservableCollection<Category> collection, string filterText)
        {
            foreach (Category category in collection)
            {
                category.Products = new ObservableCollection<Product>(category.Products.Where(p => p.Name.ToLower().Contains(filterText)));
            }

            return new ObservableCollection<Category>(collection.Where(cat => (cat.Name.ToLower().Contains(filterText) && cat.Products.Count == 0) || cat.Products.Count > 0));
        }

        //the RadTreeView.ItemsSource collection is traversed to find an item by a provided name
        private void Search(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                searchText = searchTextBox.Text.ToLower();
                foreach (Category category in radTreeView.ItemsSource)
                {
                    if (category.Name.ToLower().Contains(searchText))
                    {
                        RadTreeViewItem item = radTreeView.GetItemByPath(category.Path);
                        item.BringIntoView();
                        item.IsSelected = true;
                        return;
                    }
                    foreach (Product product in category.Products)
                    {
                        if (product.Name.ToLower().Contains(searchText))
                        {
                            RadTreeViewItem item = radTreeView.GetItemByPath(category.Path + "\\" + product.Path);
                            item.BringIntoView();
                            item.IsSelected = true;
                            return;
                        }
                    }
                }
            }

            isFiltered = false;
        }
    }
}
