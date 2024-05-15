using System.Collections.Generic;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace TaskResources
{
    public class ResourceTimeLineVisualizationBehavior : DefaultGanttTimeLineVisualizationBehavior
    {
        protected override IEnumerable<IEventInfo> GetEventInfos(TimeLineVisualizationState state, Telerik.Windows.Core.HierarchicalItem hierarchicalItem)
        {
            foreach (var eventInfo in base.GetEventInfos(state, hierarchicalItem))
            {
                yield return eventInfo;
            }

            var task = hierarchicalItem.SourceItem as GanttResourceTask;
        
            yield return new TimeLineResourceEventInfo(new Range<long>(task.End.Ticks, task.End.AddYears(2).Ticks), hierarchicalItem.Index, 1, new Range<int>(0), IsSummary(hierarchicalItem.SourceItem), IsMilestone(hierarchicalItem.SourceItem))
            {
                OriginalEvent = hierarchicalItem.SourceItem
            };
        }

        private static bool IsMilestone(object originalEvent)
        {
            var milestone = originalEvent as IMilestone;
            return milestone != null && milestone.IsMilestone;
        }

        private static bool IsSummary(object originalEvent)
        {
            var milestone = originalEvent as ISummary;
            return milestone != null && milestone.IsSummary;
        }
    }
}
