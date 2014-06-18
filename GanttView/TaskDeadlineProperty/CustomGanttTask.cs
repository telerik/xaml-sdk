using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GanttView;

namespace TaskDeadlineProperty
{
    public class CustomGanttTask : GanttTask
    {
        protected override bool CheckIsExpired()
        {
            return this.Deadline < this.Start;
        }
    }
}
