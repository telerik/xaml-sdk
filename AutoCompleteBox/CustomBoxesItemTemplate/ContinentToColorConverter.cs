using System;
using System.Windows.Data;
using System.Windows.Media;

namespace CustomBoxesItemTemplate
{
    public class ContinentToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var continent = (value as Country).Continent;

            switch (continent)
            {
                case Continent.Europe:
                    return new SolidColorBrush(Colors.Blue);
                case Continent.Africa:
                    return new SolidColorBrush(Colors.Magenta);
                case Continent.Asia:
                    return new SolidColorBrush(Colors.Orange);
                case Continent.NorthAmerica:
                    return new SolidColorBrush(Colors.Green);
                case Continent.SouthAmerica:
                    return new SolidColorBrush(Colors.Red);
                case Continent.Australia:
                    return new SolidColorBrush(Colors.Purple);
                case Continent.Antarctica:
                    return new SolidColorBrush(Colors.Gray);
                default:
                    break;
            }

            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
