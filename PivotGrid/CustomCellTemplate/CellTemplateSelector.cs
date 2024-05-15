using System;
using System.Windows;
using System.Windows.Controls;
using Telerik.Pivot.Core;
using Telerik.Windows.Controls;

namespace CustomCellTemplate
{
    public class CellTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RedTemplate { get; set; }
        public DataTemplate GreenTemplate { get; set; }
        public CellAggregateValue MaxCellAggregateValue { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var cellAggregate = item as CellAggregateValue;

            if (cellAggregate != null)
            {
                var description = cellAggregate.Description as PropertyAggregateDescription;

                if (description.PropertyName == "Net" && cellAggregate.RowGroup.Type == GroupType.BottomLevel && cellAggregate.ColumnGroup.Type == GroupType.BottomLevel)
                {
                    if (Convert.ToDouble(cellAggregate.Value) > 1000d)
                    {
                        return this.GreenTemplate;
                    }
                    else
                    {
                        return this.RedTemplate;
                    }
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}