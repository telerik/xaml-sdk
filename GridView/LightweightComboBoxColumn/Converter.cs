using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LightweightComboBoxColumn
{
	public partial class Converter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var key = (int)value;
			if (items.ContainsKey(key))
			{
				return items[key];
			}
			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		private ViewModel viewModel;

		public ViewModel ViewModel
		{
			get { return viewModel; }
			set
			{
				viewModel = value;
				foreach (var item in viewModel.Items)
				{
					items.Add(item.ItemKey, item.ItemNumber);
				}
			}
		}

		private Dictionary<int, string> items = new Dictionary<int, string>();
	}
}
