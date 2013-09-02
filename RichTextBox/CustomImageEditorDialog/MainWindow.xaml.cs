using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.Model;

namespace CustomImageEditorDialogDemoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string SampleDocumentPath = "SampleData/RadRichTextBox.xaml";

        public MainWindow()
        {
            InitializeComponent();
            this.radRichTextBox.Loaded += radRichTextBox_Loaded;
        }

        void radRichTextBox_Loaded(object sender, RoutedEventArgs e)
        {
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

        private static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(MainWindow).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}
