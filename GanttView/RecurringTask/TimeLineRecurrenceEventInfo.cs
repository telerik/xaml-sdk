using System;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace RecurringTask
{
	public class TimeLineRecurrenceEventInfo : SlotInfo, IEquatable<TimeLineRecurrenceEventInfo>
	{
		public TimeLineRecurrenceEventInfo(Range<long> timeRange, int index)
			: base(timeRange, index, index)
		{
		}

		public object OriginalEvent { get; set; }

		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimeLineRecurrenceEventInfo);
		}

		public bool Equals(TimeLineRecurrenceEventInfo other)
		{
			return base.Equals(other);
		}
	}
}
