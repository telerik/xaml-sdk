using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DataPointColorizer
{
    public partial class Blend : UserControl
    {
        private double maxX = 100;
        private double maxY = 80;

        public Blend()
        {
            InitializeComponent();

            List<PlotInfo3D> data = new List<PlotInfo3D>();

            for (int x = 0; x < this.maxX; x++)
            {
                for (int y = 0; y < this.maxY; y++)
                {
                    double z = GetZ(x, y);
                    Color itemColor = GetItemColor(x, y);
                    PlotInfo3D pi = new PlotInfo3D { X = x, Y = y, Z = z, ItemColor = itemColor, };
                    data.Add(pi);
                }
            }

            this.DataContext = data;
        }

        private double GetZ(int x, int y)
        {
            double xValue = Math.Sin(x * Math.PI / (0.25 * this.maxX));
            double yValue = Math.Cos(y * Math.PI / (0.50 * this.maxY));

            int c = 50;
            if ((0.25 * this.maxX < x && x < 0.75 * this.maxX) &&
                (0.25 * this.maxY < y && y < 0.75 * this.maxY))
            {
                c = 200;
            }

            double z = c * xValue * yValue;

            return z;
        }

        private System.Windows.Media.Color GetItemColor(double x, double y)
        {
            double xValue = Math.Sin(x * Math.PI / (0.25 * this.maxX));
            double yValue = Math.Cos(y * Math.PI / (0.50 * this.maxY));
            double product = xValue * yValue;

            if (0.8 < product)
            {
                return Colors.Red;
            }
            else if (product < -0.8)
            {
                return Colors.Blue;
            }
            else
            {
                return Colors.Green;
            }
        }
    }
}
