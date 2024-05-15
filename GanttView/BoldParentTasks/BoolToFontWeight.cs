using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Telerik.Windows.Controls.GanttView;

namespace BoldParentTasks
{
    public class BoolToFontWeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ganttTask = (GanttTask)value;

            if (ganttTask.Children.Any())
            {
                return FontWeights.Bold;
            }
            else
            {
                return FontWeights.Normal;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
