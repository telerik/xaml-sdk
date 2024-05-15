using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PdfViewerIntegration;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Editing;

namespace PdfViewerIntegration
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            this.ViewModel = new ExampleViewModel();
            InitializeComponent();
        }

        public ExampleViewModel ViewModel
        {
            get
            {
                return (ExampleViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RadFixedDocument document = new RadFixedDocument();
            FixedContentEditor editor = new FixedContentEditor(document.Pages.AddPage());
            editor.DrawText("Hello PdfProcessing!");

            // Use the empty constructor to create a format provider for PdfProcessing
            PdfFormatProvider provider = new PdfFormatProvider();
            MemoryStream stream = new MemoryStream();

            // Export the document to a MemoryStream
            provider.Export(document, stream);

            // Use this stream as a document source for RadPdfViewer
            this.ViewModel.DocumentStream = stream;
        }
    }
}
