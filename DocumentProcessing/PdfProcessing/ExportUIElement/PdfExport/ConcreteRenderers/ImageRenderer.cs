using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging;

namespace ExportUIElement
{
    internal class ImageRenderer : UIElementRendererBase
    {
        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            var image = element as System.Windows.Controls.Image;
            if (image == null)
            {
                return false;
            }

            if (image.ActualWidth > 0 && image.ActualHeight > 0)
            {
                MemoryStream stream = new MemoryStream();
                Telerik.Windows.Media.Imaging.ExportExtensions.ExportToImage(image, stream, new PngBitmapEncoder());
                context.drawingSurface.DrawImage(stream);
            }

            return true;
        }
    }
}
