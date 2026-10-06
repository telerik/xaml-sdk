using System.Windows;
using System.Windows.Controls;
using Telerik.Charting;

namespace AxisLikeAnnotations
{
    public class CustomAxisLabelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var dp = (AxisLabelModel)item;
            if (dp.Content.Equals(NumericAxisExample.CustomAxisPositionKey))
            {
                return new DataTemplate();
            }
            return base.SelectTemplate(item, container);
        }
    }
}
