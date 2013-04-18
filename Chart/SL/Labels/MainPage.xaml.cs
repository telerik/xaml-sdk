using System;
using System.Windows.Controls;
using Telerik.Windows.Controls.Charting;

namespace Labels
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

			DataSeries lineSeries = new DataSeries();
			lineSeries.LegendLabel = "Monthly Sales";
			lineSeries.Definition = new LineSeriesDefinition();
			Random r = new Random(0);
			for (int i = 0; i < 12; i++)
			{
				lineSeries.Add(new DataPoint() { YValue = i + r.Next(0, 20) });
			}

			radChart.DefaultView.ChartArea.DataSeries.Add(lineSeries);

			string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", };
			for (int i = 0; i < months.Length; i++)
			{
				radChart.DefaultView.ChartArea.AxisX.TickPoints[i].Label = months[i];
			}
		}
	}
}
