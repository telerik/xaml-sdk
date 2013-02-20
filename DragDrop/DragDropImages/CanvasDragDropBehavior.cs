using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;

namespace DragDrop
{
	public class CanvasDragDropBehavior
	{
		private Canvas _associatedObject;

		/// <summary>
		/// AssociatedObject Property
		/// </summary>
		public Canvas AssociatedObject
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

		private FrameworkElement draggedImage = null;
		private Point relativeStartPoint = new Point(0, 0);

		private static Dictionary<Canvas, CanvasDragDropBehavior> instances;

		static CanvasDragDropBehavior()
		{
			instances = new Dictionary<Canvas, CanvasDragDropBehavior>();
		}

		public static bool GetIsEnabled(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsEnabledProperty);
		}

		public static void SetIsEnabled(DependencyObject obj, bool value)
		{
			CanvasDragDropBehavior behavior = GetAttachedBehavior(obj as Canvas);

			behavior.AssociatedObject = obj as Canvas;

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
			DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(CanvasDragDropBehavior),
				new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));

		public static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			SetIsEnabled(dependencyObject, (bool)e.NewValue);
		}

		private static CanvasDragDropBehavior GetAttachedBehavior(Canvas canvas)
		{
			if (!instances.ContainsKey(canvas))
			{
				instances[canvas] = new CanvasDragDropBehavior();
				instances[canvas].AssociatedObject = canvas;
			}

			return instances[canvas];
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
			DragDropManager.AddDragOverHandler(this.AssociatedObject, OnDragOver);
		}

		private void UnsubscribeFromDragDropEvents()
		{
			DragDropManager.RemoveDragInitializeHandler(this.AssociatedObject, OnDragInitialize);
			DragDropManager.RemoveGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback);
			DragDropManager.RemoveDragOverHandler(this.AssociatedObject, OnDragOver);
		}

		private void OnDragInitialize(object sender, DragInitializeEventArgs e)
		{
			this.draggedImage = e.OriginalSource as Image ?? (e.OriginalSource as FrameworkElement).ParentOfType<Image>();
			
			if (this.draggedImage == null)
			{
				e.Cancel = true;
			}

			this.relativeStartPoint = e.RelativeStartPoint;
		}

		private void OnGiveFeedback(object sender, Telerik.Windows.DragDrop.GiveFeedbackEventArgs e)
		{
			e.SetCursor(Cursors.Arrow);
			e.Handled = true;
		}

		private void OnDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
		{
			var position = e.GetPosition(sender as Canvas);

			Canvas.SetTop(this.draggedImage, position.Y - this.relativeStartPoint.Y);
			Canvas.SetLeft(this.draggedImage, position.X - this.relativeStartPoint.X);
		}
	}
}