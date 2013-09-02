using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.Model;

namespace CustomImageEditorDialogDemo
{
    public partial class MainPage : UserControl
    {
        private const string SampleDocumentPath = "SampleData/RadRichTextBox.xaml";

        public MainPage()
        {
            InitializeComponent();

            LoadDocument();
            ShowDialog();
        }

        private void ShowDialog()
        {
            ImageInline imageInline = this.radRichTextBox.Document.EnumerateChildrenOfType<ImageInline>().FirstOrDefault();
            if (imageInline != null)
            {
                DocumentPosition start = new DocumentPosition(this.radRichTextBox.Document);
                DocumentPosition end = new DocumentPosition(this.radRichTextBox.Document);
                start.MoveToInline(imageInline);
                end.MoveToPosition(start);
                end.MoveToNext();

                this.radRichTextBox.Document.Selection.AddSelectionStart(start);
                this.radRichTextBox.Document.Selection.AddSelectionEnd(end);

                if (this.radRichTextBox.Document.Selection.GetSelectedSingleInline() is ImageInline)
                {
                    this.radRichTextBox.ShowImageEditorDialog();
                }
            }
        }

        private void LoadDocument()
        {
            using (Stream stream = Application.GetResourceStream(GetResourceUri(SampleDocumentPath)).Stream)
            {
                IDocumentFormatProvider xamlProvider = DocumentFormatProvidersManager.GetProviderByExtension(".xaml");
                this.radRichTextBox.Document = xamlProvider.Import(stream);
            }
        }

        private Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(MainPage).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}
