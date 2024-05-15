using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.DataPager;

namespace DataPager_ChangePageSizeDynamically
{
    public class CountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemCount = (int)value;
            List<int> result = new List<int>();
            for (int i = 1; i < itemCount; i++)
            {
                if (i % 5 == 0)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
