using System;
using System.Collections.Generic;
using System.Windows;

namespace ValueGradientColorizer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<PlotInfo3D> data = new List<PlotInfo3D>();
            double maxX = 100;
            double maxY = 80;

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    double xValue = Math.Sin(x * Math.PI / (0.25 * maxX));
                    double yValue = Math.Cos(y * Math.PI / (0.50 * maxY));
                    double z = 200 * xValue * yValue;
                    PlotInfo3D pi = new PlotInfo3D { X = x, Y = y, Z = z, };
                    data.Add(pi);
                }
            }

            this.DataContext = data;
        }
    }
}
