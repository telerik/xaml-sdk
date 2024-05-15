using System;
using System.Collections;
using System.Linq;
using System.Windows.Data;

namespace Grouping
{
    public class ItemsToCollectionViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                CollectionViewSource GroupedCountries = new CollectionViewSource();
                GroupedCountries.GroupDescriptions.Add(new PropertyGroupDescription("Continent"));
                var countries = (value as IEnumerable).Cast<Country>().ToList();
                GroupedCountries.Source = countries;

                return GroupedCountries.View;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
