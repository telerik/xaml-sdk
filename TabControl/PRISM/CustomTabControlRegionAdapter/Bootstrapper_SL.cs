using System;
using System.Windows;
using CustomTabControlRegionAdapter;
using CustomTabControlRegionAdapter.Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Telerik.Windows.Controls;

namespace CustomTabControlRegionAdapter_SL
{
	public class Bootstrapper_SL : UnityBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			// Use the container to create an instance of the shell.
			Shell view = Container.TryResolve<Shell>();

			// Display the Shell as the root visual for the application.
			Application.Current.RootVisual = view;

			return view;
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
