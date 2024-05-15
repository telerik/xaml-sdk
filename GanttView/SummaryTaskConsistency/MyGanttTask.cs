using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Telerik.Windows.Controls.GanttView;

namespace SummaryTaskConsistency
{
    public class MyGanttTask : GanttTask
    {
        private GanttTask parent;

        public MyGanttTask()
        {
            var children = this.Children as INotifyCollectionChanged;

            if (children != null)
            {
                children.CollectionChanged += this.OnChildrenCollectionChanged;
            }
        }

        public GanttTask Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                if (this.parent != value)
                {
                    this.parent = value;
                    this.OnPropertyChanged(() => this.Parent);
                }
            }
        }

        private void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Reset)
            {
                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems.OfType<INotifyPropertyChanged>())
                    {
                        item.PropertyChanged -= this.OnChildPropertyChanged;
                    }
                }

                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems.OfType<INotifyPropertyChanged>())
                    {
                        item.PropertyChanged += this.OnChildPropertyChanged;
                    }
                }
            }
            else
            {
                foreach (var item in this.Children.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged += this.OnChildPropertyChanged;
                }
            }
        }

        private void OnChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var taskParent = (sender as MyGanttTask).Parent as MyGanttTask;
            if (e.PropertyName == "Start" && taskParent.IsSummary)
            {
                var minStart = taskParent.Children.OfType<IGanttTask>().Select(t => t.Start).Min();
                taskParent.Start = minStart;
            }
            else if (e.PropertyName == "End")
            {
                var maxEnd = taskParent.Children.OfType<IGanttTask>().Select(t => t.End).Max();
                taskParent.End = maxEnd;
            }
        }
    }
}
