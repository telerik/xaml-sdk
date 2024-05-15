using Prism.Mvvm;
using Prism.Regions;

namespace DockingPrism7.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        public IRegionManager RegionManager { get; private set; }

        public ViewModelBase(IRegionManager manager)
        {
            RegionManager = manager;
        }
    }
}