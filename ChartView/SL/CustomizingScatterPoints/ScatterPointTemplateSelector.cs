using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using Telerik.Charting;

namespace CustomizingScatterPoints
{
	public class ScatterPointTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			ScatterDataPoint dataPoint = (ScatterDataPoint)item;
			var series = container as ScatterPointSeries;
			var chart = series.GetVisualParent<RadCartesianChart>();
			if (dataPoint.YValue > 105)
			{
				return chart.Resources["ellipseTemplate"] as DataTemplate;
			}
			else
			{
				return chart.Resources["rectangleTemplate"] as DataTemplate;
			}
		}
	}
}