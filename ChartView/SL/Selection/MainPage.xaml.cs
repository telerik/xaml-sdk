using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace Selection
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			var series = RadChart1.Series[0] as ScatterPointSeries;
			series.XValueBinding = new PropertyNameDataPointBinding("XValue");
			series.YValueBinding = new PropertyNameDataPointBinding("YValue");
			series.ItemsSource = this.GetData();

			var series2 = RadChart2.Series[0] as LineSeries;
			series2.CategoryBinding = new PropertyNameDataPointBinding("XValue");
			series2.ValueBinding = new PropertyNameDataPointBinding("YValue");
			series2.ItemsSource = this.GetData();
		}

		private List<ChartData> GetData()
		{
			Random rand = new Random(0);
			List<ChartData> data = new List<ChartData>();

			for (int i = 1; i < 30; i++)
			{
				data.Add(new ChartData() { XValue = i, YValue = rand.Next(10, 100) });
			}

			return data;
		}

		private void RadChart1_SelectionChanged(object sender, ChartSelectionChangedEventArgs e)
		{
			//Handle selection of the current point
			if (e.AddedPoints.Count > 0)
			{
				// Get the data point.
				var addedPoint = e.AddedPoints[0];
				// Get the series.
				var series = addedPoint.Presenter as ScatterPointSeries;

				//Your code here...
			}

			//Handle de-selection of the current point
			if (e.RemovedPoints.Count > 0)
			{
				// Get the data point.
				var removedPoint = e.RemovedPoints[0];
				// Get the series.
				var series = removedPoint.Presenter as ScatterPointSeries;

				//Your code here...
			}
		}

		private void RadChart1_LineSeriesSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
		{
			//Handle selection of the current point
			if (e.AddedPoints.Count > 0)
			{
				var addedPoint = e.AddedPoints[0];
				var series = addedPoint.Presenter as LineSeries;

				//Get the Content Presenter of the series
				var pointPresenter = series.
					ChildrenOfType<ContentPresenter>().
					Where(cp => cp.Tag == addedPoint).FirstOrDefault();
				var ellipseElement = pointPresenter.
					ChildrenOfType<Ellipse>().FirstOrDefault();

				//Do whatever you want with it :)
				ellipseElement.Fill = new SolidColorBrush(Colors.Red);
			}

			//Handle de-selection of the current point
			if (e.RemovedPoints.Count > 0)
			{
				var removedPoint = e.RemovedPoints[0];
				var series = removedPoint.Presenter as LineSeries;
				var pointPresenter = series.
					ChildrenOfType<ContentPresenter>().
					Where(cp => cp.Tag == removedPoint).FirstOrDefault();
				var ellipseElement = pointPresenter.
					ChildrenOfType<Ellipse>().FirstOrDefault();

				//Do whatever you want with it :)
				ellipseElement.Fill = new SolidColorBrush(Colors.Yellow);
			}
		}
	}
}