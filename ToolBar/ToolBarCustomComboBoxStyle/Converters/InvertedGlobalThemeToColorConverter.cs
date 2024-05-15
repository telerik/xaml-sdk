using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace CustomRadComboBoxStyle.Converters
{
    public class InvertedGlobalThemeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tokens = value.ToString().Split('_').ToArray();

            if (tokens.Length <= 1)
            {
                return Brushes.Black;
            }

            var themeVariation = tokens[1];

            if (themeVariation == "Dark" || themeVariation == "HighContrast")
            {
                return Brushes.White;
            }

            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
