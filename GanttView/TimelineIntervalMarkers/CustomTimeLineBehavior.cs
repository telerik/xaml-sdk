using System;
using System.Collections.Generic;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace TimelineIntervalMarkers
{
    public class CustomTimeLineBehavior : DefaultGanttTimeLineVisualizationBehavior
    {
        private DateRange visibleRange;

        public CustomTimeLineBehavior(DateRange visibleRange)
        {
            this.visibleRange = visibleRange;
        }

        protected override IEnumerable<IEventInfo> GetEventInfos(TimeLineVisualizationState state, HierarchicalItem hierarchicalItem)
        {
            foreach (var eventInfo in base.GetEventInfos(state, hierarchicalItem))
            {
                yield return eventInfo;
            }

            var dateRange = hierarchicalItem.SourceItem as IDateRange;
            var roundedRange = state.Rounder.Round(dateRange);
            var currentDay = new DateTime(roundedRange.Start.Year, this.visibleRange.Start.Month, this.visibleRange.Start.Day);

            while (this.visibleRange.End > currentDay)
            {
                var range = new Range<long>(currentDay.Ticks, currentDay.Ticks);
                currentDay = currentDay.AddDays(1);

                yield return new IntervalEventInfo(range, hierarchicalItem.Index);
            }

            yield return new CurrentHourIndicatorEventInfo(new Range<long>(DateTime.Now.Ticks, DateTime.Now.Ticks), hierarchicalItem.Index);
        }
    }
}
