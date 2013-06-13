using System;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Rendering;

namespace Baseline
{
	public class BaselineContainer : Control, IDataContainer
	{
		public static readonly DependencyProperty StartPlannedDateProperty = DependencyProperty.Register("StartPlannedDate", typeof(DateTime), typeof(BaselineContainer), null);

		public static readonly DependencyProperty EndPlannedDateProperty = DependencyProperty.Register("EndPlannedDate", typeof(DateTime), typeof(BaselineContainer), null);

		private object data;

		public BaselineContainer()
		{
			this.DefaultStyleKey = typeof(BaselineContainer);
		}

		public DateTime StartPlannedDate
		{
			get
			{
				return (DateTime)GetValue(StartPlannedDateProperty);
			}

			set
			{
				SetValue(StartPlannedDateProperty, value);
			}
		}

		public DateTime EndPlannedDate
		{
			get
			{
				return (DateTime)GetValue(EndPlannedDateProperty);
			}

			set
			{
				SetValue(EndPlannedDateProperty, value);
			}
		}

		public object DataItem
		{
			get
			{
				return this.data;
			}

			set
			{
				if (this.data != value)
				{
					this.data = value;
					var info = this.data as BaselineEventInfo;
					if (info != null)
					{
						this.StartPlannedDate = info.StartPlannedDate;
						this.EndPlannedDate = info.EndPlannedDate;
					}
				}
			}
		}
	}
}
