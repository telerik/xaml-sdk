using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.Model;

namespace MergeDocuments
{
    public partial class MainPage : UserControl
    {
        private bool isFirst = true;

        public MainPage()
        {
            InitializeComponent();
        }

        private void insertDocument_Click(object sender, RoutedEventArgs e)
        {
            InsertFragmentFromDocument(LoadDocumentToInsert());
        }

        private RadDocument LoadDocumentToInsert()
        {
            RadDocument document = null;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Word Documents (*.docx)|*.docx|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == true)
            {
                string extension;
#if SILVERLIGHT
                extension = ofd.File.Extension.ToLower();
#else
                extension = Path.GetExtension(ofd.SafeFileName).ToLower();
#endif

                IDocumentFormatProvider provider = DocumentFormatProvidersManager.GetProviderByExtension(extension);

                Stream stream;
#if SILVERLIGHT
                stream = ofd.File.OpenRead();
#else
                stream = ofd.OpenFile();
#endif
                using (stream)
                {
                    document = provider.Import(stream);
                }
            }

            return document;
        }

        private void InsertFragmentFromDocument(RadDocument document)
        {
            if (document != null)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    radRichTextBox.Document.InsertSectionBreak(radRichTextBox.Document.CaretPosition,
                                           radRichTextBox.Document.CaretPosition.GetCurrentInline().ExtractStyleFromLocalProperties(), SectionBreakType.NextPage);
                }

                document.Selection.SelectAll();
                DocumentFragment frag = document.Selection.CopySelectedDocumentElements();
                document.Selection.Clear();
                radRichTextBox.Document.InsertFragment(frag);

                CopySectionProperties(document, radRichTextBox.Document);
            }
        }

        private static void CopySectionProperties(RadDocument fromDocument, RadDocument toDocument)
        {
            CopyHeaderAndFooter(fromDocument, toDocument);

            toDocument.Sections.Last.PageOrientation = fromDocument.Sections.Last.PageOrientation;
            toDocument.Sections.Last.PageSize = fromDocument.Sections.Last.PageSize;
            toDocument.Sections.Last.PageMargin = fromDocument.Sections.Last.PageMargin;
        }

        private static void CopyHeaderAndFooter(RadDocument fromDocument, RadDocument toDocument)
        {
            if (!fromDocument.Sections.First.Headers.Default.IsEmpty)
            {
                toDocument.Sections.Last.Headers.Default = fromDocument.Sections.Last.Headers.Default;
            }
            else
            {
                toDocument.Sections.Last.Headers.Default.IsLinkedToPrevious = false;
            }

            if (!fromDocument.Sections.First.Footers.Default.IsEmpty)
            {
                toDocument.Sections.Last.Footers.Default = fromDocument.Sections.Last.Footers.Default;
            }
            else
            {
                toDocument.Sections.Last.Footers.Default.IsLinkedToPrevious = false;
            }
        }
    }
}
