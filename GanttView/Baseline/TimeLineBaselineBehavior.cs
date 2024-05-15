using System;
using System.Collections.Generic;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace Baseline
{
	public class TimeLineBaselineBehavior : DefaultGanttTimeLineVisualizationBehavior
	{
		protected override IEnumerable<IEventInfo> GetEventInfos(TimeLineVisualizationState state, HierarchicalItem hierarchicalItem)
		{
			foreach (var eventInfo in base.GetEventInfos(state, hierarchicalItem))
			{
				yield return eventInfo;
			}

			var task = hierarchicalItem.SourceItem as GanttBaselineTask;
			var baselineStartDate = task != null ? task.StartPlannedDate : DateTime.MinValue;

			if (baselineStartDate != null && baselineStartDate != DateTime.MinValue)
			{
				var roundedDeadline = state.Rounder.Round(new DateRange(baselineStartDate, task.EndPlannedDate));
				var baselineRange = new Range<long>(roundedDeadline.Start.Ticks, roundedDeadline.End.Ticks);

				if (baselineRange.IntersectsWith(state.VisibleTimeRange))
				{
					yield return new BaselineEventInfo(baselineRange, hierarchicalItem.Index, hierarchicalItem.SourceItem as GanttBaselineTask);
				}
			}
		}
	}
}
