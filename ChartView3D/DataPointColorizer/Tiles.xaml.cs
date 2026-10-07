using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DataPointColorizer
{
    public partial class Tiles : UserControl
    {
        public Tiles()
        {
            InitializeComponent();

            List<PlotInfo3D> data = new List<PlotInfo3D>();
            double maxX = 10;
            double maxY = 10;

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    double xValue = Math.Sin(x * Math.PI / maxX);
                    double yValue = Math.Cos(y * Math.PI / maxY);
                    double z = 200 * xValue * yValue;
                    PlotInfo3D pi = new PlotInfo3D { X = x, Y = y, Z = z, ItemColor = GetItemColor(z) };
                    data.Add(pi);
                }
            }

            this.DataContext = data;
        }

        private Color GetItemColor(double z)
        {
            double relativePosition = Math.Abs(z / 200);
            Color startColor = Colors.DarkGreen;
            Color endColor = z > 0 ? Colors.DarkRed : Colors.Blue;
            return MixColors(startColor, endColor, relativePosition);
        }

        private static Color MixColors(Color startColor, Color endColor, double position)
        {
            byte alpha = Convert.ToByte(startColor.A + (position * (endColor.A - startColor.A)));
            byte red = Convert.ToByte(startColor.R + (position * (endColor.R - startColor.R)));
            byte green = Convert.ToByte(startColor.G + (position * (endColor.G - startColor.G)));
            byte blue = Convert.ToByte(startColor.B + (position * (endColor.B - startColor.B)));

            return new Color { A = alpha, R = red, G = green, B = blue, };
        }
    }
}
