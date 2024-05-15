using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace CustomDragReorderBehavior
{
    public class CustomGanttDragDropBehavior : GanttDragDropBehavior
    {
        protected override bool CanStartDrag(SchedulingDragDropState state)
        {
            if (state.IsReorderOperation)
            {
                return ((CustomGanttTask)state.DraggedItem).IsDragReorderAllowed;
            }

            return base.CanStartDrag(state);
        }
    }
}
