using System.Windows.Controls;
using Telerik.Windows.Controls.Charting;

namespace ChartLegend
{
	public partial class LegendItemsShape : UserControl
	{
		public LegendItemsShape()
		{
			InitializeComponent();
			this.radChart.ItemsSource = new int[] { 1, 5, 6, 9, 5, 7 };
			this.radChart.DefaultView.ChartLegend.LegendItemMarkerShape = MarkerShape.StarFiveRay;
		}
	}
}
