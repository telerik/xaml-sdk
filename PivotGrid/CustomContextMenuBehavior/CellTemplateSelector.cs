using System;
using System.Windows;
using System.Windows.Controls;
using Telerik.Pivot.Core;
using Telerik.Windows.Controls;

namespace CustomContextMenuBehavior
{
    public class CellTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LowerValueTemplate { get; set; }
        public DataTemplate HigherValueTemplate { get; set; }
        public double LimitValue { get; set; }
        public string PropertyName { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var cellAggregate = item as CellAggregateValue;

            if (cellAggregate != null)
            {
                var description = cellAggregate.Description as PropertyAggregateDescription;

                if (description.PropertyName == this.PropertyName && cellAggregate.RowGroup.Type == GroupType.BottomLevel && cellAggregate.ColumnGroup.Type == GroupType.BottomLevel)
                {
                    if (Convert.ToDouble(cellAggregate.Value) >= this.LimitValue)
                    {
                        return this.HigherValueTemplate;
                    }
                    else
                    {
                        return this.LowerValueTemplate;
                    }
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
