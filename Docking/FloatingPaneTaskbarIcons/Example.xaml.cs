using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
using Telerik.Windows.Controls.Navigation;

namespace FloatingPaneTaskbarIcons
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

        private void RadDocking_PaneStateChange(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var pane = e.OriginalSource as RadPane;
            if (pane != null && pane.IsFloating)
            {
                var toolWindow = pane.GetParentToolWindow() as ToolWindow;
                if (toolWindow != null)
                {
                    RadWindowInteropHelper.SetTitle(toolWindow, pane.Header.ToString());
                    switch (this.GetPaneType(pane))
                    {
                        case PaneType.Green:
                            RadWindowInteropHelper.SetIcon(toolWindow, new BitmapImage(new Uri("pack://application:,,,/FloatingPaneTaskbarIcons;component/Images/icon-green.png", UriKind.Absolute)));
                            break;
                        case PaneType.Blue:
                            RadWindowInteropHelper.SetIcon(toolWindow, new BitmapImage(new Uri("pack://application:,,,/FloatingPaneTaskbarIcons;component/Images/icon-blue.png", UriKind.Absolute)));
                            break;
                        default:
                            RadWindowInteropHelper.SetIcon(toolWindow, new BitmapImage(new Uri("pack://application:,,,/FloatingPaneTaskbarIcons;component/Images/icon-default.png", UriKind.Absolute)));
                            break;
                    }
                }
            }
        }

        private PaneType GetPaneType(RadPane pane)
        {
            var content = pane.Content as Grid;
            if (content.Background.Equals(this.Resources["GreenBrush"]))
            {
                return PaneType.Green;
            }
            else if (content.Background.Equals(this.Resources["BlueBrush"]))
            {
                return PaneType.Blue;
            }
            else
            {
                return PaneType.Default;
            }
        }

        private void RadDocking_PreviewShowCompass(object sender, PreviewShowCompassEventArgs e)
        {
            if (e.TargetGroup != null)
            {
                e.Compass.IsCenterIndicatorVisible = CanDockIn((RadSplitContainer)e.DraggedElement, e.TargetGroup, DockPosition.Center);
                e.Compass.IsLeftIndicatorVisible = CanDockIn((RadSplitContainer)e.DraggedElement, e.TargetGroup, DockPosition.Left);
                e.Compass.IsTopIndicatorVisible = CanDockIn((RadSplitContainer)e.DraggedElement, e.TargetGroup, DockPosition.Top);
                e.Compass.IsRightIndicatorVisible = CanDockIn((RadSplitContainer)e.DraggedElement, e.TargetGroup, DockPosition.Right);
                e.Compass.IsBottomIndicatorVisible = CanDockIn((RadSplitContainer)e.DraggedElement, e.TargetGroup, DockPosition.Bottom);
            }
            else
            {
                e.Compass.IsLeftIndicatorVisible = CanDock((RadSplitContainer)e.DraggedElement, DockPosition.Left);
                e.Compass.IsTopIndicatorVisible = CanDock((RadSplitContainer)e.DraggedElement, DockPosition.Top);
                e.Compass.IsRightIndicatorVisible = CanDock((RadSplitContainer)e.DraggedElement, DockPosition.Right);
                e.Compass.IsBottomIndicatorVisible = CanDock((RadSplitContainer)e.DraggedElement, DockPosition.Bottom);
            }
            e.Canceled = !(CompassNeedsToShow(e.Compass));
        }

        private static bool CompassNeedsToShow(Telerik.Windows.Controls.Docking.Compass compass)
        {
            return compass.IsLeftIndicatorVisible || compass.IsTopIndicatorVisible || compass.IsRightIndicatorVisible || compass.IsBottomIndicatorVisible || compass.IsCenterIndicatorVisible;
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
                            return true;
                        case PaneType.Blue:
                            return false;
                        case PaneType.Default:
                            return false;
                    }
                    break;
                case PaneType.Blue:
                    switch (paneInTargetGroupType)
                    {
                        case PaneType.Green:
                            return false;
                        case PaneType.Blue:
                            return true;
                        case PaneType.Default:
                            return false;
                    }
                    break;
                case PaneType.Default:
                    switch (paneInTargetGroupType)
                    {
                        case PaneType.Green:
                            return false;
                        case PaneType.Blue:
                            return false;
                        case PaneType.Default:
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
                case PaneType.Blue:
                    return true;
                case PaneType.Default:
                    return true;
            }

            return false;
        }

        private bool CanDockIn(ISplitItem dragged, ISplitItem target, DockPosition position)
        {
            return !dragged.EnumeratePanes().Any((RadPane p) => target.EnumeratePanes().Any((RadPane p1) => !CanDockIn(p, p1, position)));
        }

        private bool CanDock(ISplitItem dragged, DockPosition position)
        {
            return !dragged.EnumeratePanes().Any((RadPane p) => !CanDock(p, position));
        }
    }
}
