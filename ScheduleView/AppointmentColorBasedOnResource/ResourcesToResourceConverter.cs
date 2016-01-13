using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace AppointmentColorBasedOnResource
{
	public class ResourcesToResourceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var dictionary = parameter as IDictionary;
			var resources = value as IEnumerable<IResource>;
			var resource = resources == null ? null : resources.FirstOrDefault(r => r.ResourceType == "Room");

            return dictionary != null && resource != null && dictionary.Contains(resource.ResourceName) ? dictionary[resource.ResourceName] : null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
