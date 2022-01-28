using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Rendering.Virtualization;

namespace HighlightedAndSelectableDependencies
{
    public class CustomDefaultTimeLineContainerSelector : DefaultTimeLineContainerSelector
    {
        private static readonly ContainerTypeIdentifier customRelationInfoContainerType = ContainerTypeIdentifier.FromType<CustomRelationContainer>();

        public override ContainerTypeIdentifier GetContainerType(object item)
        {
            if (item is CustomRelationInfo)
            {
                return customRelationInfoContainerType;
            }

            return base.GetContainerType(item);
        }
    }
}
