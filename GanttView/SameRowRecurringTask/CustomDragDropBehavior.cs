using Telerik.Windows.Controls.Scheduling;

namespace SameRowRecurringTask
{
    public class CustomDragDropBehavior : SchedulingDragDropBehavior
    {
        /// <summary>
        /// Determines whether movement via drag/drop is generally allowed for the task related 
        /// with the given SchedulingDragDropState.
        /// </summary>
        /// <param name="state">The SchedulingDragDropState.</param>
        /// <returns>Value indicating whether drag/drop-action can be started.</returns>
        protected override bool CanStartDrag(SchedulingDragDropState state)
        {
            if (state.SourceGroupKey is RecurrenceTask)
            {
                return false;
            }

            return base.CanStartDrag(state);
        }

        /// <summary>
        /// Determines whether drag/drop-action for the task related 
        /// with the given SchedulingDragDropState is valid and can be executed.
        /// </summary>
        /// <param name="state">The SchedulingDragDropState.</param>
        /// <returns>Value indicating whether drag/drop-action is valid.</returns>
        protected override bool CanDrop(SchedulingDragDropState state)
        {
            return true;
        }
    }
}
