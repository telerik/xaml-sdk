using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.GanttView;
using System.Linq;

namespace DependenciesEditing
{
    public class RemoveSelectedTaskConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var items = ((DependenciesEditing.ViewModel)(parameter)).Tasks;
            var selectedTask = value as GanttTask;
            var itemsWithoutCurrentlySelected = items.Where(t => t != selectedTask);
            return itemsWithoutCurrentlySelected;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
