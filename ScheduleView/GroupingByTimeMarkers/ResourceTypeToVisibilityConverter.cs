using System;
using System.Windows;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace GroupingByTimeMarkers
{
    public class ResourceTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var resourceType = value as ResourceType;

            if (resourceType != null)
            {
                if (resourceType.DisplayName == "TimeMarkers")
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
