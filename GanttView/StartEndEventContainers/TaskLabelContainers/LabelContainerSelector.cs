using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Rendering.Virtualization;

namespace StartEndEventContainers
{
    public class LabelContainerSelector : DefaultTimeLineContainerSelector
    {
        public override ContainerTypeIdentifier GetContainerType(object item)
        {
            if (item is EndLabelEventInfo)
            {
                return ContainerTypeIdentifier.FromType<EndLabelContainer>();
            }

            if (item is StartLabelEventInfo)
            {
                return ContainerTypeIdentifier.FromType<StartLabelContainer>();
            }

            return base.GetContainerType(item);
        }
    }
}
