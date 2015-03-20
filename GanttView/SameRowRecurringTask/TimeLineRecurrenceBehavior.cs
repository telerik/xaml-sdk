using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace SameRowRecurringTask
{
	public class TimeLineRecurrenceBehavior : DefaultGanttTimeLineVisualizationBehavior
	{
        protected override IEnumerable<IEventInfo> GetEventInfos(TimeLineVisualizationState state, HierarchicalItem hierarchicalItem)
        {
            if (hierarchicalItem.SourceItem is RecurrenceTask)
            {
                if (!hierarchicalItem.IsExpanded)
                {
                    return this.GetAllChildren(state, hierarchicalItem.Index, hierarchicalItem);
                }

                return Enumerable.Empty<IEventInfo>();
            }

            return this.GetTasks(state, hierarchicalItem);
        }


        private IEnumerable<IEventInfo> GetTasks(TimeLineVisualizationState state, HierarchicalItem hierarchicalItem)
        {
            foreach (var eventInfo in base.GetEventInfos(state, hierarchicalItem))
            {
                yield return eventInfo;
            }
        }

        private IEnumerable<IEventInfo> GetAllChildren(TimeLineVisualizationState state, int index, HierarchicalItem hierarchicalItem)
        {
            var recurringTask = hierarchicalItem.SourceItem as RecurrenceTask;
            if (recurringTask != null)
            {
                foreach (var recurrence in recurringTask.Recurrences)
                {
                    var roundedRecurrence = state.Rounder.Round(new DateRange(recurrence.Start, recurrence.End));
                    var range = new Range<long>(roundedRecurrence.Start.Ticks, roundedRecurrence.End.Ticks);

                    yield return new TimeLineRecurrenceEventInfo(range, hierarchicalItem.Index) { OriginalEvent = recurrence };
                }
            }
        }
	}
}
