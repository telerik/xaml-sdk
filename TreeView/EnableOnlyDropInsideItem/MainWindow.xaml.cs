using EnableOnlyDropInsideItem.ViewModels;
using System;
using System.Collections;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.DragDrop;

namespace EnableOnlyDropInsideItem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DragDropManager.AddDragOverHandler(xTreeView, OnDragOver,true);
        }

        private void OnDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;
            var draggedItem = options.DraggedItems.ElementAt(0);
            var dropItem = options.DropTargetItem.Item;
            var dropItemChildrenType = GetChildCollectionType(dropItem);
            var dragItemType = draggedItem.GetType();
            var dropItemType = dropItem.GetType();

            if ((dragItemType == dropItemType &&
                dropItemChildrenType != dragItemType &&
                options.DropPosition == Telerik.Windows.Controls.DropPosition.Inside) ||
                (dragItemType != dropItemChildrenType && dragItemType != dropItemType))
            {
                options.DropAction = DropAction.None;
                options.UpdateDragVisual();
            }
        }

        private static Type GetChildCollectionType(object dataItem)
        {
            if (dataItem is League)
            {
                return typeof(Division);
            }
            else if (dataItem is Division)
            {
                return typeof(Team);
            }

            return null;
        }
    }
}
