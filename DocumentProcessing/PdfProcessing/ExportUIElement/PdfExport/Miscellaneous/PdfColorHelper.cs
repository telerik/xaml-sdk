using System.Linq;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Documents.Fixed.Model.Data;
using PdfColors = Telerik.Windows.Documents.Fixed.Model.ColorSpaces;

namespace ExportUIElement
{
    internal static class PdfColorHelper
    {
        internal static PdfColors.ColorBase ConvertBrush(Brush brush, double opacity, IPosition position, double width, double height)
        {
            SolidColorBrush solidBrush = brush as SolidColorBrush;
            if (solidBrush != null)
            {
                return ConvertSolidColorBrush(solidBrush, opacity);
            }

            LinearGradientBrush linearGradientBrush = brush as LinearGradientBrush;
            if (linearGradientBrush != null)
            {
                return ConvertLinearGradientBrush(linearGradientBrush, opacity, position, width, height);
            }

            RadialGradientBrush radialGradientBrush = brush as RadialGradientBrush;
            if (radialGradientBrush != null)
            {
                return ConvertRadialGradientBrush(radialGradientBrush, opacity, position, width, height);
            }

            return null;
        }

        internal static PdfColors.RgbColor ConvertSolidColorBrush(SolidColorBrush brush, double opacity)
        {
            return new PdfColors.RgbColor((byte)(brush.Color.A * opacity), brush.Color.R, brush.Color.G, brush.Color.B);
        }

        internal static PdfColors.LinearGradient ConvertLinearGradientBrush(LinearGradientBrush brush, double opacity, IPosition position, double width, double height)
        {
            Point startPoint = new Point(brush.StartPoint.X * width, brush.StartPoint.Y * height);
            Point endPoint = new Point(brush.EndPoint.X * width, brush.EndPoint.Y * height);

            var pdfGradient = new PdfColors.LinearGradient(startPoint, endPoint);
            pdfGradient.Position = position;

            foreach (GradientStop gradientStop in brush.GradientStops.OrderBy(x => x.Offset))
            {
                var rgbColor = new PdfColors.RgbColor((byte)(gradientStop.Color.A * opacity), gradientStop.Color.R, gradientStop.Color.G, gradientStop.Color.B);
                pdfGradient.GradientStops.Add(new PdfColors.GradientStop(rgbColor, gradientStop.Offset));
            }

            return pdfGradient;
        }

        internal static PdfColors.RadialGradient ConvertRadialGradientBrush(RadialGradientBrush brush, double opacity, IPosition position, double width, double height)
        {
            if (width * height == 0)
            {
                return null;
            }

            var center1 = new Point(brush.GradientOrigin.X * height, brush.GradientOrigin.Y * height);
            var center2 = new Point(brush.Center.X * height, brush.Center.Y * height);
            PdfColors.RadialGradient pdfGradient = new PdfColors.RadialGradient(center1, center2, 0, height / 2);

            var matrix = new Matrix(width / height, 0, 0, 1, 0, 0);
            var newMatrix = matrix = MathHelper.Multiply(position.Matrix, matrix);
            pdfGradient.Position = new Telerik.Windows.Documents.Fixed.Model.Data.MatrixPosition(newMatrix);

            foreach (var gradientStop in brush.GradientStops)
            {
                var rgbColor = new PdfColors.RgbColor((byte)(gradientStop.Color.A * opacity), gradientStop.Color.R, gradientStop.Color.G, gradientStop.Color.B);
                pdfGradient.GradientStops.Add(new PdfColors.GradientStop(rgbColor, gradientStop.Offset));
            }

            return pdfGradient;
        }
    }
}