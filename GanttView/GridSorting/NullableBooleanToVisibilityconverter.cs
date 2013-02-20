using System;
using System.Windows;
using System.Windows.Data;

namespace GridSorting
{
	public class NullableBooleanToVisibilityconverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if ((bool?)value == true && string.Equals((string)parameter, "true"))
			{
				return Visibility.Visible;
			}
			else if ((bool?)value == false && string.Equals((string)parameter, "false"))
			{
				return Visibility.Visible;
			}

			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}