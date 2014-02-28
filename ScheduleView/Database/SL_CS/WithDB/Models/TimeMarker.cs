using System.Windows.Media;
using Telerik.Windows.Controls;
using ScheduleViewDB.Helpers;

namespace ScheduleViewDB.Web
{
	public partial class TimeMarker : ITimeMarker
	{
		private Brush timeMarkerBrush;

		public Brush TimeMarkerBrush
		{
			get
			{
				if (this.timeMarkerBrush == null)
				{
					this.timeMarkerBrush = SolidColorBrushHelper.FromNameString(this.TimeMarkerBrushName);
				}

				return this.timeMarkerBrush;
			}
			set
			{
				this.TimeMarkerBrushName = (this.timeMarkerBrush as SolidColorBrush).Color.ToString().Substring(1);
				this.timeMarkerBrush = value;
			}
		}

		public bool Equals(ITimeMarker other)
		{
			return this.TimeMarkerName != other.TimeMarkerName;
		}
	}
}
