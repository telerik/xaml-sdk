using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace CustomColumnEditor
{
    public class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var captain = value as Captain;
            if (String.IsNullOrEmpty(captain.Name))
            {
                return String.Format("{0}", captain.Position);
            }

            return String.Format("{0}, {1}", captain.Name, captain.Position);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
