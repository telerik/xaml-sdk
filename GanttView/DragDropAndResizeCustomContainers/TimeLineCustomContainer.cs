using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Rendering;

namespace DragDropAndResizeCustomContainers
{
    public class TimeLineCustomContainer : Control, IDataContainer
    {
		public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register("StartDate", typeof(DateTime), typeof(TimeLineCustomContainer), null);
		public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register("EndDate", typeof(DateTime), typeof(TimeLineCustomContainer), null);
		public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("ColorProperty", typeof(Brush), typeof(TimeLineCustomContainer), null);

		public TimeLineCustomContainer()
		{
			this.DefaultStyleKey = typeof(TimeLineCustomContainer);
		}

		public DateTime StartDate
		{
			get { return (DateTime)GetValue(StartDateProperty); }
			set { SetValue(StartDateProperty, value); }
		}

		public DateTime EndDate
		{
			get { return (DateTime)GetValue(EndDateProperty); }
			set { SetValue(EndDateProperty, value); }
		}

		public Brush Color
		{
			get { return (Brush)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}

		private object data;
		public object DataItem
		{
			get { return this.data; }
			set
			{
				if (this.data != value)
				{
					this.data = value;
					var info = this.data as TimeLineCustomEventInfo;
					if (info != null)
					{
						this.StartDate = info.Start;
						this.EndDate = info.End;
						this.Color = info.Color;
					}
				}
			}
		}
	}
}
