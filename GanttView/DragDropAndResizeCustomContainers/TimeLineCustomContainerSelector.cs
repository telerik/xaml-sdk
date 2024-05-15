using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Rendering.Virtualization;

namespace DragDropAndResizeCustomContainers
{
    public class TimeLineCustomContainerSelector : DefaultTimeLineContainerSelector
    {
        private static readonly ContainerTypeIdentifier timelineDeadlineEventInfoContainerType = ContainerTypeIdentifier.FromType<TimeLineCustomContainer>();

        public override ContainerTypeIdentifier GetContainerType(object item)
        {
            if ( item is TimeLineCustomEventInfo )
            {
                return timelineDeadlineEventInfoContainerType;
            }          

            return base.GetContainerType(item);
        }
    }
}
