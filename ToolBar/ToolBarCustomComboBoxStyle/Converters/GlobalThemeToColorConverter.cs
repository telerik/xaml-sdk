using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace CustomRadComboBoxStyle.Converters
{
    public class GlobalThemeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tokens = value.ToString().Split('_').ToArray();

            if (tokens.Length <= 1)
            {
                return Brushes.White;
            }

            var themeVariation = tokens[1];

            if (themeVariation == "Dark")
            {
                return Brushes.Black;
            }
            else if (themeVariation == "Gray")
            {
                return Brushes.Gray;
            }
            else if (themeVariation == "LightGray")
            {
                return Brushes.LightGray;
            }
            else if (themeVariation == "DarkGray")
            {
                return Brushes.DarkGray;
            }
            else if (themeVariation == "Blue")
            {
                return Brushes.MediumPurple;
            }
            else if (themeVariation == "HighContrast")
            {
                return Brushes.Black;
            }

            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
