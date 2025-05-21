using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Import;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace AddDocumentContent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            ExtensibilityManager.RegisterSignaturePropertiesDialog(new SignaturePropertiesDialog());
            ExtensibilityManager.RegisterSignSignatureDialog(new SignSignatureDialog());

            CustomUILayersBuilder uILayersBuilder = new CustomUILayersBuilder();
            ExtensibilityManager.RegisterLayersBuilder(uILayersBuilder);

            this.InitializeComponent();

            this.OpenFile(File.OpenRead("../../SampleData/Sample.pdf"));
        }

        private void tbCurrentPage_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == Key.Enter)
                {
                    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
        }

        private void OpenRadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF documents|*.pdf";

            if (openFileDialog.ShowDialog() == true)
            {
                OpenFile(openFileDialog.OpenFile());
            }
        }

        private void OpenFile(Stream stream)
        {
            using (stream)
            {
                PdfFormatProvider pdfFormatProvider = new PdfFormatProvider();
                pdfFormatProvider.ImportSettings = PdfImportSettings.ReadOnDemand;
                RadFixedDocument document = pdfFormatProvider.Import(stream, null);

                this.pdfViewer.Document = document;
            }
        }

        private void SaveRadButton_Click(object sender, RoutedEventArgs e)
        {
            PdfFormatProvider provider = new PdfFormatProvider();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF documents|*.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                {
                    provider.Export(this.pdfViewer.Document, stream, null);
                }
            }
        }
    }
}
