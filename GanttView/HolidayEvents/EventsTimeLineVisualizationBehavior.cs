using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace HolidayEvents
{
    public class EventsTimeLineVisualizationBehavior : DefaultGanttTimeLineVisualizationBehavior
    {
        private List<DateRange> globalDeadLines;

        public List<DateRange> GlobalDeadLines
        {
            get
            {
                return this.globalDeadLines;
            }

            set
            {
                if (this.globalDeadLines != value)
                {
                    this.globalDeadLines = value;
                    this.OnPropertyChanged(() => this.GlobalDeadLines);
                }
            }
        }

        protected override IEnumerable GetBackgroundData(TimeLineVisualizationState state)
        {
            foreach (var background in base.GetBackgroundData(state))
            {
                yield return background;
            }

            var visibleRange = state.VisibleTimeRange;
            foreach (var deadLine in GlobalDeadLines)
            {
                var deadline = state.Rounder.Round(new DateRange(deadLine.Start, deadLine.End));
                var deadlineRange = new Range<long>(deadline.Start.Ticks, deadline.End.Ticks);

                if (visibleRange.IntersectsWith(deadlineRange))
                {
                    yield return new GlobalEventTimeSlotInfo(deadlineRange);
                }
            }
        }

        protected override IEnumerable<IEventInfo> GetEventInfos(TimeLineVisualizationState state, Telerik.Windows.Core.HierarchicalItem hierarchicalItem)
        {
            foreach (var eventInfo in base.GetEventInfos(state, hierarchicalItem))
            {
                yield return eventInfo;
            }

            var task = hierarchicalItem.SourceItem as CustomGanttTask;
            foreach (var deadLine in task.CustomDeadLines)
            {
                if (deadLine != null)
                {
                    var roundedDeadline = state.Rounder.Round(deadLine);
                    var deadlineRange = new Range<long>(roundedDeadline.Start.Ticks, roundedDeadline.End.Ticks);

                    if (deadlineRange.IntersectsWith(state.VisibleTimeRange))
                    {
                        yield return new NationalEventSlotInfo(deadlineRange, hierarchicalItem.Index);
                    }
                }
            }
        }
    }
}
