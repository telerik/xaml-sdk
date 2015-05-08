using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace ToolBarDragAndDrop_SL
{
    public static partial class ToolBarTrayUtilitiesSL
    {
        private class DragDropInfo
        {
            internal readonly Border DragVisual;
            internal readonly RadToolBar ToolBar;
            internal RadToolBarTray Tray;

            internal DragDropInfo(RadToolBar toolBar, RadToolBarTray tray)
            {
                this.ToolBar = toolBar;
                this.Tray = tray;
                this.DragVisual = new Border();
            }
        }
    }
}