using System.Collections.Generic;
using System.Windows;
using Telerik.Windows.Documents.Fixed.Model.Editing;

namespace ExportUIElement
{
    internal class PdfRenderer
    {
        private List<UIElementRendererBase> renderers = new List<UIElementRendererBase>();

        internal void AddRenderer(UIElementRendererBase renderer)
        {
            this.renderers.Add(renderer);
        }

        internal void Render(UIElement element, FixedContentEditor drawingSurface)
        {
            PdfRenderContext context = new PdfRenderContext(drawingSurface, this);
            this.Render(element, context);
        }

        internal void Render(UIElement element, PdfRenderContext context)
        {
            if (element == null || element.Visibility != Visibility.Visible || element.Opacity == 0)
            {
                return;
            }

            var compositeSave = new CompositeDisposableObject();
            compositeSave.Add(UIElementRendererBase.SaveMatrixPosition(context.drawingSurface, element as FrameworkElement));
            compositeSave.Add(UIElementRendererBase.SaveClip(context.drawingSurface, element));
            compositeSave.Add(UIElementRendererBase.SaveOpacity(context, context.opacity * element.Opacity));

            using (compositeSave)
            {
                foreach (UIElementRendererBase renderer in this.renderers)
                {
                    bool success = renderer.Render(element, context);
                    if (success)
                    {
                        return;
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine("" + element + " could not be exported correctly.");
        }
    }
}
