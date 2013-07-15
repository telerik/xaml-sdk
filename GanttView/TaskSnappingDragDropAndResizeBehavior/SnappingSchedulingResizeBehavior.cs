using System;
using Telerik.Windows.Controls.Scheduling;

namespace TaskSnappingDragDropAndResizeBehavior
{
    public class SnappingSchedulingResizeBehavior : SchedulingResizeBehavior
    {
        protected override void Resize(SchedulingResizeState state)
        {
            if (state.IsResizeFromEnd)
            {
                state.DestinationSlot.End = SnappingHelper.RoundUpDateTime(state.DestinationSlot.End);
            }
            else
            {
                state.DestinationSlot.Start = SnappingHelper.RoundUpDateTime(state.DestinationSlot.Start);
            }

            base.Resize(state);
        }
    }
}
