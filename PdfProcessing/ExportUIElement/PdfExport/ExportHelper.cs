using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents.Fixed.Model.Editing;

namespace ExportUIElement
{
    public static class ExportHelper
    {
        private static PdfRenderer pdfRenderer;

        static ExportHelper()
        {
            pdfRenderer = new PdfRenderer();
            SetUp(pdfRenderer);
        }

        public static void ExportToPdf(UIElement element, FixedContentEditor drawingSurface)
        {
            pdfRenderer.Render(element, drawingSurface);
        }

        private static void SetUp(PdfRenderer renderer)
        {
            renderer.AddRenderer(new PanelRenderer());
            renderer.AddRenderer(new TextBlockRenderer());
            renderer.AddRenderer(new BorderRenderer());
            renderer.AddRenderer(new RectangleRenderer());
            renderer.AddRenderer(new EllipseRenderer());
            renderer.AddRenderer(new LineRenderer());
            renderer.AddRenderer(new ImageRenderer());
#if WPF
            renderer.AddRenderer(new ShapeRenderer());
            renderer.AddRenderer(new FrameworkElementRenderer(
                typeof(ContentPresenter), 
                typeof(Control), 
                typeof(ItemsPresenter),
                typeof(System.Windows.Documents.AdornerLayer)));
#elif SILVERLIGHT
            renderer.AddRenderer(new PathRenderer());
            renderer.AddRenderer(new FrameworkElementRenderer(
                typeof(ContentPresenter),
                typeof(Control),
                typeof(ItemsPresenter)));
#endif

        }
    }
}
