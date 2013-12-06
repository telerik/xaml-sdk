using System.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling.Internal;
using Telerik.Windows.Rendering;

namespace RecurringTask
{
	public class TimeLineRecurrenceContainer : Control, IDataContainer
    {
        private object data;

        public TimeLineRecurrenceContainer()
        {
            this.DefaultStyleKey = typeof(TimeLineRecurrenceContainer);
        }

        public object DataItem
        {
            get
            {
                return this.data;
            }

            set
            {
                if (this.data != value)
                {
                    this.data = value;
                    var info = this.data as TimeLineRecurrenceEventInfo;
                }
            }
        }
    }
}
