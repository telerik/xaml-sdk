using System.Windows;
using System.Data;
using System;
using Telerik.Windows.Controls.ChartView;

namespace BindingToDataTable
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			InitializeComponent();
			DataTable dt = new DataTable();
			dt.Columns.Add("Expected", typeof(double));
			dt.Columns.Add("Actual", typeof(double));
			dt.Columns.Add("Month", typeof(DateTime));

			dt.Rows.Add(30, 28, new DateTime(2013, 1, 1));
			dt.Rows.Add(45, 32, new DateTime(2013, 3, 1));
			dt.Rows.Add(5, 23, new DateTime(2013, 5, 1));
			dt.Rows.Add(10, 7, new DateTime(2013, 7, 1));
			dt.Rows.Add(3, 2, new DateTime(2013, 11, 1));
			dt.Rows.Add(7, 10, new DateTime(2013, 12, 1));

			this.DataContext = dt.Rows;

			this.barSeries1.ValueBinding = new GenericDataPointBinding<DataRow, double>()
			{
				ValueSelector = row => (double)row["Actual"]
			};

			this.barSeries1.CategoryBinding = new GenericDataPointBinding<DataRow, DateTime>()
			{
				ValueSelector = row => (DateTime)row["Month"]
			};

			this.barSeries2.ValueBinding = new GenericDataPointBinding<DataRow, double>()
			{
				ValueSelector = row => (double)row["Expected"]
			};

			this.barSeries2.CategoryBinding = new GenericDataPointBinding<DataRow, DateTime>()
			{
				ValueSelector = row => (DateTime)row["Month"]
			};
		}
	}
}