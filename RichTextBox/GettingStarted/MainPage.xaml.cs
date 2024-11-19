using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents.Model;

namespace GettingStarted
{
    public partial class MainPage : UserControl
    {
        string imagePath = @"/GettingStarted;component/Images/RadRichTextBox.png";

        public MainPage()
        {
            InitializeComponent();
            CreateDocument();

            this.radRichTextBox.Commands.ToggleBoldCommand.ToggleStateChanged += new EventHandler<Telerik.Windows.Documents.RichTextBoxCommands.StylePropertyChangedEventArgs<bool>>(ToggleBoldCommand_ToggleStateChanged);
            this.radRichTextBox.Commands.ToggleItalicCommand.ToggleStateChanged += new EventHandler<Telerik.Windows.Documents.RichTextBoxCommands.StylePropertyChangedEventArgs<bool>>(ToggleItalicCommand_ToggleStateChanged);
            this.radRichTextBox.Commands.ToggleUnderlineCommand.ToggleStateChanged += new EventHandler<Telerik.Windows.Documents.RichTextBoxCommands.StylePropertyChangedEventArgs<bool>>(ToggleUnderlineCommand_ToggleStateChanged);
        }

        private void CreateDocument()
        {
            RadDocument document = new RadDocument();
            Section section = new Section();

            Paragraph paragraph1 = new Paragraph();
            Stream stream = Application.GetResourceStream(new Uri(imagePath, UriKind.RelativeOrAbsolute)).Stream;
            Size size = new Size(236, 50);
            ImageInline imageInline = new ImageInline(stream, size, "png");
            paragraph1.Inlines.Add(imageInline);
            section.Blocks.Add(paragraph1);

            Paragraph paragraph2 = new Paragraph();
            paragraph2.TextAlignment = Telerik.Windows.Documents.Layout.RadTextAlignment.Center;
            Span span1 = new Span("Thank you for choosing Telerik");
            paragraph2.Inlines.Add(span1);

            Span span2 = new Span();
            span2.Text = " RadRichTextBox!";
            span2.FontWeight = FontWeights.Bold;
            paragraph2.Inlines.Add(span2);
            section.Blocks.Add(paragraph2);

            Paragraph paragraph3 = new Paragraph();
            Span span3 = new Span("RadRichTextBox");
            span3.FontWeight = FontWeights.Bold;
            paragraph3.Inlines.Add(span3);

            Span span4 = new Span(" is a control that is able to display and edit rich-text content including formatted text arranged in pages, paragraphs, spans (runs) etc.");
            paragraph3.Inlines.Add(span4);
            section.Blocks.Add(paragraph3);

            Table table = new Table();
            table.LayoutMode = TableLayoutMode.AutoFit;
            table.StyleName = RadDocumentDefaultStyles.DefaultTableGridStyleName;

            TableRow row1 = new TableRow();

            TableCell cell1 = new TableCell();
            Paragraph p1 = new Paragraph();
            Span s1 = new Span();
            s1.Text = "Cell 1";
            p1.Inlines.Add(s1);
            cell1.Blocks.Add(p1);
            row1.Cells.Add(cell1);

            TableCell cell2 = new TableCell();
            Paragraph p2 = new Paragraph();
            Span s2 = new Span();
            s2.Text = "Cell 2";
            p2.Inlines.Add(s2);
            cell2.Blocks.Add(p2);
            row1.Cells.Add(cell2);
            table.Rows.Add(row1);

            TableRow row2 = new TableRow();

            TableCell cell3 = new TableCell();
            cell3.ColumnSpan = 2;
            Paragraph p3 = new Paragraph();
            Span s3 = new Span();
            s3.Text = "Cell 3";
            p3.Inlines.Add(s3);
            cell3.Blocks.Add(p3);
            row2.Cells.Add(cell3);
            table.Rows.Add(row2);

            section.Blocks.Add(table);
            section.Blocks.Add(new Paragraph());
            document.Sections.Add(section);

            this.radRichTextBox.Document = document;

        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            this.radRichTextBox.ToggleBold();
            this.radRichTextBox.Focus();
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            this.radRichTextBox.ToggleItalic();
            this.radRichTextBox.Focus();
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            this.radRichTextBox.ToggleUnderline();
            this.radRichTextBox.Focus();
        }

        void ToggleBoldCommand_ToggleStateChanged(object sender, Telerik.Windows.Documents.RichTextBoxCommands.StylePropertyChangedEventArgs<bool> e)
        {
            BoldButton.IsChecked = e.NewValue;
        }

        void ToggleUnderlineCommand_ToggleStateChanged(object sender, Telerik.Windows.Documents.RichTextBoxCommands.StylePropertyChangedEventArgs<bool> e)
        {
            UnderlineButton.IsChecked = e.NewValue;
        }

        void ToggleItalicCommand_ToggleStateChanged(object sender, Telerik.Windows.Documents.RichTextBoxCommands.StylePropertyChangedEventArgs<bool> e)
        {
            ItalicButton.IsChecked = e.NewValue;
        }
    }
}
