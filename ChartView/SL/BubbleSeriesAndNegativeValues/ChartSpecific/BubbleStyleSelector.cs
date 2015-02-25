using System.Windows;
using Telerik.Charting;
using Telerik.Windows.Controls;

namespace BubbleSeriesAndNegativeValues
{
    public class BubbleStyleSelector : StyleSelector
    {
        public Style PositiveStyle { get; set; }
        public Style NegativeStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            IBubbleDataPoint dataPoint = (IBubbleDataPoint)item;
            return dataPoint.BubbleSize.Value > 0 ? this.PositiveStyle : this.NegativeStyle;
        }
    }
}
