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
                var children = item.OwnerCollection.Where(i => i.ParentId == item.Id);
                var collection = new DataItemCollection(children);
                collection.SetAssociatedItem(item);
                return collection;
            }

            // We are binding the treeview
            DataItemCollection items = value as DataItemCollection;
            if (items != null)
            {
                var children = items.Where(i => i.ParentId == 0);
                return new DataItemCollection(children);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
