using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TreeListViewBringItem
{
    public partial class Example : UserControl
    {
        DateTime listBringStart;
        private ObservableCollection<DataItem> list;

        public Example()
        {
            InitializeComponent();
            this.LoadData();
        }

        private void LoadData()
        {
            list = new ObservableCollection<DataItem>();
            for (int i = 0; i < 100; i++)
            {
                DataItem root = new DataItem() { Name = "Item " + i };
                list.Add(root);
            }
            this.treeList.ItemsSource = list;
        }

        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.listBringStart = DateTime.Now;
            this.ScrollIntoViewRecursive(0, list[85]);
        }

        private void ScrollIntoViewRecursive(int level, DataItem item)
        {
            if (level >= 20)
            {
                MessageBox.Show(DateTime.Now.Subtract(this.listBringStart).TotalSeconds.ToString() + " sec.");
                return;
            }
            var newItem = item.Children[85];
            this.treeList.ScrollIntoViewAsync(item, (f) => { ScrollIntoViewRecursive(++level, newItem); }, true);
        }

        private void RadButton_Click_2(object sender, RoutedEventArgs e)
        {
            DateTime startTime = DateTime.Now;

            DataItem start = this.list[85];
            for (int i = 0; i < 20; i++)
            {
                this.treeList.ExpandHierarchyItem(start);
                start = start.Children[85];
            }
            this.treeList.ScrollIntoView(start, false);
            this.treeList.SelectedItems.Add(start);
            DateTime end = DateTime.Now;
            MessageBox.Show(end.Subtract(startTime).TotalSeconds.ToString() + " sec.");
        }
    }
}
