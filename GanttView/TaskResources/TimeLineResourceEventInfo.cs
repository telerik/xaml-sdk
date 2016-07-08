using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace TaskResources
{
    public class TimeLineResourceEventInfo : EventInfo
    {
        public TimeLineResourceEventInfo(Range<long> timeRange, int index, int intersectingRowsCount, Range<int> intersectingGroupCoordinates, bool isSummary, bool isMilestone)
            : base(timeRange, index, intersectingRowsCount, intersectingGroupCoordinates, isSummary, isMilestone)
        {
        }      
    }
}
