using Prism;
using Prism.Regions;
using System;

namespace ShellPrism8
{
    public class DockActivationRegionBehavior : RegionBehavior
    {
        private IRegionViewRegistry regionViewRegistry;
        
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
            if(!this.Region.Views.Contains(sender))
            {
                return;
            }

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