using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Tooltips
{
	public class DurationToBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			TimeSpan durationDifference = (TimeSpan)value;
			if (durationDifference.Days == 0)
			{
				return new SolidColorBrush(Colors.Red);
			}
			if (durationDifference.Days > 70)
			{
				return new SolidColorBrush(Colors.Green);
			}
			return new SolidColorBrush(Colors.Black);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
