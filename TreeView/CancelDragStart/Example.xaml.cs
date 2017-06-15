using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.DragDrop;

namespace CancelDragStart
{
    public partial class Example : UserControl
    {
        private static Random rand = new Random();

        public Example()
        {
            InitializeComponent();
            this.DataContext = this.GenerateData();
            DragDropManager.AddDragInitializeHandler(this.radTreeView, OnTreeViewDragInitialize, true);
        }

        private void OnTreeViewDragInitialize(object sender, DragInitializeEventArgs e)
        {
            var options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;
            if (options != null)
            {
                bool canDrag = options.DraggedItems.OfType<DataItem>().Any(x => x.CanDrag);
                if (!canDrag)
                {
                    e.Data = null;
                    e.DragVisual = null;
                }
            }
        }

        private ObservableCollection<DataItem> GenerateData()
        {
            var source = new ObservableCollection<DataItem>();
            for (int i = 0; i < 5; i++)
            {
                DataItem item = new DataItem();
                item.Children = new ObservableCollection<DataItem>();
                item.Header = "Item " + i;
                item.CanDrag = rand.Next(2) == 0;

                for (int k = 0; k < 3; k++)
                {
                    DataItem child = new DataItem();
                    child.Header = "Item " + i + "." + k;
                    child.CanDrag = rand.Next(2) == 0;
                    item.Children.Add(child);
                }

                source.Add(item);
            }

            return source;
        }
    }
}
