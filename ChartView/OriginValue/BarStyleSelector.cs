using System.Windows;
using System.Windows.Controls;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;

namespace OriginValue
{
    public class BarStyleSelector : StyleSelector
    {
        public Style AboveOriginStyle { get; set; }
        public Style BelowOriginStyle { get; set; }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            var dp = (CategoricalDataPoint)item;
            var series = (BarSeries)dp.Presenter;
            if (dp.Value >= series.OriginValue)
            {
                return this.AboveOriginStyle;
            }
            else
            {
                return this.BelowOriginStyle;
            }
        }
    }
}
