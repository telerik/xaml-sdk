using System;
using System.IO;
using System.Linq;
#if SILVERLIGHT
using System.Windows.Controls;
#endif
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Commands;
using Telerik.Windows.Documents.Fixed;
using Telerik.Windows.Documents.Fixed.FormatProviders;
#if WPF
using Microsoft.Win32;
#endif

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
#if SILVERLIGHT
                FileStream str = dialog.File.OpenRead();
#elif WPF
                Stream str = dialog.OpenFile();
#endif              
                str.CopyTo(stream);
                str.Flush();
                str.Seek(0, SeekOrigin.Begin);
                stream.Flush();
                PdfDocumentSource source = new PdfDocumentSource(str, FormatProviderSettings.ReadOnDemand);
                source.Loaded += (s, e) =>
                {
                    str.Close();
                };
                this.Viewer.DocumentSource = source;
            }
        }
    }
}
