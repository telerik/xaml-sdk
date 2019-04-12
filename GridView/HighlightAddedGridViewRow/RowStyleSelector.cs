using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace HighlightAddedGridViewRow
{
    public class RowStyleSelector : StyleSelector
    {
        public Style NewRowStyle { get; set; }
        public Style DefaultStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            Sample sample = item as Sample;
            var gridViewRow = container as GridViewRow;
            if (sample == null || gridViewRow == null || NewRowStyle == null)
            {
                return base.SelectStyle(item, container);
            }

            if (sample.IsNew)
            {
                return NewRowStyle;
            }
            
            return DefaultStyle;
        }
    }
}
