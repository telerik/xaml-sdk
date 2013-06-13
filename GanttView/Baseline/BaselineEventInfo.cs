using System;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace Baseline
{
	public class BaselineEventInfo : SlotInfo
	{
		private readonly GanttBaselineTask task;

		public BaselineEventInfo(Range<long> timeRange, int index, GanttBaselineTask task)
			: base(timeRange, index, index)
		{
			this.task = task;
		}

		public DateTime StartPlannedDate
		{
			get
			{
				return this.task.StartPlannedDate;
			}
		}

		public DateTime EndPlannedDate
		{
			get
			{
				return this.task.EndPlannedDate;
			}
		}
	}
}
