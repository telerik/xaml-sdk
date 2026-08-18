using CaliburnMicroIntegration.Models;
using System.Runtime.CompilerServices;
using System.Windows;
using Telerik.Windows.Controls;

namespace CaliburnMicroIntegration.Utilities
{
    public class MapExtensions
    {
        public static LocationInfo GetZoomOnSelectedItem(DependencyObject obj)
        {
            return (LocationInfo)obj.GetValue(ZoomOnSelectedItemProperty);
        }

        public static void SetZoomOnSelectedItem(DependencyObject obj, LocationInfo value)
        {
            obj.SetValue(ZoomOnSelectedItemProperty, value);
        }

        public static readonly DependencyProperty ZoomOnSelectedItemProperty =
            DependencyProperty.RegisterAttached("ZoomOnSelectedItem", typeof(LocationInfo), typeof(MapExtensions), new PropertyMetadata(null, OnZoomOnSelectedItemChanged));

        private static void OnZoomOnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                RadMap radMap = (RadMap)d;
                LocationInfo locationInfo = (LocationInfo)e.NewValue;

                radMap.Center = locationInfo.Location;
                radMap.Zoom = 4.5;
            }
        }
    }
}
