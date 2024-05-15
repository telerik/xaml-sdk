using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace ReadOnlyStyleSelector
{
    public class ReadOnlyStyle : StyleSelector
    {
        public Style ReadOnlyStyleProperty { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var column = (container as GridViewCellBase).Column as GridViewDataColumn;
            if (column != null)
            {
                return column.CanEdit(item) ? base.SelectStyle(item, container) : this.ReadOnlyStyleProperty;
            }

            return base.SelectStyle(item, container);
        }
    }
}
