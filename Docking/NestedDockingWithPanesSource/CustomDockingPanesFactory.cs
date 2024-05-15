using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace NestedDockingWithPanesSource
{
    public class CustomDockingPanesFactory : DockingPanesFactory
    {
        protected override void AddPane(Telerik.Windows.Controls.RadDocking radDocking, Telerik.Windows.Controls.RadPane pane)
        {
            if (pane.Tag != null)
            {
                var tag = pane.Tag.ToString();
                var paneGroup = radDocking.SplitItems.ToList().FirstOrDefault(i => i.Control.Name.Contains(tag)) as RadPaneGroup;

                if (paneGroup != null)
                {
                    paneGroup.Items.Add(pane);
                }
                else
                {
                    base.AddPane(radDocking, pane);
                }
            }
            else
            {
                base.AddPane(radDocking, pane);
            }
        }
    }
}
