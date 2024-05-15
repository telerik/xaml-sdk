using System.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling.Internal;
using Telerik.Windows.Rendering;

namespace SameRowRecurringTask
{
    public class TimeLineRecurrenceContainer : Control, IDataContainer
    {
        private object dataItem;

        public TimeLineRecurrenceContainer()
        {
            this.DefaultStyleKey = typeof(TimeLineRecurrenceContainer);
        }

        public object DataItem
        {
            get
            {
                return this.dataItem;
            }

            set
            {
                if (this.dataItem != value)
                {
                    this.dataItem = value;
                    var info = this.DataItem as TimeLineRecurrenceEventInfo;
                    this.DataContext = info.OriginalEvent;
                }
            }
        }
    }
}
