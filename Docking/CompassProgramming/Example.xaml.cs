using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace CompassProgramming
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
        }

        private static bool CompassNeedsToShow(Telerik.Windows.Controls.Docking.Compass compass)
        {
            return compass.IsLeftIndicatorVisible || compass.IsTopIndicatorVisible || compass.IsRightIndicatorVisible || compass.IsBottomIndicatorVisible || compass.IsCenterIndicatorVisible;
        }

        private void RadDocking_PreviewShowCompass(object sender, Telerik.Windows.Controls.Docking.PreviewShowCompassEventArgs e)
        {
            if (e.TargetGroup != null)
            {
                e.Compass.IsCenterIndicatorVisible = CanDockIn(e.DraggedElement, e.TargetGroup, DockPosition.Center);
                e.Compass.IsLeftIndicatorVisible = CanDockIn(e.DraggedElement, e.TargetGroup, DockPosition.Left);
                e.Compass.IsTopIndicatorVisible = CanDockIn(e.DraggedElement, e.TargetGroup, DockPosition.Top);
                e.Compass.IsRightIndicatorVisible = CanDockIn(e.DraggedElement, e.TargetGroup, DockPosition.Right);
                e.Compass.IsBottomIndicatorVisible = CanDockIn(e.DraggedElement, e.TargetGroup, DockPosition.Bottom);
            }
            else
            {
                e.Compass.IsCenterIndicatorVisible = CanDock(e.DraggedElement, DockPosition.Center);
                e.Compass.IsLeftIndicatorVisible = CanDock(e.DraggedElement, DockPosition.Left);
                e.Compass.IsTopIndicatorVisible = CanDock(e.DraggedElement, DockPosition.Top);
                e.Compass.IsRightIndicatorVisible = CanDock(e.DraggedElement, DockPosition.Right);
                e.Compass.IsBottomIndicatorVisible = CanDock(e.DraggedElement, DockPosition.Bottom);
            }
            e.Canceled = !(CompassNeedsToShow(e.Compass));
        }

        private PaneType GetPaneType(RadPane pane)
        {
            Grid c = pane.Content as Grid;
            if (c != null)
            {
                if (c.Background.Equals(this.Resources["GreenBrush"]))
                {
                    return PaneType.Green;
                }
                else
                {
                    return PaneType.Purple;
                }
            }

            return PaneType.Purple;
        }

        /// <summary>
        /// Determines if the specific Pane's Top, Bottom, Left and Right compasses should be shown for the Dragged Pane
        /// </summary>
        private bool CanDockIn(RadPane paneToDock, RadPane paneInTargetGroup, DockPosition position)
        {
            PaneType paneToDockType = GetPaneType(paneToDock);
            PaneType paneInTargetGroupType = GetPaneType(paneInTargetGroup);

            switch (paneToDockType)
            {
                case PaneType.Green:
                    switch (paneInTargetGroupType)
                    {
                        case PaneType.Green:
                            // The Top, Bottom, Left, Right and Center compasses will be shown when dragging a Green Pane over a Green Pane
                            //return true;

                            // The Center compass only will be shown when dragging a Green Pane over a Green Pane
                            return position != DockPosition.Top && position != DockPosition.Bottom && position != DockPosition.Left && position != DockPosition.Right;
                        case PaneType.Purple:
                            return false;
                    }
                    break;
                case PaneType.Purple:
                    switch (paneInTargetGroupType)
                    {
                        case PaneType.Green:
                            return false;
                        case PaneType.Purple:
                            return true;
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// Determines if the Docking's Top, Bottom, Left and Right compasses should be shown for the Dragged Pane
        /// </summary>
        private bool CanDock(RadPane paneToDock, DockPosition position)
        {
            PaneType paneToDockType = GetPaneType(paneToDock);

            switch (paneToDockType)
            {
                case PaneType.Green:
                    return true;
                case PaneType.Purple:
                    return false;
            }

            return false;
        }

        private bool CanDockIn(object dragged, ISplitItem target, DockPosition position)
        {
            // If there is a pane that cannot be dropped in any of the targeted panes.
            var splitContainer = dragged as RadSplitContainer;
            return !splitContainer.EnumeratePanes().Any((RadPane p) => target.EnumeratePanes().Any((RadPane p1) => !CanDockIn(p, p1, position)));
        }

        private bool CanDock(object dragged, DockPosition position)
        {
            var splitContainer = dragged as RadSplitContainer;
            return !splitContainer.EnumeratePanes().Any((RadPane p) => !CanDock(p, position));
        }
    }
}
