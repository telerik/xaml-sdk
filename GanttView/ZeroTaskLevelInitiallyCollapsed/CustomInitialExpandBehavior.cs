using Telerik.Windows.Controls.Scheduling;

namespace ZeroTaskLevelInitiallyCollapsed
{
    public class CustomInitialExpandBehavior : IInitialExpandBehavior
    {
        public bool ShouldExpandItemByDefault(Telerik.Windows.Core.HierarchicalItem item)
        {
            var shouldExpand = item.Level > 0;

            return shouldExpand;
        }
    }
}
