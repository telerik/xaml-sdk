using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace ToolBarDragAndDrop
{
    public static partial class ToolBarTrayUtilities
    {
        private class DragDropInfo
        {
            internal readonly Border DragVisual;
            internal readonly RadToolBar ToolBar;
            internal readonly RadToolBarTray Tray;

            internal DragDropInfo(RadToolBar toolBar, RadToolBarTray tray)
            {
                this.ToolBar = toolBar;
                this.Tray = tray;
                this.DragVisual = new Border();
            }
        }
    }
}