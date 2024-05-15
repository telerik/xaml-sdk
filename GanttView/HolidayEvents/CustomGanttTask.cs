using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace HolidayEvents
{
    public class CustomGanttTask : GanttTask
    {
        private List<DateRange> customDeadLines = new List<DateRange>();

        public List<DateRange> CustomDeadLines
        {
            get
            {
                return this.customDeadLines;
            }

            set
            {
                if (this.customDeadLines != value)
                {
                    this.customDeadLines = value;
                    this.OnPropertyChanged(() => this.CustomDeadLines);
                }
            }
        }
    }
}
