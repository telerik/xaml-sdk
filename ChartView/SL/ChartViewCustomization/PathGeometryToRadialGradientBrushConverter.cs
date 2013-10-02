using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Charting;
using Telerik.Windows.Diagrams.Core;

namespace ChartViewCustomization
{
    public class PathGeometryToRadialGradientBrushConverter : IValueConverter
    {
        static PathGeometryToRadiusConverter pathGeometryToRadiusConverter = new PathGeometryToRadiusConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            RadialGradientBrush brush = new RadialGradientBrush();
            brush.MappingMode = BrushMappingMode.Absolute;
            var mainColor = (((value as Path).Tag as PieDataPoint).DataItem as DetailedInfo).MainColor;
            var secondColor = (((value as Path).Tag as PieDataPoint).DataItem as DetailedInfo).SecondColor;

            brush.GradientStops.Add(new GradientStop { Color = Telerik.Windows.Controls.ColorEditor.ColorConverter.ColorFromString(mainColor), Offset = 0.4 });
            brush.GradientStops.Add(new GradientStop { Color = Telerik.Windows.Controls.ColorEditor.ColorConverter.ColorFromString(secondColor), Offset = 1});

            BindingOperations.SetBinding(brush, RadialGradientBrush.CenterProperty, new Binding("Data.Figures[0].StartPoint") { Source = value });
            BindingOperations.SetBinding(brush, RadialGradientBrush.GradientOriginProperty, new Binding("Data.Figures[0].StartPoint") { Source = value });
            BindingOperations.SetBinding(brush, RadialGradientBrush.RadiusXProperty, new Binding("Data") { Source = value, Converter = pathGeometryToRadiusConverter });
            BindingOperations.SetBinding(brush, RadialGradientBrush.RadiusYProperty, new Binding("Data") { Source = value, Converter = pathGeometryToRadiusConverter });
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

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

            var vector = lineSegment.Point.Subtract(data.Figures[0].StartPoint);

            return vector.Length;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
