using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace ChartViewCustomization
{
    public class PathGeometryToRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            PathGeometry data = value as PathGeometry;

            if (data == null || data.Figures.Count == 0 || data.Figures[0].Segments.Count == 0)
                return null;

            var lineSegment = data.Figures[0].Segments[0] as LineSegment;
            if (lineSegment == null)
                return null;

            var vector = lineSegment.Point - data.Figures[0].StartPoint;

            return vector.Length;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
