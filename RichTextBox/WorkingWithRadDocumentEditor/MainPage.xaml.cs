using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.Layout;
using Telerik.Windows.Documents.Lists;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Model.Styles;

namespace WorkingWithRadDocumentEditor_SL
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            RadDocument document = CreateDocument();
            this.radRichTextBox.Document = document;
        }

        private RadDocument CreateDocument()
        {
            RadDocument document = new RadDocument();
            document.History.IsEnabled = false;

            RadDocumentEditor editor = new RadDocumentEditor(document);

            editor.Document.LayoutMode = DocumentLayoutMode.Paged;

            #region CreateCover

            StyleDefinition titleStyle = new StyleDefinition("Title", StyleType.Paragraph);
            titleStyle.ParagraphProperties.TextAlignment = RadTextAlignment.Center;
            titleStyle.SpanProperties.FontSize = 35;
            titleStyle.SpanProperties.ForeColor = Color.FromArgb(255, 101, 156, 239);
            titleStyle.NextStyleName = RadDocumentDefaultStyles.NormalStyleName;

            editor.Document.StyleRepository.Add(titleStyle);
            editor.ChangeStyleName("Title");

            editor.Insert("Sample Word Document Test");
            editor.InsertSectionBreak(SectionBreakType.NextPage);

            #endregion

            #region CreateTOCSection

            editor.ChangeStyleName("Title");

            editor.Insert("Table of contents");
            editor.InsertField(new TableOfContentsField() { });
            editor.InsertSectionBreak(SectionBreakType.NextPage);

            #endregion

            #region Content

            editor.ChangeStyleName(RadDocumentDefaultStyles.GetHeadingStyleNameByIndex(1));
            editor.ChangeParagraphListStyle(DefaultListStyles.NumberedHierarchical);
            editor.Insert("Section A Heading");
            editor.InsertParagraph();
            editor.ChangeParagraphListStyle(null);

            editor.Insert("Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in elementum ipsum. Duis vel vulputate massa, eget iaculis urna. Morbi feugiat, magna eget accumsan mollis, leo lectus porta diam, id sollicitudin mi tellus nec tortor. Nullam lacinia consequat blandit. Sed tincidunt pulvinar ultricies. Interdum et malesuada fames ac ante ipsum primis in faucibus. Praesent nec convallis nunc. Maecenas fermentum, dolor sed egestas aliquet, diam sem tempus nulla, sed vehicula ipsum metus ut odio. Proin commodo malesuada justo in mollis. Nullam et blandit est, ac dapibus tortor. Aliquam ligula mauris, sodales vitae gravida a, bibendum eget arcu.");
            editor.InsertParagraph();

            editor.ChangeStyleName(RadDocumentDefaultStyles.GetHeadingStyleNameByIndex(2));
            editor.ChangeParagraphListStyle(DefaultListStyles.Numbered);
            editor.ContinueListNumbering();
            editor.ChangeParagraphListLevel(1);
            editor.Insert("Subsection A1");

            editor.InsertParagraph();
            editor.ChangeParagraphListStyle(DefaultListStyles.None);

            editor.Insert("Proin sodales aliquam lorem ac laoreet. Integer diam lorem, cursus at arcu sed, ornare luctus diam. Maecenas a blandit sem. Donec quam nunc, euismod quis quam vel, pulvinar rhoncus urna.");
            editor.InsertParagraph();

            editor.ChangeStyleName(RadDocumentDefaultStyles.GetHeadingStyleNameByIndex(2));
            editor.ChangeParagraphListStyle(DefaultListStyles.Numbered);
            editor.ContinueListNumbering();
            editor.ChangeParagraphListLevel(1);
            editor.Insert("Subsection A2");
            editor.InsertParagraph();
            editor.ChangeParagraphListStyle(null);

            editor.Insert("Duis ornare magna mi, id commodo sem pulvinar et. Quisque adipiscing diam purus, nec posuere eros fringilla non. Nam a dictum lacus. In sit amet dignissim est. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Suspendisse potenti");
            #endregion

            editor.UpdateAllFields(FieldDisplayMode.Result);

            editor.Document.Sections.First.Headers.Default = this.CreateHeader();
            editor.Document.Sections.First.Footers.Default = this.CreateFooter();

            return document;
        }

        private Header CreateHeader()
        {
            RadDocument headerDocument = new RadDocument();
            RadDocumentEditor editor = new RadDocumentEditor(headerDocument);

            using (Stream stream = Application.GetResourceStream(GetResourceUri("Images/Telerik_Logo.jpg")).Stream)
            {
                ImageInline image = new ImageInline(stream);
                editor.InsertInline(image);
            }

            editor.ChangeParagraphTextAlignment(RadTextAlignment.Center);

            Header header = new Header() { Body = headerDocument };
            return header;
        }

        private static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(MainPage).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }

        private Footer CreateFooter()
        {
            RadDocument footerDocument = new RadDocument();
            RadDocumentEditor editor = new RadDocumentEditor(footerDocument);

            editor.InsertTable(1, 2);
            editor.ChangeStyleName(RadDocumentDefaultStyles.DefaultNormalTableStyleName);

            editor.Document.Selection.SelectAll();

            editor.ChangeFontFamily(new FontFamily("Arial"));
            editor.ChangeForeColor(Color.FromArgb(255, 29, 192, 34));
            editor.ChangeFontSize(Unit.PointToDip(10));

            editor.Document.Selection.Clear();

            editor.Insert("Copyright © 2002-2015 Telerik. All rights reserved.");

            var table = editor.Document.EnumerateChildrenOfType<Table>().FirstOrDefault();

            editor.Document.CaretPosition.MoveToStartOfDocumentElement(table.Rows.First.Cells.Last);
            table.Grid.Columns.Last().PreferredWidth = new TableWidthUnit(20);

            PageField field = new PageField();
            editor.InsertField(field, FieldDisplayMode.Result);
            editor.ChangeParagraphTextAlignment(RadTextAlignment.Right);

            Footer footer = new Footer() { Body = footerDocument };
            return footer;
        }
    }
}
