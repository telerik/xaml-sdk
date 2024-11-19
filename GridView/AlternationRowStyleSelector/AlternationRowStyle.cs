using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls.GridView;

namespace AlternationRowStyleSelector
{
    public class AlternationRowStyle : System.Windows.Controls.StyleSelector
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
