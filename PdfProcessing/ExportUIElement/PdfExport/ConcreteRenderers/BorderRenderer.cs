using System.Windows;
using System.Windows.Media;

namespace ExportUIElement
{
    internal class BorderRenderer : UIElementRendererBase
    {
        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            var border = element as System.Windows.Controls.Border;
            if (border == null)
            {
                return false;
            }

            if (IsSimpleStrokeThickness(border.BorderThickness))
            {
                RectangleRenderer.DrawRectangle(context, border.Background, border.BorderBrush, border.BorderThickness.Left, border.ActualWidth, border.ActualHeight, null);
            }
            else
            {
                DrawBackground(context, border.Background, border.BorderThickness, border.ActualWidth, border.ActualHeight);
                DrawBorderStroke(context, border.BorderThickness, border.BorderBrush, border.ActualWidth, border.ActualHeight);
            }

            UIElement firstChild = border.Child;
            context.facade.Render(firstChild, context);

            return true;
        }

        private static bool IsSimpleStrokeThickness(Thickness thickness)
        {
            return thickness.Top == thickness.Right &&
                thickness.Top == thickness.Bottom &&
                thickness.Top == thickness.Left;
        }

        private void DrawBackground(PdfRenderContext context, Brush fill, Thickness thickness, double actualWidth, double actualHeight)
        {
            using (context.drawingSurface.SaveGraphicProperties())
            {
                context.drawingSurface.GraphicProperties.IsStroked = false;
                SetFill(context, fill, actualWidth, actualHeight);
                Rect innerRect = new Rect(thickness.Left, thickness.Top, actualWidth - thickness.Left - thickness.Right, actualHeight - thickness.Top - thickness.Bottom);
                context.drawingSurface.DrawRectangle(innerRect);
            }
        }

        private void DrawBorderStroke(PdfRenderContext context, Thickness thickness, Brush stroke, double actualWidth, double actualHeight)
        {
            if (stroke == null)
            {
                return;
            }

            if (thickness.Left != 0)
            {
                LineRenderer.DrawLine(context, thickness.Left / 2, 0, thickness.Left / 2, actualHeight, thickness.Left, stroke, null);
            }
            if (thickness.Top != 0)
            {
                LineRenderer.DrawLine(context, 0, thickness.Top / 2, actualWidth, thickness.Top / 2, thickness.Top, stroke, null);
            }
            if (thickness.Right != 0)
            {
                double x = actualWidth - (thickness.Right / 2);
                LineRenderer.DrawLine(context, x, 0, x, actualHeight, thickness.Right, stroke, null);
            }
            if (thickness.Bottom != 0)
            {
                double y = actualHeight - (thickness.Bottom / 2);
                LineRenderer.DrawLine(context, 0, y, actualWidth, y, thickness.Bottom, stroke, null);
            }
        }
    }
}
