using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace CustomDragVisual
{
    public class CustomTreeView : RadTreeView
    {
        protected override object CreateDragVisualContent(IList<object> draggedItems)
        {
            string dragVisualContent = "Dragging " + draggedItems.Count + " items";
            TextBlock tBlock = new TextBlock() { Text = dragVisualContent, Margin = new Thickness(5) };
            return tBlock;
        }       
    }
}
