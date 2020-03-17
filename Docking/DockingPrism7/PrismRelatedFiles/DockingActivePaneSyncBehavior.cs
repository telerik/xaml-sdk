using Prism.Regions;
using Prism.Regions.Behaviors;
using System.Collections.Specialized;
using System.Windows;
using Telerik.Windows.Controls;

namespace DockingPrism7.PrismRelatedFiles
{
    public class DockingActivePaneSyncBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        /// <summary>
        /// Name that identifies the DockingActivePaneSyncBehavior behavior in a collection of RegionsBehaviors. 
        /// </summary>
        public static readonly string BehaviorKey = "DockingActivePaneSyncBehavior";
        private RadDocking hostControl;
        private bool updatingActiveViewsInDockingActivePaneChanged;

        /// <summary>
        /// Gets or sets the <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// </summary>
        /// <value>
        /// A <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// </value>
        public DependencyObject HostControl
        {
            get
            {
                return this.hostControl;
            }

            set
            {
                this.hostControl = value as RadDocking;
            }
        }

        /// <summary>
        /// Starts to monitor the <see cref="IRegion"/> to keep it in synch with the items of the <see cref="HostControl"/>.
        /// </summary>
        protected override void OnAttach()
        {
            this.hostControl.ActivePaneChanged += this.OnDockingActivePaneChanged;
            this.Region.ActiveViews.CollectionChanged += this.OnActiveViewsCollectionChanged;
        }

        private void OnActiveViewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.updatingActiveViewsInDockingActivePaneChanged)
            {
                return;
            }

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var newActivePane = e.NewItems[0] as RadPane;
                if (newActivePane.IsHidden)
                {
                    newActivePane.IsHidden = false;
                }

                this.hostControl.ActivePane = newActivePane;
            }
        }

        private void OnDockingActivePaneChanged(object sender, Telerik.Windows.Controls.Docking.ActivePangeChangedEventArgs e)
        {
            try
            {
                this.updatingActiveViewsInDockingActivePaneChanged = true;

                if (e.OriginalSource == sender)
                {
                    if (e.OldPane != null)
                    {
                        if (this.Region.Views.Contains(e.OldPane) && this.Region.ActiveViews.Contains(e.OldPane))
                        {
                            this.Region.Deactivate(e.OldPane);
                        }
                    }

                    if (e.NewPane != null)
                    {
                        if (this.Region.Views.Contains(e.NewPane) && !this.Region.ActiveViews.Contains(e.NewPane))
                        {
                            this.Region.Activate(e.NewPane);
                        }
                    }
                }
            }
            finally
            {
                this.updatingActiveViewsInDockingActivePaneChanged = false;
            }
        }
    }
}
