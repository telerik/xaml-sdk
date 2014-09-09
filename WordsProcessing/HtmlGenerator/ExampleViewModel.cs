using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Styles;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace HtmlGenerator
{
    public class ExampleViewModel : ViewModelBase
    {
        private static readonly ThemableColor GreenColor = new ThemableColor(Color.FromArgb(255, 92, 230, 0));

        private ICommand saveCommand = null;
        public ICommand SaveCommand
        {
            get
            {
                return this.saveCommand;
            }
            set
            {
                if (this.saveCommand != value)
                {
                    this.saveCommand = value;
                    this.OnPropertyChanged("SaveCommand");
                }
            }
        }

        private ICommand showCommand = null;
        public ICommand ShowCommand
        {
            get
            {
                return this.showCommand;
            }
            set
            {
                if (this.showCommand != value)
                {
                    this.showCommand = value;
                    this.OnPropertyChanged("ShowCommand");
                }
            }
        }

        public ExampleViewModel()
        {
            this.SaveCommand = new DelegateCommand(this.Save);
            this.ShowCommand = new DelegateCommand(this.Show);
        }

        private void Show(object obj)
        {
            RadFlowDocument document = this.CreateDocument();
            HtmlFormatProvider formatProvider = new HtmlFormatProvider();
            string html = formatProvider.Export(document);

            WebBrowser browser = new WebBrowser();
            browser.NavigateToString(html);

            Window window = new Window() { Width = 1000, Height = 350 };
            window.Content = browser;
            window.Show();
        }

        private void Save(object obj)
        {
            RadFlowDocument document = this.CreateDocument();
            FileHelper.SaveDocument(document, "HTML");
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
            editor.InsertLine("We're happy to introduce the new Telerik RadWordsProcessing component for WPF. High performance library that enables you to read, write and manipulate documents in DOCX, RTF, HTML and plain text format.");

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

            using (Stream stream = FileHelper.GetSampleResourceStream("WordsProcessing.png"))
            {
                editor.InsertImageInline(stream, "png", new Size(470, 261));
            }

            return document;
        }
    }
}
