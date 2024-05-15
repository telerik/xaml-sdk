using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.DragDrop;

namespace DragDrop
{
	public class ListBoxDragDropBehavior
	{
		private static bool shouldCancelDrop = false;
		private ListBox _associatedObject;

		/// <summary>
		/// AssociatedObject Property
		/// </summary>
		public ListBox AssociatedObject
		{
			get
			{
				return _associatedObject;
			}
			set
			{
				_associatedObject = value;
			}
		}

		private static Dictionary<ListBox, ListBoxDragDropBehavior> instances;

		static ListBoxDragDropBehavior()
		{
			instances = new Dictionary<ListBox, ListBoxDragDropBehavior>();
		}

		public static bool GetIsEnabled(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsEnabledProperty);
		}

		public static void SetIsEnabled(DependencyObject obj, bool value)
		{
			ListBoxDragDropBehavior behavior = GetAttachedBehavior(obj as ListBox);

			behavior.AssociatedObject = obj as ListBox;

			if (value)
			{
				behavior.Initialize();
			}
			else
			{
				behavior.CleanUp();
			}
			obj.SetValue(IsEnabledProperty, value);
		}

		// Using a DependencyProperty as the backing store for IsEnabled.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsEnabledProperty =
			DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ListBoxDragDropBehavior),
				new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));
		
		private static int lastAdded = 0;

		public static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			SetIsEnabled(dependencyObject, (bool)e.NewValue);
		}

		private static ListBoxDragDropBehavior GetAttachedBehavior(ListBox listBox)
		{
			if (!instances.ContainsKey(listBox))
			{
				instances[listBox] = new ListBoxDragDropBehavior();
				instances[listBox].AssociatedObject = listBox;
			}

			return instances[listBox];
		}

		protected virtual void Initialize()
		{
			this.UnsubscribeFromDragDropEvents();
			this.SubscribeToDragDropEvents();
		}

		protected virtual void CleanUp()
		{
			this.UnsubscribeFromDragDropEvents();
		}

		private void SubscribeToDragDropEvents()
		{
			DragDropManager.AddDragInitializeHandler(this.AssociatedObject, OnDragInitialize);
			DragDropManager.AddGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback);
			DragDropManager.AddDropHandler(this.AssociatedObject, OnDrop);
			DragDropManager.AddDragDropCompletedHandler(this.AssociatedObject, OnDragDropCompleted);
		}

		private void UnsubscribeFromDragDropEvents()
		{
			DragDropManager.RemoveDragInitializeHandler(this.AssociatedObject, OnDragInitialize);
			DragDropManager.RemoveGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback);
			DragDropManager.RemoveDropHandler(this.AssociatedObject, OnDrop);
			DragDropManager.RemoveDragDropCompletedHandler(this.AssociatedObject, OnDragDropCompleted);
		}

		private void OnDragInitialize(object sender, DragInitializeEventArgs e)
		{
			var draggedItem = (sender as ListBox).SelectedItem as MyObject;
			e.AllowedEffects = DragDropEffects.All;
			var data = DragDropPayloadManager.GeneratePayload(null);
			string text = typeof(MyObject).ToString();

			if (draggedItem != null)
			{
				text = draggedItem.Name;
			}

			data.SetData("Text", text);
			data.SetData("DraggedData", draggedItem);

			e.DragVisual = new DragVisual()
			{
				Content = text,
			};

			e.DragVisualOffset = e.RelativeStartPoint;
			e.Data = data;
			shouldCancelDrop = false;
		}

		private void OnGiveFeedback(object sender, Telerik.Windows.DragDrop.GiveFeedbackEventArgs e)
		{
			e.SetCursor(Cursors.Arrow);
			e.Handled = true;
		}

		private void OnDragDropCompleted(object sender, DragDropCompletedEventArgs e)
		{
			var draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");

			if (e.Effects != DragDropEffects.None && !shouldCancelDrop)
			{
				var collection = (sender as ListBox).ItemsSource as IList;
				collection.Remove(draggedItem);
			}
		}

		private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
		{
			var text = DragDropPayloadManager.GetDataFromObject(e.Data, "Text") as string;
			var listBox = sender as ListBox;

			if (text != null && listBox != null && !shouldCancelDrop)
			{
				var result = MessageBox.Show("Do you allow the drop?", "Drag Drop Operation", MessageBoxButton.OKCancel);

				if (result == MessageBoxResult.OK || result == MessageBoxResult.Yes)
				{
					(listBox.ItemsSource as IList).Add(new MyObject() { ID = ListBoxDragDropBehavior.lastAdded++, Name = text });
				}
				else
				{
					shouldCancelDrop = true;
				}
			}
		}
	}
}