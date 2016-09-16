using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TileView;
using Telerik.Windows.DragDrop;

namespace TileView_ReorderingOfMinimizedTiles
{
	public class CustomTileView : RadTileView
	{
		private RadTileViewItem DraggingCandidate;
		private RadTileViewItem DraggingItem;
		private Point lastMousePosition;
		private Telerik.Windows.DragDrop.DragEventArgs mouseArgs;
		private List<DependencyObject> visualHits;
		internal List<RadTileViewItem> SwappingItems;
		private RadTileViewItem containerToSwap;
		private bool shouldMaximizeDraggedItem;
		private Point currentPosition;
		private Point previousPosition;
		public CustomTileView()
		{
			this.SwappingItems = new List<RadTileViewItem>();
			this.InitializeDragAndDrop();
			this.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), true);
		}

		internal bool IsDraggingInMaximizedMode
		{
			get
			{
				return this.DraggingItem != null;
			}
		}

		private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (this.MaximizedItem != null)
			{
				this.lastMousePosition = e.GetPosition(null);
				var itemUnderMouse = this.GetItemUnderMouse(this.lastMousePosition);
				this.DraggingCandidate = itemUnderMouse as RadTileViewItem ?? itemUnderMouse.ParentOfType<RadTileViewItem>();
			}
		}

		private void InitializeDragAndDrop()
		{
			DragDropManager.RemoveDragInitializeHandler(this, new DragInitializeEventHandler(this.OnDragInitialized));
			DragDropManager.RemoveDragOverHandler(this, new Telerik.Windows.DragDrop.DragEventHandler(this.OnDragOver));
			DragDropManager.RemoveDragDropCompletedHandler(this, new Telerik.Windows.DragDrop.DragDropCompletedEventHandler(this.OnElementDragDropCompleted));
			DragDropManager.RemoveDropHandler(this, new Telerik.Windows.DragDrop.DragEventHandler(this.OnDropCompleted));
			DragDropManager.RemoveGiveFeedbackHandler(this, new Telerik.Windows.DragDrop.GiveFeedbackEventHandler(this.OnGiveFeedback));

			DragDropManager.AddDragInitializeHandler(this, new DragInitializeEventHandler(this.OnDragInitialized));
			DragDropManager.AddDragOverHandler(this, new Telerik.Windows.DragDrop.DragEventHandler(this.OnDragOver));
			DragDropManager.AddDragDropCompletedHandler(this, new Telerik.Windows.DragDrop.DragDropCompletedEventHandler(this.OnElementDragDropCompleted));
			DragDropManager.AddDropHandler(this, new Telerik.Windows.DragDrop.DragEventHandler(this.OnDropCompleted));
			DragDropManager.AddGiveFeedbackHandler(this, new Telerik.Windows.DragDrop.GiveFeedbackEventHandler(this.OnGiveFeedback));
		}

		private void OnDragInitialized(object sender, DragInitializeEventArgs args)
		{
			if (this.DraggingCandidate != null && this.IsItemDraggingEnabled && this.MaximizedItem != null)
			{
				Rect bounds = VisualTreeHelper.GetDescendantBounds(this.DraggingCandidate);
				RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, 96, 96, PixelFormats.Pbgra32);
				DrawingVisual dv = new DrawingVisual();
				using (DrawingContext dc = dv.RenderOpen())
				{
					VisualBrush vb = new VisualBrush(this.DraggingCandidate);
					dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
				}
				rtb.Render(dv);
				PngBitmapEncoder png = new PngBitmapEncoder();
				png.Frames.Add(BitmapFrame.Create(rtb));
				MemoryStream stream = new MemoryStream();
				using (stream)
				{
					png.Save(stream);
					BitmapImage bitmap = new BitmapImage();
					bitmap.BeginInit();
					bitmap.StreamSource = stream;
					bitmap.CacheOption = BitmapCacheOption.OnLoad;
					bitmap.EndInit();
					Border tmpBorder = new Border();
					tmpBorder.Padding = new Thickness(Math.Max(0, (this.DraggingCandidate.ActualWidth - bounds.Width) / 2));
					Image image = new Image() { Source = bitmap, Width = bounds.Width, Height = bounds.Height };
					tmpBorder.Child = image;
					args.DragVisual = tmpBorder;
				}

				args.Handled = true;
			}
		}

		private void OnDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs args)
		{
			if (this.DraggingCandidate != null && this.MaximizedItem != null)
			{
				this.previousPosition = args.GetPosition(Window.GetWindow(this));
				var currentMousePosition = previousPosition;
				if (this == sender && this.DraggingCandidate != null &&
					this.lastMousePosition != currentMousePosition)
				{
					this.DragTiles(args, currentMousePosition);
					args.Handled = true;
				}
			}
		}

		private void OnGiveFeedback(object sender, Telerik.Windows.DragDrop.GiveFeedbackEventArgs args)
		{
			if (this.DraggingCandidate != null && this.MaximizedItem != null)
			{
				args.Handled = true;
				args.UseDefaultCursors = false;
				args.SetCursor(System.Windows.Input.Cursors.Arrow);
			}
		}

		private void OnDropCompleted(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
		{
			if (this.DraggingCandidate != null && this.MaximizedItem != null)
			{
				this.FinishDrag();
				e.Handled = true;
			}
		}

		private void OnElementDragDropCompleted(object sender, DragDropCompletedEventArgs e)
		{
			if (this.DraggingCandidate != null && this.MaximizedItem != null)
			{
				this.FinishDrag();
				e.Handled = true;
			}
		}

		private void DragTiles(Telerik.Windows.DragDrop.DragEventArgs e, Point currentMousePosition)
		{
			if (this.IsItemDraggingEnabled)
			{
				var offsetX = currentMousePosition.X - this.lastMousePosition.X;
				var offsetY = currentMousePosition.Y - this.lastMousePosition.Y;

				if (!this.IsDraggingInMaximizedMode && this.DraggingCandidate != null)
				{
					this.DraggingItem = this.DraggingCandidate;
					this.RaiseEvent(new TileViewDragEventArgs(this.DraggingItem, TileDragStartedEvent, this));
					this.HideDraggingItem();
				}
				else if (this.IsDraggingInMaximizedMode)
				{
					this.lastMousePosition = currentMousePosition;
					this.mouseArgs = e;
					this.MoveDraggingItem(offsetX, offsetY);
				}
			}
		}
		internal void MoveDraggingItem(double offsetX, double offsetY)
		{
			if (this.IsDraggingInMaximizedMode)
			{
				if (this.IsDockingEnabled)
					this.CheckForMaximization();

				var itemToSwap = this.FindItemToSwap();
				if (this.DragMode == TileViewDragMode.Slide)
				{
					this.SwapWithDraggingItem(itemToSwap);
				}
				else
				{
					if (this.containerToSwap != null && this.containerToSwap != itemToSwap)
						this.containerToSwap.IsMouseOverDragging = false;

					this.containerToSwap = itemToSwap;

					if (this.containerToSwap != null)
						this.containerToSwap.IsMouseOverDragging = true;
				}
			}
		}

		private void SwapWithDraggingItem(RadTileViewItem item)
		{
			if (item != null && item != this.MaximizedItem &&
				((currentPosition.Y > this.previousPosition.Y && this.DraggingItem.Position > item.Position) ||
					currentPosition.Y < this.previousPosition.Y && this.DraggingItem.Position < item.Position))
			{
				this.SwappingItems.Clear();
			}
			if (item != null && !this.SwappingItems.Contains(item))
			{
				this.SwappingItems.Clear();
				if (this.DraggingItem.Position != item.Position)
					this.SwappingItems.Add(item);
				
				this.DraggingItem.Position = item.Position;
			}
			currentPosition = this.previousPosition;
		}

		private RadTileViewItem FindItemToSwap()
		{
			this.visualHits = new List<DependencyObject>();
			var pt = this.mouseArgs.GetPosition(this);
			VisualTreeHelper.HitTest(this, null, new HitTestResultCallback(this.MyHitTestResult), new PointHitTestParameters(pt));
			var items = new List<RadTileViewItem>();
			foreach (var vh in this.visualHits)
			{
				RadTileViewItem container = vh.GetParents().OfType<RadTileViewItem>().FirstOrDefault();
				if (container != this.DraggingItem)
				{
					items.Add(container);
				}
			}
			this.visualHits.Clear();
			return items.OfType<RadTileViewItem>().Where(i => i != null && i != this.DraggingItem && i.ParentTileView == this).FirstOrDefault();
		}

		private HitTestResultBehavior MyHitTestResult(HitTestResult result)
		{
			this.visualHits.Add(result.VisualHit);
			// Set the behavior to return visuals at all z-order levels.
			return HitTestResultBehavior.Continue;
		}

		private void TransformDraggingItem(double offsetX, double offsetY)
		{
			var transformation = EnsureDragginItemTransformation().Children[3] as TranslateTransform;
			transformation.Y += offsetY;
			if (this.FlowDirection == System.Windows.FlowDirection.LeftToRight)
				transformation.X += offsetX;
			else
				transformation.X -= offsetX;
		}

		private void ClearTransformDraggingItem()
		{
			var item = this.DraggingItem.FindChildByType<Panel>();
			var transformation = this.EnsureDragginItemTransformation().Children[3] as TranslateTransform;
			transformation.Y = 0;
			transformation.X = 0;
		}

		private TransformGroup EnsureDragginItemTransformation()
		{
			TransformGroup group = this.DraggingItem.RenderTransform as TransformGroup;
			if (group == null || group.Children.Count < 4 ||
				!(group.Children[0] is ScaleTransform) ||
				!(group.Children[1] is SkewTransform) ||
				!(group.Children[2] is RotateTransform) ||
				!(group.Children[3] is TranslateTransform))
			{
				group = new TransformGroup();
				group.Children.Add(new ScaleTransform());
				group.Children.Add(new SkewTransform());
				group.Children.Add(new RotateTransform());
				group.Children.Add(new TranslateTransform());

				this.DraggingItem.RenderTransform = group;
			}
			return group;
		}

		private void HideDraggingItem()
		{
			if (this.DraggingItem != null)
			{
				this.DraggingItem.Opacity = 0;
			}
		}

		private void ShowDraggingItem()
		{
			if (this.DraggingItem != null)
			{
				this.DraggingItem.Opacity = 1;
			}
		}

		private void FinishDrag()
		{
			this.ClearTransformDraggingItem();
			this.DraggingCandidate = null;
			this.mouseArgs = null;
			if (this.IsDraggingInMaximizedMode && this.DraggingItem != null)
			{
				bool isDockingMaximization = this.shouldMaximizeDraggedItem && this.PossibleDockingPosition != null;

				if (this.DragMode == TileViewDragMode.Swap && this.containerToSwap != null)
				{
					this.containerToSwap.IsMouseOverDragging = false;

					if (!isDockingMaximization)
					{
						this.SwappingItems.Add(this.DraggingItem);
						this.SwapWithDraggingItem(this.containerToSwap);
					}
				}

				if (this.ItemContainerGenerator.IndexFromContainer(this.DraggingItem) != -1)
					this.OnTileDragEnded(new TileViewDragEventArgs(this.DraggingItem, TileDragEndedEvent, this));
				if (this.DraggingItem != null)
				{
					this.ShowDraggingItem();
					if (isDockingMaximization)
					{
						this.MinimizedItemsPosition = this.PossibleDockingPosition.Value;
						this.PossibleDockingPosition = null;
						var tmp = this.DraggingItem;
						this.ClearTransformDraggingItem();
						this.DraggingItem = null;
						tmp.TileState = TileViewItemState.Maximized;
					}
					else
					{
						this.ClearTransformDraggingItem();
						this.DraggingItem = null;
					}
				}
			}
			
			this.SwappingItems.Clear();
		}

		private void CheckForMaximization()
		{
			if (this.MaximizeMode == TileViewMaximizeMode.Zero)
				return;

			this.shouldMaximizeDraggedItem = true;
			var position = this.mouseArgs.GetPosition(this);
			if (position.Y < 20)
			{
				this.PossibleDockingPosition = Dock.Bottom;
			}
			else if (position.Y + 20 > this.ActualHeight)
			{
				this.PossibleDockingPosition = Dock.Top;
			}
			else if (position.X < 20)
			{
				this.PossibleDockingPosition = Dock.Right;
			}
			else if (position.X + 20 > this.ActualWidth)
			{
				this.PossibleDockingPosition = Dock.Left;
			}
			else
			{
				this.shouldMaximizeDraggedItem = false;
				this.PossibleDockingPosition = null;
			}
		}

		private FrameworkElement GetItemUnderMouse(Point mousePosition)
		{
			return (FrameworkElement)this.InputHitTest(new Point(mousePosition.X - this.BorderThickness.Left, mousePosition.Y - this.BorderThickness.Top));
		}
	}
}
