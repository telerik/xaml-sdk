using Telerik.Windows.Controls.GanttView;

namespace LockingDragDependenciesBehavior
{
    public class LockingTask : GanttTask
    {
        private bool areDependenciesLocked;

        public bool AreDependenciesLocked
        {
            get
            {
                return areDependenciesLocked;
            }
            set
            {
                if (this.areDependenciesLocked != value)
                {
                    this.areDependenciesLocked = value;
                    this.OnPropertyChanged(() => this.AreDependenciesLocked);
                }

            }

        }
    }
}
