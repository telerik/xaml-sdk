using System;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace RecurringTask
{
	public class TimeLineRecurrenceEventInfo : SlotInfo
    {
        public TimeLineRecurrenceEventInfo(Range<long> timeRange, int index)
            : base(timeRange, index, index)
        {
        }

        public object OriginalEvent { get; set; }
    }
}
