using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CustomHeatMapSourceAndDefinition
{
    public partial class MainWindow : Window
    {
        private static Random randomGenerator = new Random();
        private static List<Color> colors = new List<Color> { Colors.Red, Colors.DarkBlue, Colors.Cornsilk, Colors.DarkGoldenrod, Colors.LightBlue, };

        public MainWindow()
        {
            InitializeComponent();

            CustomHeatMapItem[,] data = this.GetData(2000, 3000);
            CustomHeatMapSource source = new CustomHeatMapSource(data);
            this.heatmap.Definition = new CustomHeatMapDefinition(source);
        }

        private CustomHeatMapItem[,] GetData(int rowsCount, int columnsCount)
        {
            CustomHeatMapItem[,] data = new CustomHeatMapItem[rowsCount, columnsCount];
            for (int row = 0; row < rowsCount; row++)
            {
                for (int column = 0; column < columnsCount; column++)
                {
                    data[row, column] = new CustomHeatMapItem { Value = row + column, Color = colors[randomGenerator.Next(0, colors.Count)] };
                }
            }
            return data;
        }
    }
}
