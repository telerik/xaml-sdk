using System;
using System.Windows.Data;

namespace OrgChart.Common
{
	public class PercentageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (parameter != null && parameter.ToString() == "Round")
			{
				return (int)((double)value * 100);
			}
			return (double)value * 100;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is double)
				return (double)value / 100.0;
			return null;
		}
	}
}
