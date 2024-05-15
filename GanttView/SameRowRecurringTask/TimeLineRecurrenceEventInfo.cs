using System;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace SameRowRecurringTask
{
	public class TimeLineRecurrenceEventInfo : EventInfo
    {
        public TimeLineRecurrenceEventInfo(Range<long> timeRange, int index)
            : base(timeRange, index, 0, null)
        {
        }
    }
}
