using System;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Commands;

namespace CustomCommandDescriptor
{
    public class FitToWidthCommand : FixedDocumentViewerCommandBase
    {
        private const double PageMargin = 20;

        public FitToWidthCommand(FixedDocumentViewerBase fixedDocumentViewerBase)
            : base(fixedDocumentViewerBase)
        {
        }

        public override void Execute(object parameter)
        {
            double width = this.Viewer.ActualWidth - 2 * PageMargin;
            double pageWidth = this.Viewer.CurrentPage.ActualWidth;
            this.Viewer.ScaleFactor = width / pageWidth;
        }
    }
}
