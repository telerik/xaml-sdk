using System;
using System.Collections.Generic;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace RecurringTask
{
	public class TimeLineRecurrenceBehavior : DefaultGanttTimeLineVisualizationBehavior
	{
		protected override IEnumerable<IEventInfo> GetEventInfos(TimeLineVisualizationState state, HierarchicalItem hierarchicalWrapper)
		{
			if (!(hierarchicalWrapper.SourceItem is CustomRecurrenceTask))
			{
				foreach (var info in base.GetEventInfos(state, hierarchicalWrapper))
				{
					yield return info;
				}
			}

			var dateRange = hierarchicalWrapper.SourceItem as IDateRange;
			var roundedRange = state.Rounder.Round(dateRange);
			var taskRange = new Range<long>(roundedRange.Start.Ticks, roundedRange.End.Ticks);
			var task = hierarchicalWrapper.SourceItem as CustomRecurrenceTask;
			Range<long> range = null;

			if (task != null && task.RecurrenceRule != null)
			{
				for (int i = 0; i < task.RecurrenceRule.OcurrenceCount; i++)
				{
					var recurrence = state.Rounder.Round(this.GetRecurrence(task, i));
					range = new Range<long>(recurrence.Start.Ticks, recurrence.End.Ticks);

					yield return new TimeLineRecurrenceEventInfo(range, hierarchicalWrapper.Index)
					{
						OriginalEvent = recurrence
					};
				}
			}
		}

		private CustomRecurrenceTask GetRecurrence(IGanttTask task, int index)
		{
			CustomRecurrenceTask taskToReturn = null;
			var recurrenceTask = task as CustomRecurrenceTask;

			if (recurrenceTask != null && recurrenceTask.RecurrenceRule != null)
			{
				var start = new DateTime(recurrenceTask.RecurrenceRule.Start.Ticks + index * recurrenceTask.RecurrenceRule.Interval.Ticks);
				var end = start + (recurrenceTask.End - recurrenceTask.Start);

				taskToReturn = new CustomRecurrenceTask(start, end, "recurrence " + (index + 1).ToString());
			}
			return taskToReturn;
		}
	}
}
