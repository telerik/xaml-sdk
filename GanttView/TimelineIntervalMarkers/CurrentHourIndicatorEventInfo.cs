using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace TimelineIntervalMarkers
{
    public class CurrentHourIndicatorEventInfo : SlotInfo
    {
        public CurrentHourIndicatorEventInfo(Range<long> timeRange, int index)
            : base(timeRange, index, index)
        {

        }
    }
}
