using System;
using Telerik.Windows.Controls.GanttView;

namespace RecurringTask
{
    public class RecurrenceTask : GanttTask
    {
        public RecurrenceTask(DateTime start, DateTime end, string title)
            : base(start, end, title)
        {
        }
    }
}
