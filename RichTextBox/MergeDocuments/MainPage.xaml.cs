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
                    this.radRichTextBox.InsertSectionBreak(SectionBreakType.NextPage);
                }

                document.Selection.SelectAll();
                DocumentFragment frag = new DocumentFragment(document.Selection);
                radRichTextBox.InsertFragment(frag);

                CopySectionProperties(document, radRichTextBox.Document);
            }
        }

        private static void CopySectionProperties(RadDocument fromDocument, RadDocument toDocument)
        {
            CopyHeaderAndFooter(fromDocument, toDocument);

            RadDocumentEditor documentEditor = new RadDocumentEditor(toDocument);
            documentEditor.Document.CaretPosition.MoveToLastPositionInDocument();

            documentEditor.ChangeSectionPageOrientation(fromDocument.Sections.Last.PageOrientation);
            documentEditor.ChangeSectionPageSize(fromDocument.Sections.Last.PageSize);
            documentEditor.ChangeSectionPageMargin(fromDocument.Sections.Last.PageMargin);
            documentEditor.ChangeSectionFooterBottomMargin(fromDocument.Sections.Last.FooterBottomMargin);
            documentEditor.ChangeSectionHeaderTopMargin(fromDocument.Sections.Last.HeaderTopMargin);
        }

        private static void CopyHeaderAndFooter(RadDocument fromDocument, RadDocument toDocument)
        {
            RadDocumentEditor documentEditor = new RadDocumentEditor(toDocument);
            if (!fromDocument.Sections.First.Headers.Default.IsEmpty)
            {
                documentEditor.ChangeSectionHeader(documentEditor.Document.Sections.First, HeaderFooterType.Default, fromDocument.Sections.Last.Headers.Default);
            }
            else
            {
                documentEditor.ChangeSectionHeaderLinkToPrevious(documentEditor.Document.Sections.Last, HeaderFooterType.Default, false);
            }

            if (!fromDocument.Sections.First.Footers.Default.IsEmpty)
            {
                documentEditor.ChangeSectionFooter(documentEditor.Document.Sections.Last, HeaderFooterType.Default, fromDocument.Sections.Last.Footers.Default);
            }
            else
            {
                documentEditor.ChangeSectionFooterLinkToPrevious(documentEditor.Document.Sections.Last, HeaderFooterType.Default, false);
            }
        }
    }
}
