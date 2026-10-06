using System.Windows.Controls;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;

namespace SeriesProvider
{
    public partial class SeriesCreated : UserControl
    {
        public SeriesCreated()
        {
            this.InitializeComponent();
        }

        private void ChartSeriesProvider_SeriesCreated(object sender, ChartSeriesCreatedEventArgs e)
        {
            int index = this.chart1.Series.Count;

            if (index == 0)
            {
                LineSeries lineSeries = ((LineSeries)e.Series);
                
                LinearAxis axis = new LinearAxis();
                axis.HorizontalLocation = AxisHorizontalLocation.Right;
                PaletteEntry? entry = this.chart1.Palette.GetEntry(lineSeries, index);
                axis.ElementBrush = entry.Value.Stroke;
                lineSeries.VerticalAxis = axis;
            }
        }
    }
}
