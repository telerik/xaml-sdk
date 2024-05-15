using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Model.Code;
using Telerik.Windows.Documents.Model.Styles;

namespace CustomCodeFormattingLanguage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // we should register the new tagger to the initial document of RadRichTextBox and to every new document.
            this.AddTagger(this.radRichTextBox.Document);
        }

        private void editor_DocumentChanged(object sender, EventArgs e)
        {
            this.AddTagger(this.radRichTextBox.Document);
        }

        private void AddTagger(RadDocument document)
        {
            CodeLanguage vbCodeLanguage = new CodeLanguage("VB");

            RegexTagger vbRegexTagger = MainWindow.GetVbTagger();

            document.CodeFormatter.RegisterCodeLanguage(vbCodeLanguage, vbRegexTagger);

            StyleDefinition vbKeywordStyle = new StyleDefinition("vbKeywordStyle", StyleType.Character);
            vbKeywordStyle.SpanProperties.ForeColor = Colors.Orange;

            document.CodeFormatter.RegisterClassificationType(ClassificationTypes.Keyword, vbCodeLanguage, vbKeywordStyle);
        }

        private static RegexTagger GetVbTagger()
        {
            Regex keywordRegex = RegexTagger.GetKeywordsRegex(Keywords.Vb);
            Regex preprocessorRegex = new Regex(@"^\s*#.*$", RegexOptions.Multiline);
            Regex stringRegex = new Regex(DefaultRegexPatterns.StringPattern, RegexOptions.Multiline);
            Regex singleCommentRegex = new Regex(DefaultRegexPatterns.SingleLineCommentPattern, RegexOptions.Multiline);
            Regex multiLineCommentRegex = new Regex(DefaultRegexPatterns.MultilineCommentPattern, RegexOptions.Multiline);

            Dictionary<Regex, ClassificationType> collection = new Dictionary<Regex, ClassificationType>();
            collection.Add(keywordRegex, ClassificationTypes.Keyword);
            collection.Add(preprocessorRegex, ClassificationTypes.PreprocessorKeyword);
            collection.Add(stringRegex, ClassificationTypes.StringLiteral);
            collection.Add(singleCommentRegex, ClassificationTypes.Comment);
            collection.Add(multiLineCommentRegex, ClassificationTypes.Comment);

            RegexTagger regexVbTagger = new RegexTagger(collection);

            return regexVbTagger;
        }
    }
}
