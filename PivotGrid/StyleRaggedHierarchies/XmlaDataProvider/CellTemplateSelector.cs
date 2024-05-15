using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Telerik.Pivot.Core;
using Telerik.Windows.Controls;

namespace XmlaStyleRaggedHierarchies
{
    public class CellTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalFontWeightTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var aggregateValue = item as CellAggregateValue;
            if (aggregateValue != null)
            {
                if ((aggregateValue.RowGroup.Type == GroupType.BottomLevel || aggregateValue.RowGroup.Type == GroupType.Subheading) &&
                    (aggregateValue.ColumnGroup.Type == GroupType.BottomLevel || aggregateValue.ColumnGroup.Type == GroupType.Subheading) &&
                    !aggregateValue.ColumnGroup.HasGroups && !aggregateValue.RowGroup.HasGroups)
                {
                    return this.NormalFontWeightTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
