using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using System;
using System.ComponentModel.Composition;

namespace ShellPrism
{
    [Export(typeof(DockActivationRegionBehavior))]
    public class DockActivationRegionBehavior : RegionBehavior
    {
        private IRegionViewRegistry regionViewRegistry;

        [ImportingConstructor]
        public DockActivationRegionBehavior(IRegionViewRegistry regionViewRegistry)
        {
            this.regionViewRegistry = regionViewRegistry;
        }

        protected override void OnAttach()
        {
            if (this.regionViewRegistry != null)
            {
                this.regionViewRegistry.ContentRegistered += this.OnViewRegistered;
            }
        }

        public void OnViewRegistered(object sender, ViewRegisteredEventArgs e)
        {
            var view = e.GetView() as IActiveAware;
            if (view != null)
            {
                // NOTE: This could cause memory leaks! Consider using weak event managers or remove handler when removing the view.
                view.IsActiveChanged += this.OnViewIsActiveChanged;
            }
        }

        public void OnViewIsActiveChanged(object sender, EventArgs e)
        {
            var activeAware = (IActiveAware)sender;
            if (activeAware.IsActive)
            {
                this.Region.Activate(sender);
            }
            else
            {
                this.Region.Deactivate(sender);
            }
        }
    }
}