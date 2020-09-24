using System.Windows;
using System.Windows.Controls;

namespace ExportUIElement
{
    internal class PanelRenderer : FrameworkElementRenderer
    {
        internal PanelRenderer()
            : base(typeof(Panel))
        {
        }

        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            Panel panel = element as Panel;
            if (panel == null)
            {
                return false;
            }

            RectangleRenderer.DrawRectangle(context, panel.Background, null, 0, panel.ActualWidth, panel.ActualHeight, null);

            return base.Render(panel, context);
        }
    }
}
