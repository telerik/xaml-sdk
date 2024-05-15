using Prism.Regions;
using Telerik.Windows.Controls;

namespace ShellPrism8
{
    public class DockingRegionAdapter : RegionAdapterBase<RadDocking>
    {
        IRegionBehaviorFactory factory; 

        public DockingRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
            factory = regionBehaviorFactory;
        }

        protected override void Adapt(IRegion region, RadDocking regionTarget)
        {
            regionTarget.PanesSource = region.Views;
        }

        protected override IRegion CreateRegion()
        {
            var region = new Region();
            
            region.Behaviors.Add("DockActivationRegionBehavior", factory.CreateFromKey("DockActivationRegionBehavior"));
            return region;
        }
    }
}