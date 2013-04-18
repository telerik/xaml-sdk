using System.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace TrackBall
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void ChartTrackBallBehavior_TrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
		{
			foreach (DataPointInfo info in e.Context.DataPointInfos)
			{
				info.DisplayHeader = "Custom data point header";
			}

			e.Header = "Sample header";
		}
	}
}