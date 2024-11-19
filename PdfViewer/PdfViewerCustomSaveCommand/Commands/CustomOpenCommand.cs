using Microsoft.Win32;
using System.IO;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Commands;
using Telerik.Windows.Documents.Fixed;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Import;

namespace PdfViewerCustomSaveCommand.Commands
{
    public class CustomOpenCommand : OpenPdfDocumentCommand
    {
        private readonly Stream stream;

        public CustomOpenCommand(FixedDocumentViewerBase viewer)
            : base(viewer)
        {
        }

        public CustomOpenCommand(RadPdfViewer viewer, System.IO.Stream stream)
            : base(viewer)
        {
            this.stream = stream;
        }

        public void UpdateCanExecute()
        {
            this.OnCanExecuteChanged();
        }

        public override void Execute(object parameter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PDF Files (*.pdf)|*.pdf";
            if (dialog.ShowDialog() == true)
            {
                Stream str = dialog.OpenFile();
                str.CopyTo(stream);
                str.Flush();
                str.Seek(0, SeekOrigin.Begin);
                stream.Flush();
                PdfDocumentSource source = new PdfDocumentSource(str, PdfImportSettings.ReadOnDemand);
                source.Loaded += (s, e) =>
                {
                    str.Close();
                };
                this.Viewer.DocumentSource = source;
            }
        }
    }
}
