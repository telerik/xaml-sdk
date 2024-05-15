using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Telerik.Pivot.Core;
using Telerik.Windows.Controls;

namespace XmlaStyleRaggedHierarchies
{
    public class RowHeaderStyleSelector : StyleSelector
    {
        public System.Windows.Style BottomLevelStyle { get; set; }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            var group = item as IGroup;
            if (group != null)
            {
                // Raggged hierarchies only!!!
                if (!group.HasGroups && group.Type == GroupType.Subheading)
                {
                    return this.BottomLevelStyle;
                }
            }

            return base.SelectStyle(item, container);
        }
    }
}
