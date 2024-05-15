using System;
using System.Linq;
using System.Windows;
using CustomTabControlRegionAdapter;
using CustomTabControlRegionAdapter.Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Telerik.Windows.Controls;

namespace CustomTabControlRegionAdapter_WPF
{
	public class Bootstrapper_WPF : UnityBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<Shell>();
		}

		protected override void InitializeShell()
		{
			base.InitializeShell();
			App.Current.MainWindow = (Window)Shell;
			App.Current.MainWindow.Show();
		}

		protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
		{
			ModuleCatalog catalog = new ModuleCatalog();
			catalog.AddModule(typeof(Module1.Module1));
			catalog.AddModule(typeof(Module2.Module2));
			return catalog;
		}

		protected override Microsoft.Practices.Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
		{
			RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
			mappings.RegisterMapping(typeof(RadTabControl), Container.Resolve<RadTabControlRegionAdapter>());
			return mappings;
		}
	}
}
