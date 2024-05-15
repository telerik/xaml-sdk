using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace SummariesVisibilityToggle
{
    public class CustomGanttTimeLineVisualizationBehavior : DefaultGanttTimeLineVisualizationBehavior
    {
        protected override IEnumerable<IEventInfo> GetEventInfos(TimeLineVisualizationState state, HierarchicalItem hierarchicalItem)
        {
            if (!hierarchicalItem.IsExpanded)
            {
                return base.GetEventInfos(state, hierarchicalItem);
            }

            return Enumerable.Empty<IEventInfo>();
        }
    }
}
