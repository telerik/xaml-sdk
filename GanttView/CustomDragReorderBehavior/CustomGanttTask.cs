using Telerik.Windows.Controls.GanttView;

namespace CustomDragReorderBehavior
{
    public class CustomGanttTask : GanttTask
    {
        private bool isDragReorderAllowed;

        public bool IsDragReorderAllowed
        {
            get
            {
                return this.isDragReorderAllowed;
            }

            set
            {
                if (this.isDragReorderAllowed != value)
                {
                    this.isDragReorderAllowed = value;
                    this.OnPropertyChanged(() => this.IsDragReorderAllowed);
                }
            }
        }
    }
}
