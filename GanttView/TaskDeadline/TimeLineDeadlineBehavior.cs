using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace TaskDeadline
{
	public class TimeLineDeadlineBehavior : DefaultGanttTimeLineVisualizationBehavior
	{
		protected override IEnumerable<IEventInfo> GetEventInfos(TimeLineVisualizationState state, Telerik.Windows.Core.HierarchicalItem hierarchicalItem)
		{
			foreach (var eventInfo in base.GetEventInfos(state, hierarchicalItem))
			{
				yield return eventInfo;
			}

			var task = hierarchicalItem.SourceItem as GanttDeadlineTask;
			var deadline = task != null ? task.GanttDeadLine : default(DateTime?);

			if (deadline.HasValue)
			{
				var roundedDeadline = state.Rounder.Round(new DateRange(deadline.Value, deadline.Value));
				var deadlineRange = new Range<long>(roundedDeadline.Start.Ticks, roundedDeadline.End.Ticks);

				if (deadlineRange.IntersectsWith(state.VisibleTimeRange))
				{
					yield return new TimeLineDeadlineEventInfo(deadlineRange, hierarchicalItem.Index);
				}
			}
		}
	}
}
