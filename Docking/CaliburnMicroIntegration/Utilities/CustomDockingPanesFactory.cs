using Caliburn.Micro;
using CaliburnMicroIntegration.ViewModels;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace CaliburnMicroIntegration.Utilities
{
    public class CustomDockingPanesFactory : DockingPanesFactory
    {
        protected override RadPane CreatePaneForItem(RadDocking radDocking, object item)
        {
            PaneViewModel viewModel = item as PaneViewModel;

            if (viewModel == null)
            {
                return base.CreatePaneForItem(radDocking, item);
            }

            RadPane pane = new RadPane
            {
                Header = viewModel.DisplayName,
                Title = viewModel.DisplayName,
                Content = ViewLocator.LocateForModel(viewModel, null, null),
                DataContext = viewModel,
            };

            return pane;
        }

        protected override void AddPane(RadDocking radDocking, RadPane pane)
        {
            PaneViewModel viewModel = (PaneViewModel)pane.DataContext;
            string displayName = viewModel.DisplayName.ToLower();

            var paneGroup = radDocking.SplitItems
                .ToList()
                .FirstOrDefault(i => i.Control.Name.ToLower().Contains(displayName)) as RadPaneGroup;

            if (paneGroup != null)
            {
                paneGroup.Items.Add(pane);
            }
            else
            {
                base.AddPane(radDocking, pane);
            }
        }
    }
}
