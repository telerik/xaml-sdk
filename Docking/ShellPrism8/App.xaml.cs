using Prism.Common;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using ShellPrism8.Menu;
using System.Windows;
using Telerik.Windows.Controls;

namespace ShellPrism8
{
    public partial class App : PrismApplication
    {
        private Shell shell;
        protected override Window CreateShell()
        {
            this.shell = Container.Resolve<Shell>();
            return null;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<Output>();
            containerRegistry.RegisterSingleton<ErrorList>();
            containerRegistry.RegisterSingleton<NewDocument>();
            containerRegistry.RegisterSingleton<PropertiesView>();
            containerRegistry.RegisterSingleton<ServerExplorer>();
            containerRegistry.RegisterSingleton<SolutionExplorer>();
            containerRegistry.RegisterSingleton<ToolBox>();
        }

        protected override void OnInitialized()
        {
            var regionManager = Container.Resolve<IRegionManager>();
            RegionManager.SetRegionManager(this.shell, regionManager);
            RegionManager.UpdateRegions();
            
            // Docking region
            regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(ErrorList));
            regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(Output));
            regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(PropertiesView));
            regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(ServerExplorer));
            regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(SolutionExplorer));
            regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(ToolBox));

            // Menu region
            regionManager.RegisterViewWithRegion("FileMenuRegion", typeof(MenuItemNew));
            regionManager.RegisterViewWithRegion("FileMenuRegion", typeof(MenuItemSave));
            regionManager.RegisterViewWithRegion("FileMenuRegion", typeof(MenuItemLoad));
            regionManager.RegisterViewWithRegion("FileMenuRegion", typeof(MenuItemActivatePane));

            Container.Resolve<IEventAggregator>().GetEvent<LoadLayoutEvent>().Publish(null);

            this.shell.Show();

        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            //Registering the DockingRegionAdapter. This will allow the dock to be marked as region and accomudate views.
            regionAdapterMappings.RegisterMapping(typeof(RadDocking), Container.Resolve<DockingRegionAdapter>());
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
            regionBehaviors.AddIfMissing<DockActivationRegionBehavior>("DockActivationRegionBehavior");
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            
            moduleCatalog.AddModule(typeof(FileServicesModule));
        }
    }
}