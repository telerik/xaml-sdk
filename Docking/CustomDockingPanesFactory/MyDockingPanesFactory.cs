using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace CustomDockingPanesFactory
{
    public class MyDockingPanesFactory : DockingPanesFactory
    {
        protected override void AddPane(Telerik.Windows.Controls.RadDocking radDocking, Telerik.Windows.Controls.RadPane pane)
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

        protected override void RemovePane(RadPane pane)
        {
            base.RemovePane(pane);
        }
    }
}
