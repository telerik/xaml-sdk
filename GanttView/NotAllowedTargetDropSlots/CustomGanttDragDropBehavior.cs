using System;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace NotAllowedTargetDropSlots
{
    public class CustomGanttDragDropBehavior : GanttDragDropBehavior
    {
        protected override bool CanDrop(Telerik.Windows.Controls.Scheduling.SchedulingDragDropState state)
        {
            var destionationSlotDateRange = new DateRange(state.DestinationSlot.Start, state.DestinationSlot.End);
            foreach (var range in Constants.LockedRanges)
            {
                if (this.Intersects(destionationSlotDateRange, range))
                {
                    return false;
                }
            }

            return base.CanDrop(state);
        }

        private bool Intersects(DateRange date1, DateRange date2)
        {
            if (date1 != null && date2 != null)
            {
                if (date1.Start >= date2.Start && date1.Start <= date2.End)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new NullReferenceException("'date1' and 'date2' need to be provided.");
            }
        }
    }
}
