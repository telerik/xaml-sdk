using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace PaneSourceWithLayout
{
    public class CustomDockingPanesFactory : DockingPanesFactory
    {
        protected override Telerik.Windows.Controls.RadPane CreatePaneForItem(object item)
        {
            var viewModel = item as PaneViewModel;
            if (viewModel != null)
            {
                var pane = new RadPane();
                pane.DataContext = item;
                pane.Header = viewModel.HeaderText;
                RadDocking.SetSerializationTag(pane, viewModel.SerializationTag);

                return pane;
            }

            return base.CreatePaneForItem(item);
        }
    }
}
