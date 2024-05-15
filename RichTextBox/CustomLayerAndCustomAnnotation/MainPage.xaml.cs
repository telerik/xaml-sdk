using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Selection;

namespace CustomLayerAndCustomAnnotation
{
    public partial class MainPage : UserControl
    {
        private bool suppressUpdate = false;

        public MainPage()
        {
            InitializeComponent();
            CreateShowDocument();

            this.radRichTextBox.UILayersBuilder = new CustomUILayersBuilder();
            this.radRichTextBox.DocumentContentChanged += radRichTextBox_DocumentContentChanged;
        }

        private void CreateShowDocument()
        {
            var document = new RadDocument();
            document.LayoutMode = DocumentLayoutMode.Paged;

            RadDocumentEditor editor = new RadDocumentEditor(document);
            editor.Insert("Text Before Text Inside Text After");

            DocumentPosition rangeStartPosition = new DocumentPosition(document);
            rangeStartPosition.MoveToNextWordStart();
            rangeStartPosition.MoveToNextWordStart();

            DocumentPosition rangeEndPosition = new DocumentPosition(rangeStartPosition);
            rangeEndPosition.MoveToNextWordStart();
            rangeEndPosition.MoveToCurrentWordEnd();

            document.Selection.SetSelectionStart(rangeStartPosition);
            document.Selection.AddSelectionEnd(rangeEndPosition);

            editor.InsertAnnotationRange(new CustomRangeStart(), new CustomRangeEnd());

            this.radRichTextBox.Document = document;

            UpdateTextBoxText();
        }

        void radRichTextBox_DocumentContentChanged(object sender, EventArgs e)
        {
            UpdateTextBoxText();
        }

        private void UpdateTextBoxText()
        {
            string textInRange;

            CustomRangeStart rangeStart = this.radRichTextBox.Document.EnumerateChildrenOfType<CustomRangeStart>().FirstOrDefault();
            if (rangeStart != null)
            {
                CustomRangeEnd rangeEnd = (CustomRangeEnd)rangeStart.End;

                DocumentPosition start = new DocumentPosition(this.radRichTextBox.Document);
                start.MoveToInline(rangeStart);

                DocumentPosition end = new DocumentPosition(this.radRichTextBox.Document);
                end.MoveToInline(rangeEnd);

                DocumentSelection selection = new DocumentSelection(this.radRichTextBox.Document);

                selection.SetSelectionStart(start);
                selection.AddSelectionEnd(end);

                string text = selection.GetSelectedText();

                textInRange = text;
            }
            else
            {
                textInRange = string.Empty;
            }

            this.suppressUpdate = true;

            this.textBox.Text = textInRange;

            this.suppressUpdate = false;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (suppressUpdate)
            {
                return;
            }

            CustomRangeStart rangeStart = this.radRichTextBox.Document.EnumerateChildrenOfType<CustomRangeStart>().FirstOrDefault();
            if (rangeStart != null)
            {
                CustomRangeEnd rangeEnd = (CustomRangeEnd)rangeStart.End;

                DocumentPosition start = new DocumentPosition(this.radRichTextBox.Document);
                start.MoveToInline(rangeStart);

                DocumentPosition end = new DocumentPosition(this.radRichTextBox.Document);
                end.MoveToInline(rangeEnd);

                DocumentSelection selection = this.radRichTextBox.Document.Selection;// new DocumentSelection(this.radRichTextBox.Document);

                selection.SetSelectionStart(start);
                selection.AddSelectionEnd(end);

                this.radRichTextBox.Insert(this.textBox.Text);
            }
        }
    }
}
