using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace DateTimeAxes
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			InitializeComponent();

			DateTime lastDate = new DateTime(2011, 11, 15);
			double lastVal = 20;

			List<ChartDataObject> dataSouce = new List<ChartDataObject>();
			for (int i = 0; i < 5; ++i)
			{
				ChartDataObject obj = new ChartDataObject { Date = lastDate.AddMonths(1), Value = lastVal++ };
				dataSouce.Add(obj);
				lastDate = obj.Date;
			}
			LineSeries series = (LineSeries)this.chart1.Series[0];
			series.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Date" };
			series.ValueBinding = new PropertyNameDataPointBinding() { PropertyName = "Value" };
			series.ItemsSource = dataSouce;

			series = (LineSeries)this.chart2.Series[0];
			series.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Date" };
			series.ValueBinding = new PropertyNameDataPointBinding() { PropertyName = "Value" };
			series.ItemsSource = dataSouce;
		}
	}
}