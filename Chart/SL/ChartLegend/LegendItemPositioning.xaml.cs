using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace ChartLegend
{
	public partial class LegendItemPositioning : UserControl
	{
		public LegendItemPositioning()
		{
			InitializeComponent();
			this.radChart.ItemsSource = new int[] { 1, 5, 6, 9, 5, 7 };
			this.radChart.DefaultView.ChartLegendPosition = Dock.Bottom;
		}
	}
}
