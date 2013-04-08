using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls.Charting;

namespace MarkedZoneAnnotation
{
    public partial class OverlappingMarkedZones : UserControl
    {
        public OverlappingMarkedZones()
        {
            InitializeComponent();
            DataSeries series1 = new DataSeries()
            {
                new DataPoint(145),
                new DataPoint(132),
                new DataPoint(164),
                new DataPoint(187),
                new DataPoint(186),
                new DataPoint(131),
                new DataPoint(173),
                new DataPoint(172),
                new DataPoint(140),
                new DataPoint(129),
                new DataPoint(158),
                new DataPoint(149)
            };

            series1.Definition = new LineSeriesDefinition();
            series1.Definition.Appearance.Fill = new SolidColorBrush(Colors.Black);
            series1.Definition.Appearance.Stroke = new SolidColorBrush(Colors.Black);
            series1.Definition.Appearance.PointMark.Stroke = new SolidColorBrush(Colors.Black);
            radChart.DefaultView.ChartArea.DataSeries.Add(series1);
        }
    }
}
