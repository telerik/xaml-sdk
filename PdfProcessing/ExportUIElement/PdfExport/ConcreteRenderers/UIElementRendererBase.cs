using System;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Documents.Fixed.Model.Editing;

namespace ExportUIElement
{
    internal abstract class UIElementRendererBase
    {
        internal static IDisposable SaveClip(FixedContentEditor drawingSurface, UIElement element)
        {
            Geometry clip = null;
            FrameworkElement frameworkElement = element as FrameworkElement;
            if (frameworkElement != null)
            {
                clip = System.Windows.Controls.Primitives.LayoutInformation.GetLayoutClip(frameworkElement);
            }            
            if (clip == null)
            {
                clip = element.Clip;
            }

            RectangleGeometry rectangleClip = clip as RectangleGeometry;
            if (rectangleClip == null)
            {
                return null;
            }

            PathGeometry geometry = MathHelper.TransformRectangle(drawingSurface.Position.Matrix, rectangleClip.Rect);
            var pdfGeometry = PdfGeometryHelper.ConvertPathGeometry(geometry);
            return drawingSurface.PushClipping(pdfGeometry);
        }

        internal static IDisposable SaveMatrixPosition(FixedContentEditor drawingSurface, FrameworkElement element)
        {
            if (element == null)
            {
                return null;
            }

            GeneralTransform transform = MathHelper.GetGeneralTransform(element);
            Matrix matrix = MathHelper.CreateMatrix(transform);
            if (matrix.IsIdentity)
            {
                return null;
            }

            matrix = MathHelper.Multiply(matrix, drawingSurface.Position.Matrix);
            IDisposable savePosition = drawingSurface.SavePosition();
            drawingSurface.Position = new Telerik.Windows.Documents.Fixed.Model.Data.MatrixPosition(matrix);
            return savePosition;
        }

        internal static IDisposable SaveOpacity(PdfRenderContext context, double newOpacity)
        {
            if (context.opacity != newOpacity)
            {
                DisposableOpacity disposableOpacity = new DisposableOpacity(context);
                context.opacity = newOpacity;
                return disposableOpacity;
            }
            else
            {
                return null;
            }
        }

        internal static void SetFill(PdfRenderContext context, Brush brush, double width, double height)
        {
            var fill = PdfColorHelper.ConvertBrush(brush, context.opacity, context.drawingSurface.Position, width, height);

            context.drawingSurface.GraphicProperties.IsFilled = fill != null;
            context.drawingSurface.GraphicProperties.FillColor = fill;
        }

        internal static void SetStroke(PdfRenderContext context, double thickness, Brush brush, double width, double height, DoubleCollection dashArray)
        {
            var stroke = PdfColorHelper.ConvertBrush(brush, context.opacity, context.drawingSurface.Position, width, height);
            context.drawingSurface.GraphicProperties.IsStroked = thickness != 0 && stroke != null;

            if (context.drawingSurface.GraphicProperties.IsStroked)
            {
                context.drawingSurface.GraphicProperties.StrokeThickness = thickness;
                context.drawingSurface.GraphicProperties.StrokeColor = stroke;

                if (dashArray != null)
                {
                    context.drawingSurface.GraphicProperties.StrokeDashArray = dashArray;
                }
            }
        }

        internal abstract bool Render(UIElement element, PdfRenderContext context);
    }
}
