using Telerik.Windows.Documents.Fixed.Model.Editing;

namespace ExportUIElement
{
    internal class DisposableOpacity : System.IDisposable
    {
        PdfRenderContext context;
        double opacity;

        internal DisposableOpacity(PdfRenderContext context)
        {
            this.context = context;
            this.opacity = context.opacity;
        }

        public void Dispose()
        {
            if (this.context != null)
            {
                this.context.opacity = this.opacity;
                this.context = null;
            }
        }
    }
}
