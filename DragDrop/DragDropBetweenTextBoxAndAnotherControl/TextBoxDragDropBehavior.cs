using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.DragDrop;

namespace DragDrop
{
	public class TextBoxDragDropBehavior
	{
		private TextBox _associatedObject;
		/// <summary>
		/// AssociatedObject Property
		/// </summary>
		public TextBox AssociatedObject
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

		private static Dictionary<TextBox, TextBoxDragDropBehavior> instances;

		static TextBoxDragDropBehavior()
		{
			instances = new Dictionary<TextBox, TextBoxDragDropBehavior>();
		}

		public static bool GetIsEnabled(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsEnabledProperty);
		}

		public static void SetIsEnabled(DependencyObject obj, bool value)
		{
			TextBoxDragDropBehavior behavior = GetAttachedBehavior(obj as TextBox);

			behavior.AssociatedObject = obj as TextBox;

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
			DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(TextBoxDragDropBehavior),
				new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));
		
		private static int lastAdded = 0;

		public static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			SetIsEnabled(dependencyObject, (bool)e.NewValue);
		}

		private static TextBoxDragDropBehavior GetAttachedBehavior(TextBox listBox)
		{
			if (!instances.ContainsKey(listBox))
			{
				instances[listBox] = new TextBoxDragDropBehavior();
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
			DragDropManager.AddDragInitializeHandler(this.AssociatedObject, OnDragInitialize, true);
			DragDropManager.AddGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback, true);
			DragDropManager.AddDropHandler(this.AssociatedObject, OnDrop, true);
			DragDropManager.AddDragDropCompletedHandler(this.AssociatedObject, OnDragDropCompleted, true);
		}	

		private void UnsubscribeFromDragDropEvents()
		{
			DragDropManager.RemoveDragInitializeHandler(this.AssociatedObject, OnDragInitialize);
			DragDropManager.RemoveGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback);
			DragDropManager.RemoveDropHandler(this.AssociatedObject, OnDrop);
			DragDropManager.RemoveDragDropCompletedHandler(this.AssociatedObject, OnDragDropCompleted);
		}

		private void OnDragDropCompleted(object sender, DragDropCompletedEventArgs e)
		{
			(sender as TextBox).Text = "";
		}

		private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
		{
			var text = DragDropPayloadManager.GetDataFromObject(e.Data, "Text") as string;
			if (text != null)
			{
				(sender as TextBox).Text += text;
			}
		}

		private void OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			e.SetCursor(Cursors.Arrow);
			e.Handled = true;
		}

		private void OnDragInitialize(object sender, DragInitializeEventArgs e)
		{
			var draggedItem = (sender as TextBox).Text;
			e.AllowedEffects = DragDropEffects.All;
			var data = DragDropPayloadManager.GeneratePayload(null);
			string text = typeof(MyObject).ToString();

			if (draggedItem != null)
			{
				text = draggedItem;
			}

			data.SetData("Text", text);

			e.DragVisual = new DragVisual()
			{
				Content = text,
			};

			e.DragVisualOffset = e.RelativeStartPoint;
			e.Data = data;
		}
	}
}
