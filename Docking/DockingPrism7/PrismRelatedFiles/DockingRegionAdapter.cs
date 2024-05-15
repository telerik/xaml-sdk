using CommonServiceLocator;
using Prism.Regions;
using Telerik.Windows.Controls;

namespace DockingPrism7.PrismRelatedFiles
{
    public class DockingRegionAdapter : RegionAdapterBase<RadDocking>
    {
        public DockingRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory, IServiceLocator serviceLocator)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, RadDocking regionTarget)
        {
            regionTarget.PanesSource = region.Views;
        }

        protected override IRegion CreateRegion()
        {
            return new Region();
        }
    }
}