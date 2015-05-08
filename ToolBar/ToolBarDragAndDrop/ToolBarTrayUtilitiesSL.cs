using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;

namespace ToolBarDragAndDrop_SL
{
    public partial class ToolBarTrayUtilitiesSL
    {
        public static readonly DependencyProperty IsDragDropEnabledProperty = DependencyProperty.RegisterAttached(
                    "IsDragDropEnabled",
                    typeof(bool),
                    typeof(ToolBarTrayUtilitiesSL),
                    new PropertyMetadata(false, OnIsDragDropEnabledChanged));

        public static readonly DependencyProperty NewBandModeProperty = DependencyProperty.RegisterAttached(
            "NewBandMode",
            typeof(NewBandMode),
            typeof(ToolBarTrayUtilitiesSL),
            new PropertyMetadata(NewBandMode.None));

        public static readonly DependencyProperty TrayOwnerProperty = DependencyProperty.RegisterAttached(
            "TrayOwner",
            typeof(RadToolBarTray),
            typeof(ToolBarTrayUtilitiesSL),
            new PropertyMetadata(null, OnTrayOwnerChanged));

        private static Dictionary<RadToolBarTray, Border> TrayToIndicatorBorderDict = new Dictionary<RadToolBarTray, Border>();
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
                ToolBarTrayUtilitiesSL.SetNewBandMode(tray, mode);
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

            SetActiveToolBarStyle(info.ToolBar);

            DragDropManager.AddDragDropCompletedHandler(info.ToolBar, ToolBarDragDropCompleted);
           
            e.DragVisual = CreateDragImageFromToolBar(info.ToolBar);
            e.Data = info;
            e.DragVisualOffset = e.RelativeStartPoint;
            e.AllowedEffects = DragDropEffects.All;
            e.Handled = true;
        }

        private static Image CreateDragImageFromToolBar(RadToolBar bar)
        {
            Image draggingImage = new System.Windows.Controls.Image
            {
                Source = new Telerik.Windows.Media.Imaging.RadBitmap(bar).Bitmap,
                Width = bar.ActualWidth,
                Height = bar.ActualHeight
            };
            return draggingImage;
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

            ClearActiveToolBarStyle(info.ToolBar);
            ClearHitTesting(info.Tray.Items);          

            HideNewBandIndicator(info.Tray);

            e.Handled = true;
        }

        private static void MoveToolBarToTray(DragDropInfo info, RadToolBarTray tray)
        {
            info.Tray.Items.Remove(info.ToolBar);
            tray.Items.Add(info.ToolBar);
            info.Tray = tray;

            info.DragVisual.Child = null;

            // Prevents Flickering when dragging over tray and the mouse is over.
            info.ToolBar.IsHitTestVisible = false;
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
                    ToolBarTrayUtilitiesSL.TrayToIndicatorBorderDict[tray].BorderThickness = new Thickness(0, band < 0 ? margin : 0, 0, band < 0 ? 0 : margin);
                }
                else
                {
                    ToolBarTrayUtilitiesSL.TrayToIndicatorBorderDict[tray].BorderThickness = new Thickness(band < 0 ? margin : 0, 0, band < 0 ? 0 : margin, 0);
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

            ToolBarTrayUtilitiesSL.TrayToIndicatorBorderDict[tray].BorderThickness = new Thickness();
        }
    }
}