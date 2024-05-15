using System;
using System.Windows.Data;
using Telerik.Windows.Diagrams.Core;

namespace OrgChart.Converters
{
	public class LayoutTypeToRotateAngleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var layoutType = value as TreeLayoutType?;
			if (layoutType.HasValue)
			{ 
				switch(layoutType)
				{
					case TreeLayoutType.TreeLeft : return 270;
					case TreeLayoutType.TreeRight: return 90;
					case TreeLayoutType.TreeUp: return 0;
					default : return 180;
				}
			}
			return 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
