using Telerik.Windows.Controls.GanttView;

namespace DirtyTasks
{
    public class MyGanttTask : GanttTask
    {
        public bool IsModified { get; set; }

        protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            this.IsModified = true;
            base.OnPropertyChanged(args);
        }
    }
}
