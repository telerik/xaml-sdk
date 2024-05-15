using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.DragDrop;

namespace CustomArrowCue
{
    public static class DragDropManagerUtilities
    {
        private const double dragStartThreshold = 5;
        private static readonly Point EndPointOffset = new Point(3, 3);
        private static bool allowDropCache;
        private static Popup arrowContainer;
        private static ArrowShape arrowVisual;
        private static FrameworkElement rootVisual;

        static DragDropManagerUtilities()
        {
            rootVisual = App.Current.MainWindow;

            arrowVisual = new ArrowShape();
            arrowVisual.HeadHeight = 10;
            arrowVisual.HeadWidth = 10;
            arrowVisual.Stroke = Brushes.RoyalBlue;
            arrowVisual.StrokeThickness = 3;

            arrowContainer = new Popup();
            arrowContainer.AllowsTransparency = true;
            arrowContainer.AllowDrop = true;
            arrowContainer.IsHitTestVisible = false;
            arrowContainer.Placement = PlacementMode.Relative;
            arrowContainer.PlacementTarget = rootVisual;
            arrowContainer.Child = arrowVisual;

            DragDropManager.AddDragOverHandler(rootVisual, OnWindowDragOver, true);
        }

        public static readonly DependencyProperty ShowArrowDragCueProperty =
            DependencyProperty.RegisterAttached(
                "ShowArrowDragCue",
                typeof(bool),
                typeof(DragDropManagerUtilities),
                new PropertyMetadata(false, OnShowArrowDragCueChanged));

        public static bool GetShowArrowDragCue(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowArrowDragCueProperty);
        }

        public static void SetShowArrowDragCue(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowArrowDragCueProperty, value);
        }

        private static void OnShowArrowDragCueChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            var element = (FrameworkElement)target;
            if ((bool)args.NewValue)
            {
                DragDropManager.AddDragInitializeHandler(element, OnElementDragInitialize, true);
                DragDropManager.AddDragDropCompletedHandler(element, OnElementDragDropCompleted, true);
                element.Unloaded += OnElementUnloaded;
            }
            else
            {
                UnsubscribeFromEvents(element);
            }
        }

        private static void OnElementDragInitialize(object sender, DragInitializeEventArgs e)
        {
            arrowContainer.Width = rootVisual.ActualWidth;
            arrowContainer.Height = rootVisual.ActualHeight;
            var position = Mouse.GetPosition(rootVisual);
            arrowVisual.X1 = arrowVisual.X2 = position.X;
            arrowVisual.Y1 = arrowVisual.Y2 = position.Y;

            allowDropCache = rootVisual.AllowDrop;
            rootVisual.AllowDrop = true;
        }

        private static void OnElementDragDropCompleted(object sender, DragDropCompletedEventArgs e)
        {
            arrowContainer.IsOpen = false;
            rootVisual.AllowDrop = allowDropCache;
        }

        private static void OnWindowDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            if (e.AllowedEffects != DragDropEffects.None)
            {
                var position = e.GetPosition(rootVisual) - EndPointOffset;
                arrowVisual.X2 = position.X;
                arrowVisual.Y2 = position.Y;
                arrowVisual.UpdateGeometry();

                if (!arrowContainer.IsOpen &&
                    GetDistance(arrowVisual.X1, arrowVisual.Y1, arrowVisual.X2, arrowVisual.Y2) >= dragStartThreshold)
                {
                    arrowContainer.IsOpen = true;
                }
            }
        }

        private static void UnsubscribeFromEvents(FrameworkElement element)
        {
            DragDropManager.RemoveDragInitializeHandler(element, OnElementDragInitialize);
            DragDropManager.RemoveDragDropCompletedHandler(element, OnElementDragDropCompleted);
            element.Unloaded -= OnElementUnloaded;
        }

        private static void OnElementUnloaded(object sender, RoutedEventArgs e)
        {
            UnsubscribeFromEvents((FrameworkElement)sender);
        }

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Point.Subtract(new Point(x2, y2), new Point(x1, y1)).Length;
        }
    }
}
