using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace LockingDragDependenciesBehavior
{
    public class CustomDragDependenciesBehavior : GanttDragDependenciesBehavior
    {
        protected override bool CanStartLink(SchedulingLinkState state)
        {
            if (!base.CanStartLink(state))
            {
                return false;
            }

            return !((LockingTask)state.LinkSourceItem).AreDependenciesLocked;
        }

        protected override bool CanLink(SchedulingLinkState state)
        {
            if (!base.CanLink(state))
            {
                return false;
            }
            return !((LockingTask)state.TargetElementGroupKey).AreDependenciesLocked;
        }
    }
}
