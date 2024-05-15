using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Rendering.Virtualization;

namespace HolidayEvents
{
    public class HolidayEventsTimeLineContainerSelector : DefaultTimeLineContainerSelector
    {
        public override ContainerTypeIdentifier GetContainerType(object item)
        {
            if (item is NationalEventSlotInfo)
            {
                return ContainerTypeIdentifier.FromType<NationalEventContainer>();
            }

            if (item is GlobalEventTimeSlotInfo)
            {
                return ContainerTypeIdentifier.FromType<GlobalEventContainer>();
            }

            return base.GetContainerType(item);
        }
    }
}
