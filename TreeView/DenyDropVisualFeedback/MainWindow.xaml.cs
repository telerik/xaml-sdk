using System;
using System.Linq;
using System.Windows;
using DenyDropVisualFeedback.ViewModels;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.DragDrop;

namespace DenyDropVisualFeedback_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DragDropManager.AddDragOverHandler(xTreeView, OnDragOverTree, true);
        }
        private void OnDragOverTree(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;
            if (options != null && options.DropPosition == Telerik.Windows.Controls.DropPosition.Inside && options.DropTargetItem != null && options.DropTargetItem.Item is Division)
            {
                options.DropAction = DropAction.None;
                var dragVisual = options.DragVisual as TreeViewDragVisual;
                if (dragVisual != null)
                {
                    dragVisual.IsDropPossible = false;
                    dragVisual.DropActionText = "Cannot drop into ";
                } 
            }
        }
    }
}