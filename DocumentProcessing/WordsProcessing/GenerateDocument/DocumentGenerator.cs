using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
#if NETCOREAPP
using Telerik.Documents.Common.Model;
using Telerik.Documents.Primitives;
using Telerik.Documents.Media;
using Telerik.Documents.Core.Fonts;
#else
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Documents.Spreadsheet.Model;
#endif
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.Flow.FormatProviders.Txt;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Styles;

namespace GenerateDocument
{
    public class DocumentGenerator
    {
        private static readonly ThemableColor greenColor = new ThemableColor(Color.FromArgb(255, 92, 230, 0));
        private static readonly ThemableColor yellowColor = new ThemableColor(Color.FromArgb(255, 255, 255, 0));
        private readonly string sampleDataFolder = "SampleData/";

        private string selectedExportFormat;

        public string SelectedExportFormat
        {
            get
            {
                return this.selectedExportFormat;
            }
            set
            {
                if (!object.Equals(this.selectedExportFormat, value))
                {
                    this.selectedExportFormat = value;
                }
            }
        }

        public DocumentGenerator()
            : this("docx")
        {
        }

        public DocumentGenerator(string documentFormat)
        {
            this.SelectedExportFormat = documentFormat;
        }

        public void Generate()
        {
            RadFlowDocument document = this.CreateDocument();

            this.SaveDocument(document, this.SelectedExportFormat);
        }

        private RadFlowDocument CreateDocument()
        {
            RadFlowDocument document = new RadFlowDocument();
            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(document);
            editor.ParagraphFormatting.TextAlignment.LocalValue = Alignment.Justified;

            // Body
            editor.InsertLine("Dear Telerik User,");
            editor.InsertText("We’re happy to introduce the Telerik RadWordsProcessing component. High performance library that enables you to read, write and manipulate documents in DOCX, RTF and plain text format. The document model is independent from UI and ");
            Run run = editor.InsertText("does not require");
            run.Underline.Pattern = UnderlinePattern.Single;
            editor.InsertLine(" Microsoft Office.");

            editor.InsertText("The current community preview version comes with full rich-text capabilities including ");
            editor.InsertText("bold, ").FontWeight = FontWeights.Bold;
            editor.InsertText("italic, ").FontStyle = FontStyles.Italic;
            editor.InsertText("underline,").Underline.Pattern = UnderlinePattern.Single;
            editor.InsertText(" font sizes and ").FontSize = Telerik.Windows.Documents.Media.Unit.PointToDip(20);
            Run coloredRun = editor.InsertText("colors ");
            coloredRun.ForegroundColor = greenColor;
            coloredRun.Shading.BackgroundColor = yellowColor;

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
                new Border(4, BorderStyle.Single, greenColor),
                new Border(BorderStyle.None));

            // Create paragraph with image
            signatureTable.Rows[0].Cells[0].PreferredWidth = new TableWidthUnit(140);
            Paragraph paragraphWithImage = signatureTable.Rows[0].Cells[0].Blocks.AddParagraph();
            paragraphWithImage.Spacing.SpacingAfter = 0;
            editor.MoveToParagraphStart(paragraphWithImage);

            using (Stream stream = File.OpenRead(this.sampleDataFolder + "Telerik_logo.png"))
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
            Footer footer = editor.Document.Sections.First().Footers.Add();
            Paragraph paragraph = footer.Blocks.AddParagraph();
            paragraph.TextAlignment = Alignment.Right;
            paragraph.Inlines.AddRun("www.telerik.com");

            editor.MoveToParagraphStart(paragraph);
        }

        private void CreateHeader(RadFlowDocumentEditor editor)
        {
            Header header = editor.Document.Sections.First().Headers.Add();
            editor.MoveToParagraphStart(header.Blocks.AddParagraph());

            using (Stream stream = File.OpenRead(this.sampleDataFolder + "Telerik_develop_experiences.png"))
            {
                editor.InsertImageInline(stream, "png", new Size(660, 237));
            }
        }

        private void SaveDocument(RadFlowDocument document, string selectedFormat)
        {
            string selectedFormatLower = selectedFormat.ToLower();

            IFormatProvider<RadFlowDocument> formatProvider = null;
            switch (selectedFormatLower)
            {
                case "docx":
                    formatProvider = new DocxFormatProvider();
                    break;
                case "rtf":
                    formatProvider = new RtfFormatProvider();
                    break;
                case "txt":
                    formatProvider = new TxtFormatProvider();
                    break;
                case "html":
                    formatProvider = new HtmlFormatProvider();
                    break;
                case "pdf":
                    formatProvider = new PdfFormatProvider();
                    break;
            }

            if (formatProvider == null)
            {
                Console.WriteLine("Unknown or not supported format.");
                return;
            }

            string path = "Sample document." + selectedFormat;
            using (FileStream stream = File.OpenWrite(path))
            {
                formatProvider.Export(document, stream);
            }

            Console.Write("Document generated.");

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}