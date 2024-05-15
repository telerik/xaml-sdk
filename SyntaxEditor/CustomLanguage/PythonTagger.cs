using System.Collections.Generic;
using Telerik.Windows.Controls.SyntaxEditor.Tagging.Taggers;
using Telerik.Windows.SyntaxEditor.Core.Tagging;

namespace CustomLanguage
{
    public class PythonTagger : WordTaggerBase
    {
        private const char CommentSymbol = '#';
        private const char VariableNameSeparatorSymbol = '_';

        private static readonly string[] Keywords = new string[]
        {
        "False", "None", "True", "and", "as", "assert","break", "class",
        "continue", "def", "del", "elif", "else", "except", "for", "from",
        "global", "if", "import", "in", "is", "lambda", "nonlocal", "not",
        "or", "pass", "raise", "finally", "return", "try", "while", "with", "yield"
        };

        private static readonly string[] Comments = new string[]
        {
        "#"
        };

        private static readonly string[] Operators = new string[]
        {
        "+", "-",  "*", "/"
        };

        private static readonly string[] Fruits = new string[]
        {
        "apple", "banana", "blue_berry", "cherry"
        };

        private static readonly Dictionary<string, ClassificationType> WordsToClassificationType = new Dictionary<string, ClassificationType>();

        public static readonly ClassificationType FruitsClassificationType = new ClassificationType("Fruits");

        static PythonTagger()
        {
            WordsToClassificationType = new Dictionary<string, ClassificationType>();

            foreach (var keyword in Keywords)
            {
                WordsToClassificationType.Add(keyword, ClassificationTypes.Keyword);
            }

            foreach (var preprocessor in Operators)
            {
                WordsToClassificationType.Add(preprocessor, ClassificationTypes.Operator);
            }

            foreach (var comment in Comments)
            {
                WordsToClassificationType.Add(comment, ClassificationTypes.Comment);
            }

            foreach (var comment in Fruits)
            {
                WordsToClassificationType.Add(comment, FruitsClassificationType);
            }
        }

        public PythonTagger(Telerik.Windows.Controls.RadSyntaxEditor editor)
          : base(editor)
        {
        }

        /// <summary>
        /// Gets the words to classification types.
        /// </summary>
        /// <returns>Dictionary&lt;System.String, ClassificationType&gt;.</returns>
        protected override Dictionary<string, ClassificationType> GetWordsToClassificationTypes()
        {
            return PythonTagger.WordsToClassificationType;
        }

        /// <summary>
        /// Tries to get the classification type for the given string word.
        /// </summary>
        /// <param name="word">The string word.</param>
        /// <param name="classificationType">The result classification type.</param>
        /// <returns>Returns true if classification type is found, otherwise - false.</returns>
        protected override bool TryGetClassificationType(string word, out ClassificationType classificationType)
        {
            int number;

            if (int.TryParse(word, out number))
            {
                classificationType = ClassificationTypes.NumberLiteral;
                return true;
            }

            return base.TryGetClassificationType(word, out classificationType);
        }

        /// <summary>
        /// Called when a word is split during processing of a line string.
        /// </summary>
        protected override void OnWordSplit(int wordCharType, string word)
        {
            if (wordCharType == 3 && word.Length > 1)
            {
                this.AddWord(word, ClassificationTypes.Comment);
            }
        }

        /// <summary>
        /// Defines the different char types which is the essence of splitting words.
        /// A word is considered sequence of equally typed chars.
        /// 1 - whitespace, 2 - punctuation or symbol, 0 - letters and all other characters.
        /// </summary>
        protected override int GetCharType(char c)
        {
            if (c == CommentSymbol)
            {
                return 3;
            }

            // Avoid splitting variable names by the underscore character.
            if (c == VariableNameSeparatorSymbol)
            {
                return 0;
            }

            if (char.IsWhiteSpace(c))
            {
                return 1;
            }

            if (char.IsPunctuation(c) || char.IsSymbol(c))
            {
                return 2;
            }

            return 0;
        }
    }
}
