using System.Windows;
using System.Windows.Shapes;

namespace ExportUIElement
{
    internal class PathRenderer : UIElementRendererBase
    {
        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            Path path = element as Path;
            if (path == null)
            {
                return false;
            }

            var pdfGeometry = PdfGeometryHelper.ConvertGeometry(path.Data);
            if (pdfGeometry == null)
            {
                return false;
            }

            using (context.drawingSurface.SaveGraphicProperties())
            {
                SetFill(context, path.Fill, path.ActualWidth, path.ActualHeight);
                SetStroke(context, path.StrokeThickness, path.Stroke, path.ActualWidth, path.ActualHeight, path.StrokeDashArray);

                if (context.drawingSurface.GraphicProperties.IsFilled || context.drawingSurface.GraphicProperties.IsStroked)
                {
                    context.drawingSurface.DrawPath(pdfGeometry);
                }

                return true;
            }
        }
    }
}
