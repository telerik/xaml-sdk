using System.Collections.Generic;
using System.Windows;
using Telerik.Windows.Controls.Charting;

namespace CategoricalCharts
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SeriesMapping seriesMapping = new SeriesMapping();
            seriesMapping.ItemsSource = new List<FruitSales>()
            {
                new FruitSales { Fruit = "Apples", Orders = 13 },
                new FruitSales { Fruit = "Oranges", Orders = 33 },
                new FruitSales { Fruit = "Grapes", Orders = 52 },
            };
            seriesMapping.LegendLabel = "Exported Fruits";
            seriesMapping.SeriesDefinition = new BarSeriesDefinition();
            ItemMapping itemMapping = new ItemMapping();
            itemMapping.DataPointMember = DataPointMember.XCategory;
            itemMapping.FieldName = "Fruit";
            seriesMapping.ItemMappings.Add(itemMapping);
            itemMapping = new ItemMapping();
            itemMapping.DataPointMember = DataPointMember.YValue;
            itemMapping.FieldName = "Orders";
            seriesMapping.ItemMappings.Add(itemMapping);
            radChart.SeriesMappings.Add(seriesMapping);
        }
    }
}
