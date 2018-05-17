using System.Collections.ObjectModel;
using Telerik.Windows.Controls.GanttView;

namespace ConnectToDatabase_WPF
{
    public class CustomGanttTask : GanttTask 
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }

        public CustomGanttTask()
            : base()
        {
            (this.Children as ObservableCollection<IGanttTask>).CollectionChanged += CustomGanttTask_CollectionChanged;
        }       

        private void CustomGanttTask_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (CustomGanttTask task in e.OldItems)
                {
                    task.ParentId = null;
                }
            }

            if (e.NewItems != null)
            {
                foreach (CustomGanttTask task in e.NewItems)
                {
                    task.ParentId = this.Id;
                }
            }            
        }
    }
}
