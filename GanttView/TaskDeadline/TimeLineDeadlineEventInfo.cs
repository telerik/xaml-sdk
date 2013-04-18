using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace TaskDeadline
{
	public class TimeLineDeadlineEventInfo : SlotInfo
	{
		public TimeLineDeadlineEventInfo(Range<long> timeRange, int index)
			: base(timeRange, index, index)
		{
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimeLineDeadlineEventInfo);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
