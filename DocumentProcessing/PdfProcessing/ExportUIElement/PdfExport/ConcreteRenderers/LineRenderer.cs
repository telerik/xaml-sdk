using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ExportUIElement
{
    internal class LineRenderer : UIElementRendererBase
    {
        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            Line line = element as Line;
            if (line == null)
            {
                return false;
            }

            DrawLine(context, line.X1, line.Y1, line.X2, line.Y2, line.StrokeThickness, line.Stroke, line.StrokeDashArray);

            return true;
        }

        internal static void DrawLine(PdfRenderContext context, double x1, double y1, double x2, double y2, double strokeThickness, Brush stroke, DoubleCollection dashArray)
        {
            using (context.drawingSurface.SaveGraphicProperties())
            {
                if (x1 == x2 && System.Math.Abs(y1 - y2) > 10)
                {
                    if (y2 < y1)
                    {
                        double max = y1;
                        y1 = y2;
                        y2 = max;
                    }

                    // offset the start position of the line to resolve a visual bug in Adobe Reader, where the axis line seems to continue after the last tick
                    y1 += 0.5;
                }

                SetStroke(context, strokeThickness, stroke, Math.Abs(x2 - x1), Math.Abs(y2 - y1), dashArray);
                if (context.drawingSurface.GraphicProperties.IsStroked)
                {
                    context.drawingSurface.DrawLine(new Point(x1, y1), new Point(x2, y2));
                }
            }
        }
    }
}
