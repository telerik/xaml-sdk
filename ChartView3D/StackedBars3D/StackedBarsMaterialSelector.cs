using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;

namespace StackedBars3D
{
    public class StackedBarsMaterialSelector : MaterialSelector
    {
        public override Material SelectMaterial(object context)
        {
            var dp = (XyzDataPoint3D)context;            
            var dataItem = (PlotInfo)dp.DataItem;

            var brush = new LinearGradientBrush() { StartPoint = new Point(0.5, 0), EndPoint = new Point(0.5, 1) };
            var zValue = dataItem.ZValue;

            double currentOffset = 0;
            for (int i = 0; i < dataItem.StackedZValues.Count; i++)
            {
                var paletteBrush = (SolidColorBrush)ChartPalettes.Windows8.GlobalEntries[i % ChartPalettes.Windows8.GlobalEntries.Count].Fill;

                currentOffset += dataItem.StackedZValues[i] / zValue;

                if (brush.GradientStops.Count > 0)
                {
                    brush.GradientStops.Add(new GradientStop(paletteBrush.Color, brush.GradientStops[brush.GradientStops.Count - 1].Offset));
                }

                var stop = new GradientStop(paletteBrush.Color, currentOffset);
                brush.GradientStops.Add(stop);
            }
            
            return new DiffuseMaterial(brush);
        }
    }
}
