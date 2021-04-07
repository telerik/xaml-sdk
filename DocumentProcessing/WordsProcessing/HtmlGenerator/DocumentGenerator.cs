using System;
using System.Diagnostics;
using System.IO;
#if NETCOREAPP
using Telerik.Documents.Common.Model;
using Telerik.Documents.Core.Fonts;
using Telerik.Documents.Media;
using Telerik.Documents.Primitives;
#else
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Documents.Spreadsheet.Model;
#endif
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Styles;

namespace HtmlGenerator
{
    public class DocumentGenerator
    {
        private static readonly ThemableColor GreenColor = new ThemableColor(Color.FromArgb(255, 92, 230, 0));
        private static readonly string SampleDataFolder = "SampleData/";

        public void Generate()
        {
            RadFlowDocument document = this.CreateDocument();
            this.SaveDocument(document);
        }

        private RadFlowDocument CreateDocument()
        {
            RadFlowDocument document = new RadFlowDocument();
            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(document);
            editor.ParagraphFormatting.TextAlignment.LocalValue = Alignment.Justified;

            Table bodyTable = editor.InsertTable(1, 2);

            Paragraph paragraphWithText = bodyTable.Rows[0].Cells[0].Blocks.AddParagraph();
            editor.MoveToParagraphStart(paragraphWithText);

            editor.InsertLine("Dear Telerik User,");
            editor.InsertLine("We're happy to introduce the Telerik RadWordsProcessing component. High performance library that enables you to read, write and manipulate documents in DOCX, RTF, HTML and plain text format.");

            editor.InsertText("The current beta version comes with full rich-text capabilities including ");
            editor.InsertText("bold, ").FontWeight = FontWeights.Bold;
            editor.InsertText("italic, ").FontStyle = FontStyles.Italic;
            Run underlined = editor.InsertText("underline,");
            underlined.Underline.Pattern = UnderlinePattern.Dotted;
            underlined.Underline.Color = new ThemableColor(Colors.Black);
            editor.InsertText(" font sizes and ").FontSize = 20;
            editor.InsertText("colors ").ForegroundColor = GreenColor;
            editor.InsertLine("as well as text alignment and indentation. Other options include tables, lists, hyperlinks, bookmarks and comments, inline and floating images. Even more sweetness is added by the built-in styles and themes.");

            editor.InsertLine("We hope you'll enjoy RadWordsProcessing as much as we do. Happy coding!");

            editor.InsertParagraph().Spacing.SpacingAfter = 0;
            editor.InsertLine("Regards,");
            editor.InsertHyperlink("Telerik Team ", "http://www.telerik.com", false, "Telerik Site");

            Paragraph paragraphWithImage = bodyTable.Rows[0].Cells[1].Blocks.AddParagraph();
            editor.MoveToParagraphStart(paragraphWithImage);

            using (Stream stream = File.OpenRead(SampleDataFolder + "WordsProcessing.png"))
            {
                editor.InsertImageInline(stream, "png", new Size(470, 261));
            }

            return document;
        }

        private void SaveDocument(RadFlowDocument document)
        {
            HtmlFormatProvider formatProvider = new HtmlFormatProvider();

            string path = "Sample Document.html";
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
