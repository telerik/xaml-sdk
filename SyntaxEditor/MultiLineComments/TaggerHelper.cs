using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Windows.SyntaxEditor.Core.Tagging;
using Telerik.Windows.SyntaxEditor.Core.Text;

namespace MultiLineComments
{
    internal static class TaggerHelper
    {
        internal const string CommentTypeString = "comment";
        internal const string AttributeTypeString = "attribute";
        internal const string ElementTypeString = "element";
        internal const string TagTypeString = "tag";
        internal const string ContentTypeString = "content";
        internal const string StringTypeString = "string";
        internal const string CharacterDataString = "cdata";

        internal const string CommentsRegex = @"(?<comment>\/(\s)?\*[\s\S]*?(\*\/|\z))";
        internal static IList<TagSpan<ClassificationTag>> GetTags(TextDocument document, string text, string stringRegexPattern, params string[] regexPatters)
        {
            // String pattern is to prevent multiline pattern to match falsely multiline start/end tags in strings.
            StringBuilder pattern = new StringBuilder(stringRegexPattern + "|");
            for (int i = 0; i < regexPatters.Length; i++)
            {
                pattern.Append("{" + i + "}");
                if (i != regexPatters.Length - 1)
                {
                    pattern.Append("|");
                }
            }
            string resultRegex = string.Format(pattern.ToString(), regexPatters);
            Regex regularExpression = new Regex(resultRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Multiline | RegexOptions.Compiled);
            MatchCollection matches = regularExpression.Matches(text);
            string[] regexGroups = new string[] { CommentTypeString };

            List<TagSpan<ClassificationTag>> tags = new List<TagSpan<ClassificationTag>>();
            foreach (Match match in matches)
            {
                foreach (var description in regexGroups)
                {
                    if (match.Groups[description].Success)
                    {
                        var start = match.Index;
                        var length = match.Length;
                        var textSnapshot = document.CurrentSnapshot;
                        var tagSpan = CreateTagSpan(textSnapshot, start, length, description);

                        tags.Add(tagSpan);
                    }
                }
            }

            return tags;
        }

        private static TagSpan<ClassificationTag> CreateTagSpan(TextSnapshot snapshot, int start, int len, string description)
        {
            var textSnapshotSpan = new TextSnapshotSpan(snapshot, new Span(start, len));
            var classificationTag = new ClassificationTag(GetClassificationType(description));
            var tagSpan = new TagSpan<ClassificationTag>(textSnapshotSpan, classificationTag);

            return tagSpan;
        }

        private static ClassificationType GetClassificationType(string description)
        {
            switch (description)
            {
                case CommentTypeString: return ClassificationTypes.Comment;
            }

            return null;
        }
    }
}
