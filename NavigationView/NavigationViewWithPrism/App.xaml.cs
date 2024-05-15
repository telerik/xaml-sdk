using NavigationViewWithPrism.PrismRelatedFiles;
using NavigationViewWithPrism.Views;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using Telerik.Windows.Controls;

namespace NavigationViewWithPrism
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(RadNavigationView), Container.Resolve<NavigationViewRegionAdapter>());
        }
    }
}
