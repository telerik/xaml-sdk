using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace RecurringTask
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
            if (!hierarchicalItem.CanExpand)
            {
                return this.GetEventInfosBase(state, index, hierarchicalItem);
            }

            return hierarchicalItem.Children.SelectMany(h => this.GetAllChildren(state, index, h));
        }

        private IEnumerable<IEventInfo> GetEventInfosBase(TimeLineVisualizationState state, int index, HierarchicalItem hierarchicalItem)
        {
            var dateRange = hierarchicalItem.SourceItem as IDateRange;
            var roundedRange = state.Rounder.Round(dateRange);
            var taskRange = new Range<long>(roundedRange.Start.Ticks, roundedRange.End.Ticks);

            if (taskRange.IntersectsWith(state.VisibleTimeRange))
            {
                var eventInfo = new EventInfo(taskRange, index, 1, new Range<int>(0), IsSummary(hierarchicalItem.SourceItem), IsMilestone(hierarchicalItem.SourceItem))
                {
                    OriginalEvent = hierarchicalItem.SourceItem
                };

                yield return eventInfo;
            }
        }

        private static bool IsMilestone(object originalEvent)
        {
            var milestone = originalEvent as IMilestone;
            return milestone != null && milestone.IsMilestone;
        }

        private static bool IsSummary(object originalEvent)
        {
            var summary = originalEvent as ISummary;
            return summary != null && summary.IsSummary;
        }
	}
}
