using Prism.Regions;
using System;
using System.Collections.Specialized;
using System.Linq;
using Telerik.Windows.Controls;

namespace NavigationViewWithPrism.PrismRelatedFiles
{
    public class NavigationViewRegionAdapter : RegionAdapterBase<RadNavigationView>
    {
        // By default the SelectorRegionAdapter class will be used for the RadNavigationView, since it inherits Selector.
        // We want to set the Content of the control to the regions and that is why we have extracted the logic of the ContentControlRegionAdapter.
        // Check out the source code here: https://github.com/PrismLibrary/Prism/blob/5ddd97b5c0a9169a081401a766d5448c6b29b4a9/src/Wpf/Prism.Wpf/Regions/ContentControlRegionAdapter.cs
        public NavigationViewRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }
        
        protected override void Adapt(IRegion region, RadNavigationView regionTarget)
        {
            if (regionTarget == null)
                throw new ArgumentNullException(nameof(regionTarget));

            region.ActiveViews.CollectionChanged += delegate
            {
                regionTarget.Content = region.ActiveViews.FirstOrDefault();
            };

            region.Views.CollectionChanged +=
                (sender, e) =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Add && region.ActiveViews.Count() == 0)
                    {
                        region.Activate(e.NewItems[0]);
                    }
                };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
