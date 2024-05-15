using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.HeatMap;

namespace DragToSelect
{
    public static class HeatMapUtilities
    {
        private static RadHeatMap currentHeatmap;
        private static Canvas selectionRectangleHost;
        private static Rectangle selectionRectangle;
        private static HeatMapCellDataPoint clickedDataPoint;
        private static HeatMapCellDataPoint lastHoveredDataPoint;
        private static double clickedX;
        private static double clickedY;

        static HeatMapUtilities()
        {
            selectionRectangle = new Rectangle();
            selectionRectangle.Stroke = new SolidColorBrush(Colors.White);
            selectionRectangle.Fill = new SolidColorBrush(new Color { A = 99, R = 255, G = 255, B = 255 });
            selectionRectangle.IsHitTestVisible = false;

            selectionRectangleHost = new Canvas();
            selectionRectangleHost.Children.Add(selectionRectangle);
        }

        public static bool GetIsDragToSelectEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDragToSelectEnabledProperty);
        }

        public static void SetIsDragToSelectEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragToSelectEnabledProperty, value);
        }

        public static readonly DependencyProperty IsDragToSelectEnabledProperty = DependencyProperty.RegisterAttached(
            "IsDragToSelectEnabled",
            typeof(bool),
            typeof(HeatMapUtilities),
            new PropertyMetadata(false, IsDragToSelectEnabledChanged));

        private static void IsDragToSelectEnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            var heatmap = (RadHeatMap)target;

            if ((bool)args.NewValue)
            {
                heatmap.MouseLeftButtonDown += Heatmap_MouseLeftButtonDown;
                heatmap.MouseLeftButtonUp += Heatmap_MouseLeftButtonUp;
                heatmap.MouseMove += Heatmap_MouseMove;
            }
            else
            {
                heatmap.MouseLeftButtonDown -= Heatmap_MouseLeftButtonDown;
                heatmap.MouseLeftButtonUp -= Heatmap_MouseLeftButtonUp;
                heatmap.MouseMove -= Heatmap_MouseMove;
            }
        }

        private static void Heatmap_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            currentHeatmap = (RadHeatMap)sender;
            if (currentHeatmap.HoveredCellDataPoint == null)
            {
                return;
            }

            EnsureSelectionRectangleVisualParent();
            selectionRectangle.Visibility = Visibility.Visible;
            selectionRectangle.Width = 0;
            selectionRectangle.Height = 0;

            var pos = e.GetPosition(currentHeatmap);
            clickedX = pos.X;
            clickedY = pos.Y;
            clickedDataPoint = currentHeatmap.HoveredCellDataPoint;
            lastHoveredDataPoint = clickedDataPoint;
        }

        private static void Heatmap_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (currentHeatmap != sender)
            {
                return;
            }

            if (clickedDataPoint == null || currentHeatmap.HoveredCellDataPoint == null)
            {
                return;
            }

            lastHoveredDataPoint = currentHeatmap.HoveredCellDataPoint;
            var pos = e.GetPosition(currentHeatmap);

            Rect r;
            if (currentHeatmap.Definition is CategoricalDefinition)
            {
                r = new Rect(new Point(clickedX, clickedY), pos);
            }
            else if (currentHeatmap.Definition is HorizontalDefinition)
            {
                r = new Rect(new Point(0, clickedY), new Point(currentHeatmap.ActualWidth, pos.Y));
            }
            else
            {
                r = new Rect(new Point(clickedX, 0), new Point(pos.X, currentHeatmap.ActualHeight));
            }

            Canvas.SetLeft(selectionRectangle, r.Left);
            Canvas.SetTop(selectionRectangle, r.Top);
            selectionRectangle.Width = r.Width;
            selectionRectangle.Height = r.Height;
        }

        private static void Heatmap_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DoSelection(lastHoveredDataPoint);
            selectionRectangle.Visibility = Visibility.Collapsed;
            clickedDataPoint = null;
            lastHoveredDataPoint = null;
            currentHeatmap = null;
        }

        private static void EnsureSelectionRectangleVisualParent()
        {
            var newParentGrid = Telerik.Windows.Controls.ChildrenOfTypeExtensions.ChildrenOfType<Grid>(currentHeatmap).First();
            var oldParentGrid = selectionRectangleHost.Parent as Grid;
            if (oldParentGrid != newParentGrid && oldParentGrid != null)
            {
                oldParentGrid.Children.Remove(selectionRectangleHost);
            }
            if (oldParentGrid != newParentGrid)
            {
                newParentGrid.Children.Add(selectionRectangleHost);
            }
        }

        private static void DoSelection(HeatMapCellDataPoint hoveredDataPoint)
        {
            if (clickedDataPoint == null || hoveredDataPoint == null)
            {
                return;
            }

            int startRowIndex = Math.Min(clickedDataPoint.RowIndex, hoveredDataPoint.RowIndex);
            int endRowIndex = Math.Max(clickedDataPoint.RowIndex, hoveredDataPoint.RowIndex);
            int startColumnIndex = Math.Min(clickedDataPoint.ColumnIndex, hoveredDataPoint.ColumnIndex);
            int endColumnIndex = Math.Max(clickedDataPoint.ColumnIndex, hoveredDataPoint.ColumnIndex);

            var cDefinition = currentHeatmap.Definition as CategoricalDefinition;
            if (cDefinition != null)
            {
                DoSelection(cDefinition, startRowIndex, endRowIndex, startColumnIndex, endColumnIndex);
            }
            var hDefinition = currentHeatmap.Definition as HorizontalDefinition;
            if (hDefinition != null)
            {
                DoSelection(hDefinition, startRowIndex, endRowIndex);
            }
            var vDefinition = currentHeatmap.Definition as VerticalDefinition;
            if (vDefinition != null)
            {
                DoSelection(vDefinition, startColumnIndex, endColumnIndex);
            }
        }

        private static void DoSelection(CategoricalDefinition definition, int startRowIndex, int endRowIndex, int startColumnIndex, int endColumnIndex)
        {
            List<object> rowCategories;
            List<object> colCategories;
            GetRowAndColumnCategories(definition, startRowIndex, endRowIndex, startColumnIndex, endColumnIndex, out rowCategories, out colCategories);

            definition.SelectedItems.Clear();
            foreach (var item in definition.ItemsSource)
            {
                object rowCategory = GetPropertyValue(item, definition.RowGroupMemberPath);
                object colCategory = GetPropertyValue(item, definition.ColumnGroupMemberPath);

                if (rowCategories.Contains(rowCategory) && colCategories.Contains(colCategory))
                {
                    definition.SelectedItems.Add(item);
                }
            }
        }

        private static void DoSelection(MemberDefinitionBase definition, int startIndex, int endIndex)
        {
            int i = 0;
            definition.SelectedItems.Clear();
            foreach (var item in definition.ItemsSource)
            {
                if (startIndex <= i)
                {
                    definition.SelectedItems.Add(item);
                }
                if (endIndex <= i)
                {
                    break;
                }
                i++;
            }
        }

        private static void GetRowAndColumnCategories(CategoricalDefinition definition, int startRowIndex, int endRowIndex, int startColumnIndex, int endColumnIndex, out List<object> rowCategories, out List<object> colCategories)
        {
            rowCategories = new List<object>();
            colCategories = new List<object>();
            foreach (var item in definition.ItemsSource)
            {
                object rowCategory = GetPropertyValue(item, definition.RowGroupMemberPath);
                object colCategory = GetPropertyValue(item, definition.ColumnGroupMemberPath);

                if (!rowCategories.Contains(rowCategory))
                {
                    rowCategories.Add(rowCategory);
                }
                if (!colCategories.Contains(colCategory))
                {
                    colCategories.Add(colCategory);
                }
            }

            rowCategories = rowCategories.GetRange(startRowIndex, endRowIndex - startRowIndex + 1);
            colCategories = colCategories.GetRange(startColumnIndex, endColumnIndex - startColumnIndex + 1);
        }

        private static object GetPropertyValue(object item, string propertyName)
        {
            PropertyInfo pi = item.GetType().GetProperty(propertyName);
            return pi.GetValue(item, null);
        }
    }
}