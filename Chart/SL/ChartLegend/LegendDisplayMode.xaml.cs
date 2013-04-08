using System.Windows.Controls;

namespace ChartLegend
{
	public partial class LegendDisplayMode : UserControl
	{
		public LegendDisplayMode()
		{
			InitializeComponent();
			this.radChart.ItemsSource = new int[] { 1, 5, 6, 9, 5, 7 };
			this.radChart.DefaultSeriesDefinition.LegendDisplayMode = Telerik.Windows.Controls.Charting.LegendDisplayMode.DataPointLabel;
			
		}
	}
}
