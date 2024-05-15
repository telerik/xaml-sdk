using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ExpandItemsIntoView;

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

            DataContext = new ObservableCollection<MyObject>(from i in Enumerable.Range(0, 100)
                                                             select new MyObject()
                                                             {
                                                                 ID = i,
                                                                 Name = string.Format("Name{0}", i),
                                                                 Items = new ObservableCollection<MyObject>(from j in Enumerable.Range(0, 100)
                                                                                                            select new MyObject()
                                                                                                            {
                                                                                                                ID = j,
                                                                                                                Name = string.Format("Name{0}", j),
                                                                                                                Items = new ObservableCollection<MyObject>(from k in Enumerable.Range(0, 100)
                                                                                                                                                           select new MyObject()
                                                                                                                                                           {
                                                                                                                                                               ID = k,
                                                                                                                                                               Name = string.Format("Name{0}", k)
                                                                                                                                                           })
                                                                                                            })
                                                             });
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var collection = (ObservableCollection<MyObject>)DataContext;

            // Expand and scroll into view particular item. 
            // The tree list in this case will need to find first the path to this item which will traverse all items.
            RadTreeListView1.ExpandItemIntoView(collection[99].Items[99].Items[99]);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var collection = (ObservableCollection<MyObject>)DataContext;

            // Expand and scroll into view "path" of items. No items traversal in this case! 
            RadTreeListView1.ExpandItemIntoView(new[] { collection[99], collection[99].Items[99], collection[99].Items[99].Items[99] });
        }
    }
}
