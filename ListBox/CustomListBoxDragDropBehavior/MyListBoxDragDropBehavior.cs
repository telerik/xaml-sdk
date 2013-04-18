using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop.Behaviors;

namespace CustomListBoxDragDropBehavior
{
	public class MyListBoxDragDropBehavior : ListBoxDragDropBehavior
	{
		public override void Drop(DragDropState state)
		{
			var draggedItems = state.DraggedItems.OfType<Customer>().ToList();
			TextBlock textBlock1 = new TextBlock();
			textBlock1.Text = "Dropped item:";
			TextBlock textBlock2 = new TextBlock();
			textBlock2.Text = string.Format("ID: {0}", draggedItems[0].Id);
			TextBlock textBlock3 = new TextBlock();
			textBlock3.Text = string.Format("Name: {0}", draggedItems[0].Name);
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add(textBlock1);
			stackPanel.Children.Add(textBlock2);
			stackPanel.Children.Add(textBlock3);
			RadWindow.Alert(stackPanel);

			base.Drop(state);
		}

		public override void DragDropCompleted(DragDropState state)
		{
			var draggedItems = state.DraggedItems.OfType<Customer>().ToList();
			TextBlock textBlock1 = new TextBlock();
			textBlock1.Text = "Dragged item:";
			TextBlock textBlock2 = new TextBlock();
			textBlock2.Text = string.Format("ID: {0}", draggedItems[0].Id);
			TextBlock textBlock3 = new TextBlock();
			textBlock3.Text = string.Format("Name: {0}", draggedItems[0].Name);
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add(textBlock1);
			stackPanel.Children.Add(textBlock2);
			stackPanel.Children.Add(textBlock3);
			RadWindow.Alert(stackPanel);

			base.DragDropCompleted(state);
		}
	}
}
