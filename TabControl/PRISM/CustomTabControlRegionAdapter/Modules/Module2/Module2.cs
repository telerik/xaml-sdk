using System;
using System.Linq;
using CustomTabControlRegionAdapter.Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Module2
{
	public class Module2 : IModule
	{
		private IUnityContainer Container;
		private IRegionManager RegionManager;

		public Module2(IUnityContainer container, IRegionManager regionManager)
		{
			this.Container = container;
			this.RegionManager = regionManager;
		}

		public void Initialize()
		{
			this.RegionManager.RegisterViewWithRegion(RegionNames.TabControlRegion, typeof(TabItemButtonsView));
		}
	}
}
