using System;
using System.IO;
using System.Linq;
using System.Windows;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.Model;

namespace CustomField
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string SampleDocumentPath = @"CustomField;component/SampleData/SampleDocument.docx";

        public MainWindow()
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
