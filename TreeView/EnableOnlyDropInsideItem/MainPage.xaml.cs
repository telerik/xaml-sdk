using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.DragDrop;

namespace EnableOnlyDropInsideItem_SL
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            DragDropManager.AddDragOverHandler(xTreeView, OnDragOver, true);
        }

        private void OnDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;
            if (options.DropPosition != Telerik.Windows.Controls.DropPosition.Inside)
            {
                options.DropPosition = Telerik.Windows.Controls.DropPosition.Inside;
                options.UpdateDragVisual();
            }
        }
    }
}
