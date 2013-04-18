using System;
using System.Windows;
using Telerik.Windows.Diagrams.Core;
using Telerik.Windows.Controls;
using System.ComponentModel;

namespace CustomServices
{
	public class MyResizing : ResizingService, INotifyPropertyChanged
	{
		private bool canResizeHeight;
		private bool canResizeWidth;

		public MyResizing(RadDiagram owner)
			: base(owner as IGraphInternal)
		{
			this.CanResizeWidth = true;
			this.CanResizeHeight = true;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public bool CanResizeWidth
		{
			get
			{
				return this.canResizeWidth;
			}
			set
			{
				if (this.canResizeWidth != value)
				{
					this.canResizeWidth = value;
					this.OnPropertyChaged("CanResizeWidth");
				}
			}
		}
		public bool CanResizeHeight
		{
			get
			{
				return this.canResizeHeight;
			}
			set
			{
				if (this.canResizeHeight != value)
				{
					this.canResizeHeight = value;
					this.OnPropertyChaged("CanResizeHeight");
				}
			}
		}

		protected override Point CalculateNewDelta(Point newPoint)
		{
			var newDelta = base.CalculateNewDelta(newPoint);
			return new Point(this.CanResizeWidth ? newDelta.X : 0, this.CanResizeHeight ? newDelta.Y : 0);
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
