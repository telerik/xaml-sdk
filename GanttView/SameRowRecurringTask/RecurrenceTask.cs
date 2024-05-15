using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.GanttView;

namespace SameRowRecurringTask
{
    public class RecurrenceTask : GanttTask
    {
        public RecurrenceTask(DateTime start, DateTime end, string title)
            : base(start, end, title)
        {
            this.Recurrences = new ObservableCollection<GanttTask>();
        }

        public IList<GanttTask> Recurrences 
        { 
            get;
            private set;
        }
    }
}
