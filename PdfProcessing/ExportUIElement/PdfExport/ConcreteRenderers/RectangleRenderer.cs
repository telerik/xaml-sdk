using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ExportUIElement
{
    internal class RectangleRenderer : UIElementRendererBase
    {
        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            Rectangle rectangle = element as Rectangle;
            if (rectangle == null)
            {
                return false;
            }

            bool hasStroke = rectangle.Stroke != null && rectangle.StrokeThickness != 0;
            bool isLine = rectangle.ActualWidth == 1 || rectangle.ActualHeight == 1;

            if (isLine && !hasStroke)
            {
                // export the thin Rectangle as a Line to work-around a visual bug in Adobe Reader

                // account for the different rendering in wpf's Rectangle and pdf's line
                Point startPoint;
                Point endPoint;
                if (rectangle.ActualWidth == 1)
                {
                    startPoint = new Point(0.5, 0);
                    endPoint = new Point(0.5, rectangle.ActualHeight);
                }
                else
                {
                    startPoint = new Point(0.5, 0.5);
                    endPoint = new Point(0.5 + rectangle.ActualWidth, 0.5);
                }

                LineRenderer.DrawLine(context, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y, 1, rectangle.Fill, null);
            }
            else
            {
                DrawRectangle(context, rectangle.Fill, rectangle.Stroke, rectangle.StrokeThickness, rectangle.ActualWidth, rectangle.ActualHeight, rectangle.StrokeDashArray);
            }

            return true;
        }

        internal static void DrawRectangle(PdfRenderContext context, Brush fill, Brush stroke, double strokeThickness, double actualWidth, double actualHeight, DoubleCollection dashArray)
        {
            using (context.drawingSurface.SaveGraphicProperties())
            {
                SetFill(context, fill, actualWidth, actualHeight);
                SetStroke(context, strokeThickness, stroke, actualWidth, actualHeight, dashArray);

                if (context.drawingSurface.GraphicProperties.IsFilled || context.drawingSurface.GraphicProperties.IsStroked)
                {
                    // account for the difference in the notion of stroke in wpf's Rectangle/Border and pdf's Rectangle
                    double thickness = context.drawingSurface.GraphicProperties.IsStroked ? strokeThickness : 0;

                    context.drawingSurface.DrawRectangle(new Rect(thickness / 2, thickness / 2, actualWidth - thickness, actualHeight - thickness));
                }
            }
        }
    }
}
