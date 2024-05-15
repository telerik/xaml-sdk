using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Tooltip
{
	public class ProfitToBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			double profitDifference = (double)value;
			if (profitDifference < 0)
			{
				return new SolidColorBrush(Colors.Red);
			}
			if (profitDifference > 0)
			{
				return new SolidColorBrush(Colors.Green);
			}
			return new SolidColorBrush(Colors.Green);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}