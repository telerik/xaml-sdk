using System.Windows;
using System.Windows.Controls;
using Telerik.Charting;

namespace DefaultVisualStyleSelector
{
    public class NegativeStyleSelector : StyleSelector
    {
        public Style NegativeStyle { get; set; }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            var dp = (CategoricalDataPoint)item;
            return dp.Value < 0 ? this.NegativeStyle : null;
        }
    }
}
