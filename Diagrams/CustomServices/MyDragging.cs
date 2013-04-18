using System;
using System.Windows;
using Telerik.Windows.Diagrams.Core;
using System.Linq;
using Telerik.Windows.Controls;
using System.ComponentModel;

namespace CustomServices
{
	public class MyDragging : DraggingService, INotifyPropertyChanged
	{
		private readonly RadDiagram diagram;
		private Point lastPoint;
		private bool isRestrictedToBounds;
		private bool isOn;
		private bool useRotaitonBounds;

		public MyDragging(RadDiagram graph)
			: base(graph as IGraphInternal)
		{
			this.DragAllowedArea = Rect.Empty;
			this.diagram = graph;
			this.IsOn = true;
			this.UseRotaitonBounds = true;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public Rect DragAllowedArea { get; set; }
		public bool IsRestrictedToBounds
		{
			get
			{
				return this.isRestrictedToBounds;
			}
			set
			{
				if (this.isRestrictedToBounds != value)
				{
					this.isRestrictedToBounds = value;
					this.OnPropertyChaged("IsRestrictedToBounds");
				}
			}
		}
		public bool UseRotaitonBounds
		{
			get
			{
				return this.useRotaitonBounds;
			}
			set
			{
				if (this.useRotaitonBounds != value)
				{
					this.useRotaitonBounds = value;
					this.OnPropertyChaged("UseRotaitonBounds");
				}
			}
		}
		public bool IsOn
		{
			get
			{
				return this.isOn;
			}
			set
			{
				if (this.isOn != value)
				{
					this.isOn = value;
					this.OnPropertyChaged("IsOn");
				}
			}
		}

		public override void InitializeDrag(Point point)
		{
			this.lastPoint = point;
			base.InitializeDrag(point);
		}

		public override void Drag(Point newPoint)
		{
			Point dragPoint = newPoint;

			if (this.IsOn)
			{
				if (this.IsRestrictedToBounds)
				{

					var selectionBounds = this.GetSelectionBounds();
					var offset = new Vector(newPoint.X - this.lastPoint.X, newPoint.Y - this.lastPoint.Y);
					var newBounds = new Rect(selectionBounds.X + offset.X, selectionBounds.Y + offset.Y, selectionBounds.Width, selectionBounds.Height);

					if (this.DragAllowedArea == Rect.Empty || this.DragAllowedArea.Contains(newBounds))
					{
						base.Drag(newPoint);
						this.lastPoint = dragPoint;
						return;
					}

					if (this.DragAllowedArea.Left > newBounds.Left)
						dragPoint = new Point(dragPoint.X - (newBounds.Left - this.DragAllowedArea.Left), dragPoint.Y);
					else if (this.DragAllowedArea.Right < newBounds.Right)
						dragPoint = new Point(dragPoint.X - (newBounds.Right - this.DragAllowedArea.Right), dragPoint.Y);

					if (this.DragAllowedArea.Top > newBounds.Top)
						dragPoint = new Point(dragPoint.X, dragPoint.Y - (newBounds.Top - this.DragAllowedArea.Top));
					else if (this.DragAllowedArea.Bottom < newBounds.Bottom)
						dragPoint = new Point(dragPoint.X, dragPoint.Y - (newBounds.Bottom - this.DragAllowedArea.Bottom));
				}
				else
				{
					if (this.DragAllowedArea != Rect.Empty && !this.DragAllowedArea.Contains(newPoint))
					{
						double X = dragPoint.X;
						double Y = dragPoint.Y;
						if (X > this.DragAllowedArea.Right)
							X = this.DragAllowedArea.Right;
						else if (X < this.DragAllowedArea.Left)
							X = this.DragAllowedArea.Left;

						if (Y > this.DragAllowedArea.Bottom)
							Y = this.DragAllowedArea.Bottom;
						else if (Y < this.DragAllowedArea.Top)
							Y = this.DragAllowedArea.Top;

						dragPoint = new Point(X, Y);
					}
				}
			}

			base.Drag(dragPoint);
			this.lastPoint = dragPoint;
		}

		private Rect GetSelectionBounds()
		{
			if (this.UseRotaitonBounds)
			{
				Rect result = Rect.Empty;
				foreach (var item in this.diagram.SelectedItems)
				{
					var container = this.diagram.ContainerGenerator.ContainerFromItem(item);
					var shape = item as IShape;
					if (shape != null)
						result.Union(shape.ActualBounds);
					else
						result.Union(container.Bounds);
				}

				return result;
			}
			else
			{
				return this.diagram.SelectionBounds;
			}
		}

		private void OnPropertyChaged(string name)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}
	}
}
