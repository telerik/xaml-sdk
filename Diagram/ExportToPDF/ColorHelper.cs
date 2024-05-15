using System;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Diagrams.Core;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;

namespace ExportToPDF
{
    public static class ColorHelper
    {
        public static ColorBase GetColor(Brush brush, double opacity, Rect bounds, double angle = 0)
        {
            var solidBrush = brush as SolidColorBrush;
            if (solidBrush != null)
            {
                return GetRgbColor(solidBrush.Color, opacity);
            }

            var linerBrush = brush as LinearGradientBrush;
            if (linerBrush != null)
            {
                var startPoint = new Point(bounds.X + linerBrush.StartPoint.X * bounds.Width, bounds.Y + linerBrush.StartPoint.Y * bounds.Height);
                var endPoint = new Point(bounds.X + linerBrush.EndPoint.X * bounds.Width, bounds.Y + linerBrush.EndPoint.Y * bounds.Height);
                LinearGradient gradient = new LinearGradient(startPoint, endPoint);
                foreach (var stop in linerBrush.GradientStops)
                {
                    gradient.GradientStops.Add(new Telerik.Windows.Documents.Fixed.Model.ColorSpaces.GradientStop(GetRgbColor(stop.Color, opacity), stop.Offset));
                }

                gradient.Position.RotateAt(angle, bounds.Center().X, bounds.Center().Y);

                return gradient;
            }

            var radialBrush = brush as RadialGradientBrush;
            if (radialBrush != null)
            {
                var startPoint = new Point(bounds.X + radialBrush.GradientOrigin.X * bounds.Width, bounds.Y + radialBrush.GradientOrigin.Y * bounds.Height);
                var endPoint = new Point(bounds.X + radialBrush.Center.X * bounds.Width, bounds.Y + radialBrush.Center.Y * bounds.Height);
                var radiusX = radialBrush.RadiusX * bounds.Width;
                var radiusY = radialBrush.RadiusY * bounds.Height;
                Point scale = new Point(1, 1);
                if (radiusX > radiusY)
                    scale.X = radiusX / radiusY;
                else if (radiusY > radiusX)
                    scale.Y = radiusY / radiusX;
                RadialGradient gradient = new RadialGradient(startPoint, endPoint, 0, Math.Min(radiusX, radiusY));

                for (int i = 0; i < radialBrush.GradientStops.Count; i++)
                {
                    var stop = radialBrush.GradientStops[i];
                    var color = GetRgbColor(stop.Color, opacity);
                    gradient.GradientStops.Add(new Telerik.Windows.Documents.Fixed.Model.ColorSpaces.GradientStop(color, stop.Offset));
                }

                gradient.Position.ScaleAt(scale.X, scale.Y, bounds.Center().X, bounds.Center().Y);
                gradient.Position.RotateAt(angle, bounds.Center().X, bounds.Center().Y);

                return gradient;
            }

            return RgbColors.Black;
        }

        private static RgbColor GetRgbColor(Color color, double opacity)
        {
            return new RgbColor((byte)(color.A * opacity), color.R, color.G, color.B);
        }
    }
}
