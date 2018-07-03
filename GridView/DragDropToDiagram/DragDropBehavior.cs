using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;

namespace DragDropToDiagram_WPF
{
    public class DragDropBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled", 
                typeof(bool),
                typeof(DragDropBehavior), 
                new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }    

        private static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var gridView = dependencyObject as RadGridView;
            if (gridView != null)
            {
                DragDropManager.AddDragInitializeHandler(gridView, OnGridViewRowDragInitialize);
            }
        }

        private static void OnGridViewRowDragInitialize(object sender, DragInitializeEventArgs args)
        {
            args.AllowedEffects = DragDropEffects.All;
            args.DragVisualOffset = new Point(args.RelativeStartPoint.X, args.RelativeStartPoint.Y);
            args.DragVisual = new RadDiagramShape
            {
                Content = ((FrameworkElement)args.OriginalSource).DataContext,
                ContentTemplate = Application.Current.Resources["DragTemplate"] as DataTemplate
            };
        }
    }
}
