using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace CreateChartWithStaticData
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			RadCartesianChart chart = new RadCartesianChart();
			chart.HorizontalAxis = new CategoricalAxis();
			chart.VerticalAxis = new LinearAxis() { Maximum = 100 };
			LineSeries line = new LineSeries();
			line.Stroke = new SolidColorBrush(Colors.Orange);
			line.StrokeThickness = 2;
			line.DataPoints.Add(new CategoricalDataPoint() { Value = 20 });
			line.DataPoints.Add(new CategoricalDataPoint() { Value = 40 });
			line.DataPoints.Add(new CategoricalDataPoint() { Value = 35 });
			line.DataPoints.Add(new CategoricalDataPoint() { Value = 40 });
			line.DataPoints.Add(new CategoricalDataPoint() { Value = 30 });
			line.DataPoints.Add(new CategoricalDataPoint() { Value = 50 });
			chart.Series.Add(line);
			chart.SetValue(Grid.ColumnProperty, 1);
			this.LayoutRoot.Children.Add(chart);
		}
	}
}
