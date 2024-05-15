using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace HighlightedAndSelectableDependencies
{
    public class CustomDefaultGanttTimeLineVisualizationBehavior : DefaultGanttTimeLineVisualizationBehavior
    {
        protected override System.Collections.IEnumerable GetEventsData(TimeLineVisualizationState state)
        {
            foreach (var relationInfo in this.GetVisibleCustomRalationInfos(state))
            {
                yield return relationInfo;
            }

            foreach (HierarchicalItem visibleItem in state.VisibleItems)
            {
                int index = visibleItem.Index;
                foreach (IEventInfo eventInfo in this.GetEventInfos(state, visibleItem))
                {
                    yield return eventInfo;
                }
            }
        }

        private bool IsGroupVisible(HierarchicalItem hierarchicalItem)
        {
            return hierarchicalItem.Level == 0 || (hierarchicalItem.Parent.IsExpanded && IsGroupVisible(hierarchicalItem.Parent));
        }

        private IEnumerable<CustomRelationInfo> GetVisibleCustomRalationInfos(TimeLineVisualizationState state)
        {
            var hierarchicalItems = (IList<HierarchicalItem>)state.HierarchicalAdapter;

            if (hierarchicalItems != null)
            {
                var groupItemIndex = 0;

                foreach (var groupItem in hierarchicalItems)
                {
                    var task = (IGanttTask)groupItem.SourceItem;

                    var dependencies = task.Dependencies ?? Enumerable.Empty<IDependency>();
                    if (dependencies.ToEnumerable().Count() > 0)
                    {
                        var rounedTo = state.Rounder.Round(task);

                        foreach (IDependency dependency in dependencies)
                        {
                            var fromGroupItem = state.HierarchicalAdapter.GetItemWrapperByItemKey(dependency.FromTask);
                            if (fromGroupItem != null && IsGroupVisible(fromGroupItem))
                            {
                                var fromGroupIndex = fromGroupItem.Index;

                                if ((fromGroupIndex >= state.VisibleGroupsRange.Start && groupItemIndex <= state.VisibleGroupsRange.End) ||
                                    (groupItemIndex >= state.VisibleGroupsRange.Start && fromGroupIndex <= state.VisibleGroupsRange.End))
                                {
                                    var groupsRange = new Range<int>(fromGroupIndex, groupItemIndex);
                                    var roundedFrom = state.Rounder.Round(dependency.FromTask);
                                    var timeRange = CustomRelationInfo.GetRelationTimeRange(roundedFrom, task, dependency.Type);

                                    if (state.VisibleTimeRange.IntersectsWith(timeRange.Normalize()))
                                    {
                                        yield return new CustomRelationInfo(task, dependency, groupsRange, timeRange);
                                    }
                                }
                            }
                        }
                    }

                    groupItemIndex++;
                }
            }
        }
    }
}
