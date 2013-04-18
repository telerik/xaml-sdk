using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Windows.Controls;

namespace MvvmSupport
{
	public class ExampleViewModel : ViewModelBase
	{
		private DateTime periodStart;
		private DateTime periodEnd;
		private IEnumerable<double> data;
		private DateTime visiblePeriodStart;
		private DateTime visiblePeriodEnd;

		[TypeConverter(typeof(DateTimeTypeConverter))]
		public DateTime PeriodStart
		{
			get
			{
				return this.periodStart;
			}
			set
			{
				if (this.periodStart == value)
					return;

				this.periodStart = value;
				this.OnPropertyChanged("PeriodStart");
			}
		}

		[TypeConverter(typeof(DateTimeTypeConverter))]
		public DateTime PeriodEnd
		{
			get
			{
				return this.periodEnd;
			}
			set
			{
				if (this.periodEnd == value)
					return;

				this.periodEnd = value;
				this.OnPropertyChanged("PeriodEnd");
			}
		}

		[TypeConverter(typeof(DateTimeTypeConverter))]
		public DateTime VisiblePeriodStart
		{
			get
			{
				return this.visiblePeriodStart;
			}
			set
			{
				if (this.visiblePeriodStart == value)
					return;

				this.visiblePeriodStart = value;
				this.OnPropertyChanged("VisiblePeriodStart");
			}
		}

		[TypeConverter(typeof(DateTimeTypeConverter))]
		public DateTime VisiblePeriodEnd
		{
			get
			{
				return this.visiblePeriodEnd;
			}
			set
			{
				if (this.visiblePeriodEnd == value)
					return;

				this.visiblePeriodEnd = value;
				this.OnPropertyChanged("VisiblePeriodEnd");
			}
		}

		public IEnumerable<double> Data
		{
			get
			{
				if (data == null)
				{
					Random r = new Random();
					List<double> collection = new List<double>();
					for (DateTime date = PeriodStart; date <= PeriodEnd; date = date.AddDays(1))
					{
						collection.Add(r.Next(0, 100));
					}
					this.data = collection;
				}
				return this.data;
			}
		}
	}
}