using System;
using System.Collections;
using System.Linq;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace ColumnsReorderSyncWithListBoxSL
{
	public class ColumnsCollectionConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var columns = value as IList;

			return columns.AsQueryable().OfType<GridViewColumn>().OrderBy(c=>c.DisplayIndex);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
