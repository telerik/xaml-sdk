using System;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace ChangeResizeCursorAtRuntime
{
    public class CustomScheduleViewDragDropBehavior : ScheduleViewDragDropBehavior
    {
        public override bool CanResize(DragDropState state)
        {
            var destinationSlot = state.DestinationSlots.First() as Slot;
            var duration = destinationSlot.End - destinationSlot.Start;

            if (duration <= new TimeSpan(0, 30, 0) || duration >= new TimeSpan(2, 0, 1))
            {
#if Silverlight
                this.ResizeCursor = Cursors.Wait;
#else
                this.ResizeCursor = Cursors.No;
#endif
                return false;
            }

            this.ResizeCursor = Cursors.Hand;
            return base.CanResize(state);
        }
    }
}
