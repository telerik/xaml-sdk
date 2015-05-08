using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;

namespace ToolBarDragAndDrop
{
    public static partial class ToolBarTrayUtilities
    {
        public static readonly DependencyProperty IsDragDropEnabledProperty = DependencyProperty.RegisterAttached(
            "IsDragDropEnabled",
            typeof(bool),
            typeof(ToolBarTrayUtilities),
            new PropertyMetadata(false, OnIsDragDropEnabledChanged));

        public static readonly DependencyProperty NewBandModeProperty = DependencyProperty.RegisterAttached(
            "NewBandMode",
            typeof(NewBandMode),
            typeof(ToolBarTrayUtilities),
            new UIPropertyMetadata(NewBandMode.None));

        public static readonly DependencyProperty TrayOwnerProperty = DependencyProperty.RegisterAttached(
            "TrayOwner",
            typeof(RadToolBarTray),
            typeof(ToolBarTrayUtilities),
            new PropertyMetadata(null, OnTrayOwnerChanged));

        private static Dictionary<RadToolBarTray, Border> TrayToIndicatorBorderDict = new Dictionary<RadToolBarTray, Border>();
        private static Dictionary<RadToolBarTray, FrameworkElement> trayToHostDict = new Dictionary<RadToolBarTray, FrameworkElement>();
        private static DragDropInfo lastInitializedInfo;

        public static bool GetIsDragDropEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDragDropEnabledProperty);
        }

        public static void SetIsDragDropEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragDropEnabledProperty, value);
        }

        public static NewBandMode GetNewBandMode(DependencyObject obj)
        {
            return (NewBandMode)obj.GetValue(NewBandModeProperty);
        }

        public static void SetNewBandMode(DependencyObject obj, NewBandMode value)
        {
            obj.SetValue(NewBandModeProperty, value);
        }

        public static RadToolBarTray GetTrayOwner(DependencyObject obj)
        {
            return (RadToolBarTray)obj.GetValue(TrayOwnerProperty);
        }

        public static void SetTrayOwner(DependencyObject obj, RadToolBarTray value)
        {
            obj.SetValue(TrayOwnerProperty, value);
        }

        internal static void SetNewBandModeToTrays(NewBandMode mode)
        {
            foreach (RadToolBarTray tray in TrayToIndicatorBorderDict.Keys)
            {
                ToolBarTrayUtilities.SetNewBandMode(tray, mode);
            }
        }

        private static void OnTrayOwnerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                return;

            RadToolBarTray trayOwner = e.NewValue as RadToolBarTray;
            Border border = d as Border;
            if (trayOwner != null && border != null)
            {
                TrayToIndicatorBorderDict.Add(trayOwner, border);
            }
        }

        private static void OnIsDragDropEnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            RadToolBarTray tray = (RadToolBarTray)target;

            if ((bool)args.OldValue)
            {
                DragDropManager.RemoveDragInitializeHandler(tray, TrayDragInitialized);
                DragDropManager.RemoveDragEnterHandler(tray, TrayDragEntered);
                DragDropManager.RemoveDragOverHandler(tray, TrayDraggedOver);
                DragDropManager.RemoveDragLeaveHandler(tray, TrayDragLeft);
                DragDropManager.RemoveDropHandler(tray, TrayDropped);
            }

            if ((bool)args.NewValue)
            {
                DragDropManager.AddDragInitializeHandler(tray, TrayDragInitialized);
                DragDropManager.AddDragEnterHandler(tray, TrayDragEntered);
                DragDropManager.AddDragOverHandler(tray, TrayDraggedOver);
                DragDropManager.AddDragLeaveHandler(tray, TrayDragLeft);
                DragDropManager.AddDropHandler(tray, TrayDropped);
            }
        }

        private static void TrayDragInitialized(object sender, DragInitializeEventArgs e)
        {
            RadToolBarTray tray = sender as RadToolBarTray;
            RadToolBar toolBar = e.Source as RadToolBar;
            if (tray == null || toolBar == null)
            {
                return;
            }

            DragDropInfo info = new DragDropInfo(toolBar, tray);
            lastInitializedInfo = info;
            if (trayToHostDict.ContainsKey(tray) || !tray.IsMouseOver)
            {
                MoveToolBarToDragVisual(info, tray);
            }
            else
            {
                SetHitTesting(info.Tray.Items);
            }

            SetActiveToolBarStyle(info.ToolBar);
            if (trayToHostDict.ContainsKey(info.Tray))
            {
                trayToHostDict[info.Tray].Opacity = 0;
            }

            DragDropManager.AddDragDropCompletedHandler(info.ToolBar, ToolBarDragDropCompleted);
            DragDropManager.AddGiveFeedbackHandler(info.ToolBar, ToolBarGiveFeedback);

            e.Data = info;
            e.DragVisual = info.DragVisual;
            e.DragVisualOffset = e.RelativeStartPoint;
            e.AllowedEffects = DragDropEffects.All;
            e.Handled = true;
        }

        private static void TrayDragEntered(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            RadToolBarTray tray = sender as RadToolBarTray;
            DragDropInfo info = GetDragDropInfo(e.Data);
            if (tray == null || info == null)
            {
                return;
            }

            lastInitializedInfo = null;

            if (!tray.Items.Contains(info.ToolBar))
            {
                var positionInfo = BandsUtilities.CalculateToolBarPositionInfo(info.ToolBar, tray, e.GetPosition(tray));
                MoveToolBarToTray(info, tray);
                bool allowNewBandCreation = GetNewBandMode(tray) == NewBandMode.Live;
                BandsUtilities.UpdateToolBarPosition(tray, info.ToolBar, positionInfo, allowNewBandCreation);
                UpdateNewBandIndicator(info, positionInfo, tray);
                SetHitTesting(tray.Items);
            }

            e.Handled = true;
        }

        private static void TrayDraggedOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            RadToolBarTray tray = sender as RadToolBarTray;
            DragDropInfo info = GetDragDropInfo(e.Data);
            if (tray == null || info == null)
            {
                return;
            }

            var positionInfo = BandsUtilities.CalculateToolBarPositionInfo(info.ToolBar, tray, e.GetPosition(tray));
            bool allowNewBandCreation = GetNewBandMode(tray) == NewBandMode.Live;
            BandsUtilities.UpdateToolBarPosition(tray, info.ToolBar, positionInfo, allowNewBandCreation);
            UpdateNewBandIndicator(info, positionInfo, tray);
        }

        private static void TrayDragLeft(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            RadToolBarTray tray = sender as RadToolBarTray;
            DragDropInfo info = GetDragDropInfo(e.Data);
            if (tray == null || info == null)
            {
                return;
            }

            ClearHitTesting(tray.Items);
            MoveToolBarToDragVisual(info, tray);
            HideNewBandIndicator(tray);
        }

        private static void TrayDropped(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            RadToolBarTray destinationTray = sender as RadToolBarTray;
            DragDropInfo info = GetDragDropInfo(e.Data);
            if (destinationTray == null || info == null)
            {
                return;
            }

            var positionInfo = BandsUtilities.CalculateToolBarPositionInfo(info.ToolBar, destinationTray, e.GetPosition(destinationTray));
            if (!destinationTray.Items.Contains(info.ToolBar))
            {
                MoveToolBarToTray(info, destinationTray);
            }

            bool allowNewBandCreation = GetNewBandMode(destinationTray) != NewBandMode.None;
            BandsUtilities.UpdateToolBarPosition(destinationTray, info.ToolBar, positionInfo, allowNewBandCreation);

            ClearHitTesting(destinationTray.Items);
            HideNewBandIndicator(destinationTray);

            e.Handled = true;
        }

        private static void ToolBarDragDropCompleted(object sender, DragDropCompletedEventArgs e)
        {
            RadToolBar toolBar = e.Source as RadToolBar;
            DragDropInfo info = GetDragDropInfo(e.Data);
            if (toolBar == null || info == null)
            {
                return;
            }

            DragDropManager.RemoveDragDropCompletedHandler(info.ToolBar, ToolBarDragDropCompleted);
            DragDropManager.RemoveGiveFeedbackHandler(info.ToolBar, ToolBarGiveFeedback);

            ClearActiveToolBarStyle(info.ToolBar);
            ClearHitTesting(info.Tray.Items);
            CloseHost(info);
            if (info.DragVisual.Child != null)
            {
                info.DragVisual.Child = null;

                Window dragVisualWindow = Window.GetWindow(info.DragVisual);

                InitializeHost(info.ToolBar, dragVisualWindow.Left, dragVisualWindow.Top);
            }

            HideNewBandIndicator(info.Tray);

            e.Handled = true;
        }

        private static void ToolBarGiveFeedback(object sender, Telerik.Windows.DragDrop.GiveFeedbackEventArgs e)
        {
            if (lastInitializedInfo != null && lastInitializedInfo.Tray.Items.Contains(lastInitializedInfo.ToolBar) && !lastInitializedInfo.Tray.IsMouseOver)
            {
                // Handle cases when DragInitialized was raised when the mouse was not in tray's bounds.
                ClearHitTesting(lastInitializedInfo.Tray.Items);
                MoveToolBarToDragVisual(lastInitializedInfo, lastInitializedInfo.Tray);
                lastInitializedInfo = null;
            }

            e.SetCursor(System.Windows.Input.Cursors.SizeAll);
            e.Handled = true;
        }

        private static void MoveToolBarToTray(DragDropInfo info, RadToolBarTray tray)
        {
            info.DragVisual.Child = null;
            info.Tray.Items.Remove(info.ToolBar);
            tray.Items.Add(info.ToolBar);
        }

        private static void MoveToolBarToDragVisual(DragDropInfo info, RadToolBarTray tray)
        {
            tray.Items.Remove(info.ToolBar);
            info.DragVisual.Child = info.ToolBar;
        }

        private static DragDropInfo GetDragDropInfo(object dataObject)
        {
            DragDropInfo info = dataObject as DragDropInfo;
            if (info != null)
            {
                return info;
            }

            return Telerik.Windows.DragDrop.Behaviors.DataObjectHelper.GetData(dataObject, typeof(DragDropInfo), false) as DragDropInfo;
        }

        private static void SetHitTesting(ItemCollection toolBars)
        {
            // Prevents flickering of toolbars when dragging.
            foreach (RadToolBar toolBar in toolBars)
            {
                toolBar.IsHitTestVisible = false;
            }
        }

        private static void ClearHitTesting(ItemCollection toolBars)
        {
            foreach (RadToolBar toolBar in toolBars)
            {
                toolBar.ClearValue(RadToolBar.IsHitTestVisibleProperty);
            }
        }

        private static void SetActiveToolBarStyle(RadToolBar toolBar)
        {
            toolBar.Background = new SolidColorBrush(new Color { A = 127, R = 127, G = 127, B = 127, });
        }

        private static void ClearActiveToolBarStyle(RadToolBar toolBar)
        {
            toolBar.ClearValue(RadToolBar.BackgroundProperty);
        }

        private static void UpdateNewBandIndicator(DragDropInfo info, BandsUtilities.ToolBarPositionInfo positionInfo, RadToolBarTray tray)
        {
            if (GetNewBandMode(tray) != NewBandMode.Indicator)
            {
                return;
            }

            if (positionInfo.NewBand.HasValue)
            {
                double margin = 3;
                int band = positionInfo.NewBand.Value;

                if (tray.Orientation == Orientation.Horizontal)
                {
                    ToolBarTrayUtilities.TrayToIndicatorBorderDict[tray].BorderThickness = new Thickness(0, band < 0 ? margin : 0, 0, band < 0 ? 0 : margin);
                }
                else
                {
                    ToolBarTrayUtilities.TrayToIndicatorBorderDict[tray].BorderThickness = new Thickness(band < 0 ? margin : 0, 0, band < 0 ? 0 : margin, 0);
                }
            }
            else
            {
                HideNewBandIndicator(tray);
            }
        }

        private static void HideNewBandIndicator(RadToolBarTray tray)
        {
            if (GetNewBandMode(tray) != NewBandMode.Indicator)
            {
                return;
            }

            ToolBarTrayUtilities.TrayToIndicatorBorderDict[tray].BorderThickness = new Thickness();
        }

        private static void InitializeHost(RadToolBar toolBar, double left, double top)
        {
            Border border = new Border();
            border.Background = new SolidColorBrush(new Color { A = 50, R = 127, G = 127, B = 127 });
            border.CornerRadius = new CornerRadius(3);
            RadToolBarTray tray = new RadToolBarTray();
            tray.Orientation = toolBar.Orientation;
            tray.Items.Add(toolBar);
            border.Child = tray;
            double hMargin = DragDropManager.MinimumHorizontalDragDistance;
            double vMargin = DragDropManager.MinimumVerticalDragDistance;
            tray.Margin = new Thickness(hMargin, vMargin, hMargin, vMargin);
            DragDropManager.AddDragInitializeHandler(tray, TrayDragInitialized);
            FrameworkElement host = InitializeWindowHost(border, left - hMargin - 1, top - vMargin);
            trayToHostDict[tray] = host;
        }

        private static FrameworkElement InitializeWindowHost(UIElement child, double left, double top)
        {
            Window window = new Window();
            window.Owner = Application.Current.MainWindow;
            window.AllowsTransparency = true;
            window.WindowStyle = WindowStyle.None;
            window.Background = null;
            window.ShowInTaskbar = false;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Content = child;
            window.Left = left;
            window.Top = top;
            window.Show();

            return window;
        }

        private static void CloseHost(DragDropInfo info)
        {
            if (trayToHostDict.ContainsKey(info.Tray))
            {
                DragDropManager.RemoveDragInitializeHandler(info.Tray, TrayDragInitialized);
                CloseWindowHost(trayToHostDict[info.Tray]);
                trayToHostDict.Remove(info.Tray);
            }
        }

        private static void CloseWindowHost(FrameworkElement host)
        {
            ((Window)host).Close();
        }
    }
}