using System;
using System.Windows;
using System.Windows.Data;
using Telerik.Windows.Diagrams.Core;

namespace OrgChart.Converters
{
	/// <summary>
	/// Converts TreeLayoutType To Visibility for the needs of the configuration sliders.
	/// </summary>
	public class LayoutTypeToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var layoutType = value as TreeLayoutType?;
			if (layoutType.HasValue)
			{
				switch (layoutType)
				{
					case TreeLayoutType.TipOverTree: return Visibility.Visible;
					default: return Visibility.Collapsed;
				}
			}
			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
