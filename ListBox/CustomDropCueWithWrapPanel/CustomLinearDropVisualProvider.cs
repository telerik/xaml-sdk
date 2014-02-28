using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.DragDrop;

namespace CustomDropCueWithWrapPanel
{
    public class CustomLinearDropVisualProvider : LinearDropVisualProvider
	{
        public override Point GetLocation(Telerik.Windows.Controls.RadListBoxItem container, Panel panel, Telerik.Windows.Controls.ItemDropPosition dropPosition)
        {
            return container.TransformToVisual(panel).Transform(new Point());
        }
	}
}
