using System;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace FirstLook
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }

        private void tbCurrentPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
        } 

        private void LoadFromStream(object sender, System.Windows.RoutedEventArgs e)
        {
            Stream str = App.GetResourceStream(new System.Uri("FirstLook;component/SampleData/Sample.pdf", System.UriKind.Relative)).Stream;
            this.pdfViewer.DocumentSource = new PdfDocumentSource(str);
        }

        private void LoadFromUri(object sender, System.Windows.RoutedEventArgs e)
        {
            this.pdfViewer.DocumentSource = new PdfDocumentSource(new System.Uri("FirstLook;component/SampleData/Sample.pdf", System.UriKind.Relative));
        }

    }
}
