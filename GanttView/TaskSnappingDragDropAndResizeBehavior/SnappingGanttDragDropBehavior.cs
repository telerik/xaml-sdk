using System;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace TaskSnappingDragDropAndResizeBehavior
{
    public class SnappingGanttDragDropBehavior : GanttDragDropBehavior
    {
        protected override void Drop(SchedulingDragDropState state)
        {
            state.DestinationSlot.End = SnappingHelper.RoundUpDateTime(state.DestinationSlot.End);
            state.DestinationSlot.Start = SnappingHelper.RoundUpDateTime(state.DestinationSlot.Start);

            base.Drop(state);
        }
    }
}
