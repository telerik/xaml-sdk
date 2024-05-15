using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Rendering.Virtualization;

namespace TaskResources
{
    public class TimeLineResourceContainerSelector : DefaultTimeLineContainerSelector
    {
        private static readonly ContainerTypeIdentifier timelineResourceEventInfoContainerType = ContainerTypeIdentifier.FromType<TimeLineResourceContainer>();

        public override ContainerTypeIdentifier GetContainerType(object item)
        {
            if (item is TimeLineResourceEventInfo)
            {
                return timelineResourceEventInfoContainerType;
            }

            return base.GetContainerType(item);
        }
    }
}
