using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace AlternationRowStyleSelector
{
#if !SILVERLIGHT
 public class AlternationRowStyle : System.Windows.Controls.StyleSelector
#else
     public class AlternationRowStyle : Telerik.Windows.Controls.StyleSelector
#endif 
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var items = ((GridViewRow)container).GridViewDataControl.Items;

            var style = new Style(typeof(GridViewRow));

            if (items.IndexOf(item) % 2 == 0)
            {
                style.Setters.Add(new Setter(GridViewRow.BackgroundProperty, new SolidColorBrush(Colors.Gray)));
            }

            return style;
        }

    }
}
