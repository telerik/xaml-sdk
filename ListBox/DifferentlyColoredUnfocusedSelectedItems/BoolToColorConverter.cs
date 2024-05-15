using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DifferentlyColoredUnfocusedSelectedItems
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isKeyboardFocusWithin = (bool)value;

            if (isKeyboardFocusWithin)
            {
                LinearGradientBrush linGrBrush = new LinearGradientBrush();

                linGrBrush.StartPoint = new Point(0.5, 0);
                linGrBrush.EndPoint = new Point(0.5, 1);

                GradientStop firstGrStop = new GradientStop();
                firstGrStop.Color = Color.FromArgb(255, 253, 211, 168);
                GradientStop secondGrStop = new GradientStop();
                secondGrStop.Color = Color.FromArgb(255, 252, 231, 159);

                linGrBrush.GradientStops.Add(firstGrStop);
                linGrBrush.GradientStops.Add(secondGrStop);

                return linGrBrush;
            }
            else
            {
                return new SolidColorBrush(Colors.LightGray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
