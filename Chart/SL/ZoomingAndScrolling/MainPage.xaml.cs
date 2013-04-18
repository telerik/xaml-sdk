using System.Windows.Controls;
using Telerik.Windows.Controls.Charting;

namespace ZoomingAndScrolling
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = ChartData.GetSampleData();

			this.radChart.DefaultView.ChartArea.ZoomScrollSettingsX.MinZoomRange = 0.1;
			this.radChart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd = 0.3;
			this.radChart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeStart = 0.2;
			this.radChart.DefaultView.ChartArea.ZoomScrollSettingsX.ScrollMode = ScrollMode.ScrollAndZoom;
		}
	}
}
