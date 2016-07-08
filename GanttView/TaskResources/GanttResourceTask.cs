using Telerik.Windows.Controls.GanttView;

namespace TaskResources
{
    public class GanttResourceTask : GanttTask
    {
        private GanttResource _ganttResource;

        public GanttResource GanttResource
        {
            get
            {
                return this._ganttResource;
            }
            set
            {
                if (this._ganttResource != value)
                {
                    this._ganttResource = value;
                    this.OnPropertyChanged(() => this.GanttResource);
                }
            }
        }
    }

  
}

