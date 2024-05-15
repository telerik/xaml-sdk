using Prism.Regions;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace DockingPrism7.PrismRelatedFiles
{
    public class ClosePaneAction : TriggerAction<RadDocking>
    {
        protected override void Invoke(object parameter)
        {
            var stateChangedArgs = parameter as StateChangeEventArgs;
            if (stateChangedArgs == null)
            {
                return;
            }

            var pane = stateChangedArgs.Panes.FirstOrDefault();
            if (pane == null)
            {
                return;
            }

            IRegion region = RegionManager.GetObservableRegion(stateChangedArgs.Source as DependencyObject).Value;
            if (region == null)
            {
                return;
            }

            region.Remove(pane);
        }
    }
}
