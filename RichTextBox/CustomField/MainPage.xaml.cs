using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.Model;

namespace CustomField
{
    public partial class MainPage : UserControl
    {
        private static readonly string SampleDocumentPath = @"CustomField;component/SampleData/SampleDocument.docx";

        public MainPage()
        {
            InitializeComponent();

            this.LoadSampleDocument();
        }

        private void LoadSampleDocument()
        {
            Stream stream = Application.GetResourceStream(new Uri(SampleDocumentPath, UriKind.RelativeOrAbsolute)).Stream;

            DocxFormatProvider provider = new DocxFormatProvider();

            RadDocument document = provider.Import(stream);

            this.radRichTextBox.Document = document;
        }
    }
}
