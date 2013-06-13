using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DragDropTreeViewToControls.ViewModels;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;

namespace DragDropTreeViewToControls_SL
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            // Set the items source of the controls:
            allProductsView.ItemsSource = CategoryViewModel.Generate();

            IList wishlistSource = new ObservableCollection<ProductViewModel>();
            foreach (ProductViewModel product in ProductViewModel.Generate(6))
            {
                wishlistSource.Add(product);
            }
            wishlistView.ItemsSource = wishlistSource;

            DragDropManager.AddDragOverHandler(allProductsView, OnItemDragOver);
            DragDropManager.AddDropHandler(allProductsView, OnDrop);
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var data = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            if (data == null) return;
            if (e.Effects != DragDropEffects.None)
            {
                var destinationItem = (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();
                var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

                if (destinationItems != null)
                {
                    int dropIndex = dropDetails.DropIndex >= destinationItems.Count ? destinationItems.Count : dropDetails.DropIndex < 0 ? 0 : dropDetails.DropIndex;
                    this.destinationItems.Insert(dropIndex, data);
                }
            }
        }

        IList destinationItems = null;
        private void OnItemDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var item = (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();
            if (item == null)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }
            var position = GetPosition(item, e.GetPosition(item));
            if (item.Level == 0 && position != DropPosition.Inside)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }
            RadTreeView tree = sender as RadTreeView;
            var draggedData = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if ((draggedData == null && dropDetails == null))
            {
                return;
            }
            if (position != DropPosition.Inside)
            {
                e.Effects = DragDropEffects.All;

                destinationItems = item.Level > 0 ? (IList)item.ParentItem.ItemsSource : (IList)tree.ItemsSource;
                int index = destinationItems.IndexOf(item.Item);
                dropDetails.DropIndex = position == DropPosition.Before ? index : index + 1;
            }
            else
            {
                destinationItems = (IList)item.ItemsSource;
                int index = 0;

                if (destinationItems == null)
                {
                    e.Effects = DragDropEffects.None;
                }
                else
                {
                    e.Effects = DragDropEffects.All;
                    dropDetails.DropIndex = index;
                }
            }

            dropDetails.CurrentDraggedOverItem = item.Item;
            dropDetails.CurrentDropPosition = position;

            e.Handled = true;
        }



        private DropPosition GetPosition(RadTreeViewItem item, Point point)
        {
            double treeViewItemHeight = 24;
            if (point.Y < treeViewItemHeight / 4)
            {
                return DropPosition.Before;
            }
            else if (point.Y > treeViewItemHeight * 3 / 4)
            {
                return DropPosition.After;
            }

            return DropPosition.Inside;
        }
    }
}
