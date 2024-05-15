using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace TimelineIntervalMarkers
{
    public class IntervalEventInfo : SlotInfo
    {
        public IntervalEventInfo(Range<long> timeRange, int index)
            : base(timeRange, index, index)
        {

        }
    }
}
