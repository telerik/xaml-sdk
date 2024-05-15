using DockingPrism7.PrismRelatedFiles;
using DockingPrism7.ViewModels;
using DockingPrism7.Views;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using Telerik.Windows.Controls;

namespace DockingPrism7
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);

            containerRegistry.RegisterForNavigation<ShellView>();
            containerRegistry.RegisterForNavigation<IncomingView>();
            containerRegistry.RegisterForNavigation<OutgoingView>();
            containerRegistry.RegisterForNavigation<SentView>();
            containerRegistry.RegisterForNavigation<BrowseView>();
            containerRegistry.RegisterForNavigation<AdditionalView>();

            containerRegistry.RegisterSingleton(typeof(ShellViewModel));
        }

        protected override Window CreateShell()
        {
            return null;
        }

        protected override void OnInitialized()
        {
            var regionManager = Container.Resolve<IRegionManager>();

            // Workaround for RadWindow integration in PRISM
            var shellWindow = Container.Resolve<ShellView>();
            shellWindow.Show();

            MainWindow = shellWindow.ParentOfType<Window>();
            RegionManager.SetRegionManager(MainWindow, regionManager);
            RegionManager.UpdateRegions();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(RadDocking), Container.Resolve<DockingRegionAdapter>());
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
            regionBehaviors.AddIfMissing(DockingActivePaneSyncBehavior.BehaviorKey, typeof(DockingActivePaneSyncBehavior));
        }
    }
}
