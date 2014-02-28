using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Rendering.Virtualization;

namespace TimelineIntervalMarkers
{
    public class IntervalContainerSelector : DefaultTimeLineContainerSelector
    {
        protected static readonly ContainerTypeIdentifier IntervalContainerType = ContainerTypeIdentifier.FromType<IntervalContainer>();
        protected static readonly ContainerTypeIdentifier CurrentHourIndicatorEventInfoType = ContainerTypeIdentifier.FromType<CurrentHourIndicatorContainer>();

        public override ContainerTypeIdentifier GetContainerType(object item)
        {
            if (item is IntervalEventInfo)
            {
                return IntervalContainerType;
            }

            if (item is CurrentHourIndicatorEventInfo)
            {
                return CurrentHourIndicatorEventInfoType;
            }

            return base.GetContainerType(item);
        }
    }
}
