using System;
using System.Windows;
using System.Windows.Data;
using Telerik.Windows.Diagrams.Core;

namespace HideBezierHandles
{
    public class TypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var type = (ManipulationPointType)Enum.Parse(typeof(ManipulationPointType), value.ToString(), true);
                if (type == ManipulationPointType.BezierStartHandle || type == ManipulationPointType.BezierEndHandle)
                    return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
