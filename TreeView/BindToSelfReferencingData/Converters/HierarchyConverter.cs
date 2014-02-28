using BindToSelfReferencingData.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace BindToSelfReferencingData.Converters
{
    public class HierarchyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // We are binding an item
            DataItem item = value as DataItem;
            if (item != null)
            {
                return item.Owner.Where(i => i.ParentId == item.Id);
            }
            // We are binding the treeview
            DataItemCollection items = value as DataItemCollection;
            if (items != null)
            {
                return items.Where(i => i.ParentId == 0);
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
