using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.DragDrop;

namespace DragDropBetweenTreeViews
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            MainViewModel viewModelData = new MainViewModel();

            this.xLocalMachineTree.ItemsSource = viewModelData.LocalMachinePartitions;
            this.xApplicationTree.ItemsSource = viewModelData.Applications;

            DragDropManager.AddDragOverHandler(this.xLocalMachineTree, OnLocalMachineTreeDragOver, true);
            DragDropManager.AddDragOverHandler(this.xApplicationTree, OnApplicationTreeDragOver, true);
            DragDropManager.AddDropHandler(this.xApplicationTree, OnApplicationTreeDrop, true);
        }

        private void OnApplicationTreeDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;
            if (options == null)
                return;

            // The condition after the first OR operator is needed to deny the drop of items in Application File. (sub-items)
            RadTreeViewItem dropTargetItem = options.DropTargetItem;

            var draggedItem = options.DraggedItems.First();         
            if (dropTargetItem == null ||
                (dropTargetItem != null &&
                 options.DropTargetItem.DataContext is Resource &&
                 options.DropPosition == DropPosition.Inside) ||
                draggedItem is PartitionViewModel)
            {
                options.DropAction = DropAction.None;
            }

            options.UpdateDragVisual();
        }

        private void OnApplicationTreeDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;

            if (options == null)
                return;

            MediaFile draggedItem = options.DraggedItems.FirstOrDefault() as MediaFile;
            if (draggedItem == null)
                return;

            RadTreeViewItem dropTargetItem = options.DropTargetItem;
            if (dropTargetItem == null)
                return;

            var dropItemModel = dropTargetItem.DataContext;
            if (dropItemModel == null)
                return;

            var dropTree = sender as RadTreeView;
            if (dropTree != null)
            {
                // Disable drop in Application File.
                if (dropItemModel is Resource && options.DropAction == DropAction.None)
                {
                    e.Handled = true;
                    return;
                }

                // Drop in Application.
                if (dropItemModel is ApplicationViewModel || dropItemModel is Resource)
                {
                    options.DropAction = DropAction.Copy;
                    options.UpdateDragVisual();

                    ApplicationViewModel destinationFolder = null;
                    if (dropItemModel is ApplicationViewModel)
                    {
                        // Dropping inside Application.
                        destinationFolder = dropItemModel as ApplicationViewModel;
                    }
                    else
                    {
                        // Dropping Before or After an Application Resource.
                        destinationFolder = options.DropTargetItem.ParentItem.DataContext as ApplicationViewModel;
                    }

                    if (destinationFolder == null)
                        return;

                    Resource file = new Resource()
                    {
                        ImageFilePath = new System.Windows.Media.Imaging.BitmapImage(new Uri(draggedItem.ImageFilePath, UriKind.RelativeOrAbsolute)),
                        Title = draggedItem.ImageTitle
                    };
                                        
                    destinationFolder.Resources.Add(file);
                }
            }
        }

        // Forbids the local machine tree view to drop anything
        private void OnLocalMachineTreeDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;
            if (options != null)
            {
                options.DropAction = DropAction.None;
                options.UpdateDragVisual();

                e.Handled = true;
            }
        }
    }
}
