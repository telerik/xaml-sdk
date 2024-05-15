using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace DragDropAndResizeCustomContainers
{
    public class TimeLineCustomBehavior : DefaultGanttTimeLineVisualizationBehavior
    {
        protected override IEnumerable<IEventInfo> GetEventInfos(TimeLineVisualizationState state, Telerik.Windows.Core.HierarchicalItem hierarchicalItem)
        {
            var task = hierarchicalItem.SourceItem as CustomGanttTask;
            foreach (var eventInfo in base.GetEventInfos(state, hierarchicalItem))
            {
                // Here we remove the creation of the default EventContainers
                if(task.IsSummary || task.IsMilestone || eventInfo.GetType() != typeof(EventInfo))
                {
                    yield return eventInfo;
                }                
            }

            var plannedStartDate = task != null ? task.Start : DateTime.MinValue;
            if (plannedStartDate != null && plannedStartDate != DateTime.MinValue)
            {
                var roundedDeadline = state.Rounder.Round(new DateRange(plannedStartDate, task.End));
                var palnnedRange = new Range<long>(roundedDeadline.Start.Ticks, roundedDeadline.End.Ticks);

                if (palnnedRange.IntersectsWith(state.VisibleTimeRange))
                {
                  if(!task.IsSummary && !task.IsMilestone)
                    yield return new TimeLineCustomEventInfo(palnnedRange, hierarchicalItem.Index, hierarchicalItem.SourceItem as CustomGanttTask);
                }
            }
        }
    }
}
