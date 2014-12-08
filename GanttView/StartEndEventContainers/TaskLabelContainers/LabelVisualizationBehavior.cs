using System.Collections.Generic;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace StartEndEventContainers
{
    public class LabelVisualizationBehavior : DefaultGanttTimeLineVisualizationBehavior
    {
        protected override IEnumerable<IEventInfo> GetEventInfos(TimeLineVisualizationState state, Telerik.Windows.Core.HierarchicalItem hierarchicalItem)
        {
            foreach (var eventInfo in base.GetEventInfos(state, hierarchicalItem))
            {
                yield return eventInfo;
            }

            var task = hierarchicalItem.SourceItem as GanttTask;
            if (task != null && !task.IsMilestone)
            {
                var roundedStart = state.Rounder.Round(new DateRange(task.Start, task.Start));
                var roundedEnd = state.Rounder.Round(new DateRange(task.End, task.End));
                var offSetTime = 0.3;
                var startLabelRange = new Range<long>(roundedStart.Start.AddDays(-1 - offSetTime).Ticks, roundedStart.Start.AddDays(-offSetTime).Ticks);
                var endLabelRange = new Range<long>(roundedEnd.Start.AddDays(offSetTime).Ticks, roundedEnd.Start.AddDays(1 + offSetTime).Ticks);

                if (startLabelRange.IntersectsWith(state.VisibleTimeRange))
                {
                    yield return new StartLabelEventInfo(startLabelRange, hierarchicalItem.Index)
                    {
                        OriginalEvent = hierarchicalItem.SourceItem
                    };
                }

                if (endLabelRange.IntersectsWith(state.VisibleTimeRange))
                {
                    yield return new EndLabelEventInfo(endLabelRange, hierarchicalItem.Index)
                    {
                        OriginalEvent = hierarchicalItem.SourceItem
                    };
                }
            }
        }
    }
}
