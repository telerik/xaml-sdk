using System;
using Telerik.Charting;

namespace BubbleSeriesAndNegativeValues
{
    public class CustomBubbleSizeSelector : ChartBubbleSizeSelector
    {
        public static double SelectBubbleSize(double bubbleSize)
        {
            return Math.Abs(bubbleSize) / 1000;
        }

        public override RadSize SelectBubbleSize(IBubbleDataPoint dataPoint)
        {
            double size = SelectBubbleSize(dataPoint.BubbleSize.Value);
            return new RadSize(size, size);
        }
    }
}
