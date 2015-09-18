using System.Windows;
using System.Windows.Shapes;

namespace ExportUIElement
{
    internal class EllipseRenderer : UIElementRendererBase
    {
        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            Ellipse ellipse = element as Ellipse;
            if (ellipse == null)
            {
                return false;
            }

            using (context.drawingSurface.SaveGraphicProperties())
            {
                SetStroke(context, ellipse.StrokeThickness, ellipse.Stroke, ellipse.ActualWidth, ellipse.ActualHeight, ellipse.StrokeDashArray);
                SetFill(context, ellipse.Fill, ellipse.ActualWidth, ellipse.ActualHeight);

                if (context.drawingSurface.GraphicProperties.IsFilled || context.drawingSurface.GraphicProperties.IsStroked)
                {
                    context.drawingSurface.DrawEllipse(new Point(ellipse.ActualWidth / 2, ellipse.ActualHeight / 2), ellipse.ActualWidth / 2, ellipse.ActualHeight / 2);
                }
            }

            return true;
        }
    }
}
