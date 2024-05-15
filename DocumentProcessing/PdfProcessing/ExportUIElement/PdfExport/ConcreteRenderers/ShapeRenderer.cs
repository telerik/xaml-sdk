using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ExportUIElement
{
    internal class ShapeRenderer : UIElementRendererBase
    {
        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            Shape shape = element as Shape;
            if (shape == null)
            {
                return false;
            }

            Type type = shape.GetType();
            PropertyInfo geometryProperty = type.GetProperty("DefiningGeometry", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            Geometry geometry = (Geometry)geometryProperty.GetValue(shape, null);
            var pdfGeometry = PdfGeometryHelper.ConvertGeometry(geometry);
            if (pdfGeometry == null)
            {
                return false;
            }

            using (context.drawingSurface.SaveGraphicProperties())
            {
                SetFill(context, shape.Fill, shape.ActualWidth, shape.ActualHeight);
                SetStroke(context, shape.StrokeThickness, shape.Stroke, shape.ActualWidth, shape.ActualHeight, shape.StrokeDashArray);

                if (context.drawingSurface.GraphicProperties.IsFilled || context.drawingSurface.GraphicProperties.IsStroked)
                {
                    context.drawingSurface.DrawPath(pdfGeometry);
                }
                
                return true;
            }
        }
    }
}
