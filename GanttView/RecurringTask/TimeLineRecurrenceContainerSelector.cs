using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Rendering.Virtualization;

namespace RecurringTask
{
	public class TimeLineRecurrenceContainerSelector : DefaultTimeLineContainerSelector
	{
        protected static readonly ContainerTypeIdentifier RecurrenceContainerType = ContainerTypeIdentifier.FromType<TimeLineRecurrenceContainer>();

        public override ContainerTypeIdentifier GetContainerType(object item)
        {
            if (item is TimeLineRecurrenceEventInfo)
            {
                return RecurrenceContainerType;
            }

            return base.GetContainerType(item);
        }
	}
}
