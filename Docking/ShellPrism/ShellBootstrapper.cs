using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Telerik.Windows.Controls;

namespace ShellPrism
{
    public class ShellBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
#if WPF
            Application.Current.MainWindow = this.Shell as Window;
            Application.Current.MainWindow.Show();
#else
            Application.Current.RootVisual = (UIElement)this.Shell;
#endif
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            // Registering the DockingRegionAdapter. This will allow the dock to be marked as region and accomudate views.
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            var regionBehaviorFactory = Container.GetExportedValue<IRegionBehaviorFactory>();
            mappings.RegisterMapping(typeof(RadDocking), Container.GetExportedValue<DockingRegionAdapter>());
            return mappings;
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ShellBootstrapper).Assembly));
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(FileServicesModule));
        }
    }
}