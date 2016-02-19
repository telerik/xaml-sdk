using System.Windows;
using Telerik.Charting;
using Telerik.Windows.Controls;

namespace DefaultVisualStyleSelector
{
    public class NegativeStyleSelector : StyleSelector
    {
        public Style ZeroValueStyle { get; set; }
        public Style NegativeValueStyle { get; set; }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            var dp = (CategoricalDataPoint)item;

            if (dp.Value < 0)
            {
                return this.NegativeValueStyle;
            }
            else if (dp.Value == 0)
            {
                return this.ZeroValueStyle;
            }

            return null;
        }
    }
}
