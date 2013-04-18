using System.Collections.Generic;
using System.Windows;
using Telerik.Windows.Controls.Charting;

namespace CustomLineAnnotation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataSeries data = new DataSeries();
            data.AddRange(new List<DataPoint>()
            {
                new DataPoint { XValue = 1, YValue = 1 },
                new DataPoint { XValue = 2, YValue = 5 },
                new DataPoint { XValue = 3, YValue = 6 },
                new DataPoint { XValue = 4, YValue = 9 },
                new DataPoint { XValue = 5, YValue = 5 },
                new DataPoint { XValue = 6, YValue = 7 },
                new DataPoint { XValue = 7, YValue = 12 },
                new DataPoint { XValue = 8, YValue = 10 },
            });
            data.Definition = new LineSeriesDefinition();
            this.radChart.DefaultView.ChartArea.DataSeries.Add(data);
        }
    }
}
