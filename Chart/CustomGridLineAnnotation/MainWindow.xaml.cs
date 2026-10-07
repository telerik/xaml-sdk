using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Charting;

namespace CustomGridLineAnnotation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            InitializeComponent();
            var itemsSource = new List<ChartData>()
            {
                new ChartData { Date = new DateTime(2008, 07, 8), Value = 145 },
                new ChartData { Date = new DateTime(2008, 08, 8), Value = 132 },
                new ChartData { Date = new DateTime(2008, 09, 8), Value = 164 },
                new ChartData { Date = new DateTime(2008, 10, 8), Value = 187 },
                new ChartData { Date = new DateTime(2008, 11, 8), Value = 186 },
                new ChartData { Date = new DateTime(2008, 12, 8), Value = 131 },
                new ChartData { Date = new DateTime(2009, 01, 8), Value = 173 },
                new ChartData { Date = new DateTime(2009, 02, 8), Value = 172 },
                new ChartData { Date = new DateTime(2009, 03, 8), Value = 140 },
                new ChartData { Date = new DateTime(2009, 04, 8), Value = 129 },
                new ChartData { Date = new DateTime(2009, 05, 8), Value = 149 },
                new ChartData { Date = new DateTime(2009, 06, 8), Value = 158 },
                new ChartData { Date = new DateTime(2009, 07, 8), Value = 145 },
                new ChartData { Date = new DateTime(2009, 08, 8), Value = 132 },
                new ChartData { Date = new DateTime(2009, 09, 8), Value = 164 },
                new ChartData { Date = new DateTime(2009, 10, 8), Value = 187 },
                new ChartData { Date = new DateTime(2009, 11, 8), Value = 186 },
                new ChartData { Date = new DateTime(2009, 12, 8), Value = 131 },
                new ChartData { Date = new DateTime(2010, 01, 8), Value = 173 },
                new ChartData { Date = new DateTime(2010, 02, 8), Value = 172 },
                new ChartData { Date = new DateTime(2010, 03, 8), Value = 140 },
                new ChartData { Date = new DateTime(2010, 04, 8), Value = 129 },
                new ChartData { Date = new DateTime(2010, 05, 8), Value = 158 },
                new ChartData { Date = new DateTime(2010, 06, 8), Value = 149 },
            };
            SeriesMapping series = new SeriesMapping();
            series.ItemsSource = itemsSource;
            series.SeriesDefinition = new LineSeriesDefinition();
            series.ItemMappings.Add(new ItemMapping { FieldName = "Date", DataPointMember = DataPointMember.XValue });
            series.ItemMappings.Add(new ItemMapping { FieldName = "Value", DataPointMember = DataPointMember.YValue });
            this.radChart.SeriesMappings.Add(series);
            this.radChart.DefaultView.ChartArea.AxisX.DefaultLabelFormat = "MMM dd";
            this.radChart.DefaultView.ChartArea.AxisX.LabelRotationAngle = 45;
        }
    }
}
