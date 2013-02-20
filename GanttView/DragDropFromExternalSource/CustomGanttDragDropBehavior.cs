using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace DragDropFromExternalSource
{
	public class CustomGanttDragDropBehavior : GanttDragDropBehavior
	{
		protected override bool CanStartDrag(SchedulingDragDropState state)
		{
			return state.IsReorderOperation;
		}
	}
}
