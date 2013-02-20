using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Rendering.Virtualization;

namespace RecurringTask
{
	public class TimeLineRecurrenceContainerSelector : DefaultTimeLineContainerSelector
	{
		public override ContainerTypeIdentifier GetContainerType(object item)
		{
			if (item is TimeLineRecurrenceEventInfo)
			{
				return ContainerTypeIdentifier.FromType<TimeLineRecurrenceContainer>();
			}

			return base.GetContainerType(item);
		}
	}
}
