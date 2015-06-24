using System;
using System.IO;
using System.Linq;
#if SILVERLIGHT
using System.Windows.Controls;
#endif
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Commands;
#if WPF
using Microsoft.Win32;
#endif

namespace PdfViewerCustomSaveCommand.Commands
{
    public class CustomSaveCommand : FixedDocumentViewerCommandBase
    {
        private readonly Stream documentStream;

        public CustomSaveCommand(RadPdfViewer viewer)
            : base(viewer)
        {
        }

        public CustomSaveCommand(RadPdfViewer viewer, Stream stream)
            : base(viewer)
        {
            this.documentStream = stream;
        }

        public override bool CanExecuteOverride(object parameter)
        {
            return documentStream != null && documentStream.Length > 0;
        }

        public override void Execute(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream saveStream = saveFileDialog.OpenFile())
                {
                    documentStream.Seek(0, SeekOrigin.Begin);
                    documentStream.CopyTo(saveStream);
                    documentStream.Flush();
                    saveStream.Flush();
                }
            }
        }
    }
}
