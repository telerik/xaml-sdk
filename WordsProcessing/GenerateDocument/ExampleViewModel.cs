using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Styles;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace GenerateDocument
{
    public class ExampleViewModel : ViewModelBase
    {
        private static readonly ThemableColor GreenColor = new ThemableColor(Color.FromArgb(255, 92, 230, 0));

        private ICommand generateCommand = null;
        public ICommand GenerateCommand
        {
            get
            {
                return this.generateCommand;
            }
            set
            {
                if (this.generateCommand != value)
                {
                    this.generateCommand = value;
                    this.OnPropertyChanged("GenerateCommand");
                }
            }
        }

        private IEnumerable<string> exportFormats;
        public IEnumerable<string> ExportFormats
        {
            get
            {
                if (exportFormats == null)
                {
                    exportFormats = new string[] { "Docx", "Rtf", "Txt" };
                }

                return exportFormats;
            }
        }

        private string selectedExportFormat;
        public string SelectedExportFormat
        {
            get
            {
                return selectedExportFormat;
            }
            set
            {
                if (!object.Equals(selectedExportFormat, value))
                {
                    selectedExportFormat = value;

                    this.OnPropertyChanged("SelectedExportFormat");
                }
            }
        }

        private bool includeHeader = true;
        public bool IncludeHeader
        {
            get
            {
                return this.includeHeader;
            }
            set
            {
                if (this.includeHeader != value)
                {
                    this.includeHeader = value;
                    this.OnPropertyChanged("IncludeHeader");
                }
            }
        }

        private bool includeFooter = true;
        public bool IncludeFooter
        {
            get
            {
                return this.includeFooter;
            }
            set
            {
                if (this.includeFooter != value)
                {
                    this.includeFooter = value;
                    this.OnPropertyChanged("IncludeFooter");
                }
            }
        }
        public ExampleViewModel()
        {
            this.GenerateCommand = new DelegateCommand(this.Generate);
        }

        private void Generate(object obj)
        {
            RadFlowDocument document = this.CreateDocument();

            string selectedFromat = this.SelectedExportFormat;
            FileHelper.SaveDocument(document, selectedFromat);
        }

        private RadFlowDocument CreateDocument()
        {
            RadFlowDocument document = new RadFlowDocument();
            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(document);
            editor.ParagraphFormatting.TextAlignment.LocalValue = Alignment.Justified;

            // Body
            editor.InsertLine("Dear Telerik User,");
            editor.InsertText("We’re happy to introduce the new Telerik RadWordsProcessing component for WPF. High performance library that enables you to read, write and manipulate documents in DOCX, RTF and plain text format. The document model is independent from UI and ");
            Run run = editor.InsertText("does not require");
            run.Underline.Pattern = UnderlinePattern.Single;
            editor.InsertLine(" Microsoft Office.");

            editor.InsertText("The current community preview version comes with full rich-text capabilities including ");
            editor.InsertText("bold, ").FontWeight = FontWeights.Bold;
            editor.InsertText("italic, ").FontStyle = FontStyles.Italic;
            editor.InsertText("underline,").Underline.Pattern = UnderlinePattern.Single;
            editor.InsertText(" font sizes and ").FontSize = 20;
            editor.InsertText("colors ").ForegroundColor = GreenColor;

            editor.InsertLine("as well as text alignment and indentation. Other options include tables, hyperlinks, inline and floating images. Even more sweetness is added by the built-in styles and themes.");

            editor.InsertText("Here at Telerik we strive to provide the best services possible and fulfill all needs you as a customer may have. We would appreciate any feedback you send our way through the ");
            editor.InsertHyperlink("public forums", "http://www.telerik.com/forums", false, "Telerik Forums");
            editor.InsertLine(" or support ticketing system.");

            editor.InsertLine("We hope you’ll enjoy RadWordsProcessing as much as we do. Happy coding!");
            editor.InsertParagraph();
            editor.InsertText("Kind regards,");

            this.CreateSignature(editor);

            this.CreateHeader(editor);

            this.CreateFooter(editor);

            return document;
        }
  
        private void CreateSignature(RadFlowDocumentEditor editor)
        {
            Table signatureTable = editor.InsertTable(1, 2);
            signatureTable.Rows[0].Cells[0].Borders = new TableCellBorders(
                new Border(BorderStyle.None),
                new Border(BorderStyle.None),
                new Border(4, BorderStyle.Single, GreenColor),
                new Border(BorderStyle.None));

            // Create paragraph with image
            signatureTable.Rows[0].Cells[0].PreferredWidth = new TableWidthUnit(140);
            Paragraph paragraphWithImage = signatureTable.Rows[0].Cells[0].Blocks.AddParagraph();
            paragraphWithImage.Spacing.SpacingAfter = 0;
            editor.MoveToParagraphStart(paragraphWithImage);
            using (Stream stream = FileHelper.GetSampleResourceStream("Telerik_logo.png"))
            {
                editor.InsertImageInline(stream, "png", new Size(118, 28));
            }

            // Create cell with name and position
            signatureTable.Rows[0].Cells[1].Padding = new Telerik.Windows.Documents.Primitives.Padding(12, 0, 0, 0);
            Paragraph cellParagraph = signatureTable.Rows[0].Cells[1].Blocks.AddParagraph();
            cellParagraph.Spacing.SpacingAfter = 0;
            editor.MoveToParagraphStart(cellParagraph);
            editor.CharacterFormatting.FontSize.LocalValue = 12;

            editor.InsertText("Jane Doe").FontWeight = FontWeights.Bold;
            editor.InsertParagraph().Spacing.SpacingAfter = 0;
            editor.InsertText("Support Officer");
        }

        private void CreateFooter(RadFlowDocumentEditor editor)
        {
            if (this.IncludeFooter)
            {
                Footer footer = editor.Document.Sections.First().Footers.Add();
                Paragraph paragraph = footer.Blocks.AddParagraph();
                paragraph.TextAlignment = Alignment.Right;

                editor.MoveToParagraphStart(paragraph);
                editor.InsertHyperlink("telerik.com", "http://www.telerik.com", false, "Telerik Side");
            }
        }

        private void CreateHeader(RadFlowDocumentEditor editor)
        {
            if (this.IncludeHeader)
            {
                Header header = editor.Document.Sections.First().Headers.Add();
                editor.MoveToParagraphStart(header.Blocks.AddParagraph());
                using (Stream stream = FileHelper.GetSampleResourceStream("Telerik_develop_experiences.png"))
                {
                    editor.InsertImageInline(stream, "png", new Size(660, 237));
                }
            }
        }
    }
}
