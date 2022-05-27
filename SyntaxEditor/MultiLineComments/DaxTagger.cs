using System.Collections.Generic;
using System.Windows.Media;
using Telerik.Windows.Controls.SyntaxEditor.Tagging.Taggers;
using Telerik.Windows.Controls.SyntaxEditor.UI;
using Telerik.Windows.SyntaxEditor.Core.Tagging;

namespace MultiLineComments
{
    public class DAXTagger : WordTaggerBase
    {
        private const string StringPattern = "@\"(?:[^\"]|\"\")*\"|\"(?:[^\"\\\\]|\\\\.)*\"";

        private static readonly string[] Keywords = new string[]
        {
            "APPROXIMATEDISTINCTCOUNT","AVERAGE","AVERAGEA","AVERAGEX","COUNT","COUNTA","COUNTAX","COUNTBLANK","COUNTROWS",
            "COUNTX","DISTINCTCOUNT","DISTINCTCOUNTNOBLANK","MAX","MAXA","MAXX","MIN","MINA","MINX","PRODUCT","PRODUCTX","SUM","SUMX",
            "CALENDAR","CALENDARAUTO","DATE","DATEDIFF","DATEVALUE","DAY","EDATE","EOMONTH","HOUR","MINUTE","MONTH","NOW","QUARTER",
            "SECOND","TIME","TIMEVALUE","TODAY","UTCNOW","UTCTODAY","WEEKDAY","WEEKNUM","YEAR","YEARFRAC",
            "ALL","ALLCROSSFILTERED","ALLEXCEPT","ALLNOBLANKROW","ALLSELECTED","CALCULATE","CALCULATETABLE","EARLIER","EARLIEST",
            "FILTER","KEEPFILTERS","LOOKUPVALUE","REMOVEFILTERS","SELECTEDVALUE","ACCRINT","ACCRINTM","AMORDEGRC","AMORLINC","COUPDAYBS",
            "COUPDAYS","COUPDAYSNC","COUPNCD","COUPNUM","COUPPCD","CUMIPMT","CUMPRINC","DB","DDB","DISC","DOLLARDE","DOLLARFR",
            "DURATION","EFFECT","FV","INTRATE","IPMT","ISPMT","MDURATION","NOMINAL","NPER","ODDFPRICE","ODDFYIELD","ODDLPRICE",
            "ODDLYIELD","PDURATION","PMT","PPMT","PRICE","PRICEDISC","PRICEMAT","PV","RATE","RECEIVED","RRI","SLN","SYD","TBILLEQ",
            "TBILLPRICE","TBILLYIELD","VDB","XIRR","XNPV","YIELD","YIELDDISC","YIELDMAT","CONTAINS","CONTAINSROW","CONTAINSSTRING",
            "CONTAINSSTRINGEXACT","CUSTOMDATA","HASONEFILTER","HASONEVALUE","ISAFTER","ISBLANK","ISCROSSFILTERED","ISEMPTY","ISERROR",
            "ISEVEN","ISFILTERED","ISINSCOPE","ISLOGICAL","ISNONTEXT","ISNUMBER","ISODD","ISONORAFTER","ISSELECTEDMEASURE","ISSUBTOTAL",
            "ISTEXT","NONVISUAL","SELECTEDMEASURE","SELECTEDMEASUREFORMATSTRING","SELECTEDMEASURENAME","USERNAME","USEROBJECTID",
            "USERPRINCIPALNAME","AND","BITAND","BITLSHIFT","BITOR","BITRSHIFT","BITXOR","COALESCE","FALSE","IF","IF.EAGER","IFERROR",
            "NOT","OR","SWITCH","TRUE","ABS","ACOS","ACOSH","ACOT","ACOTH","ASIN","ASINH","ATAN","ATANH","CEILING","CONVERT","COS",
            "COSH","COT","COTH","DEGREES","DIVIDE","EVEN","EXP","FACT","FLOOR","GCD","INT","ISO.CEILING","LCM","LN","LOG",
            "LOG10","MROUND","ODD","PI","POWER","QUOTIENT","RADIANS","RAND","RANDBETWEEN","ROUND","ROUNDDOWN","ROUNDUP","SIGN","SIN",
            "SINH","SQRT","SQRTPI","TAN","TANH","TRUNC","BLANK","ERROR","PATH","PATHCONTAINS","PATHITEM","PATHITEMREVERSE","PATHLENGTH",
            "CROSSFILTER","RELATED","RELATEDTABLE","USERELATIONSHIP","BETA.DIST","BETA.INV","CHISQ.DIST","CHISQ.DIST.RT","CHISQ.INV",
            "CHISQ.INV.RT","COMBIN","COMBINA","CONFIDENCE.NORM","CONFIDENCE.T","EXPON.DIST","GEOMEAN","GEOMEANX","MEDIAN","MEDIANX",
            "NORM.DIST","NORM.INV","NORM.S.DIST","NORM.S.INV","PERCENTILE.EXC","PERCENTILE.INC","PERCENTILEX.EXC","PERCENTILEX.INC",
            "PERMUT","POISSON.DIST","RANK.EQ","RANKX","SAMPLE","STDEV.P","STDEV.S","STDEVX.P","STDEVX.S","T.DIST","T.DIST.2T",
            "T.DIST.RT","T.INV","T.INV.2t","VAR.P","VAR.S","VARX.P","VARX.S","ADDCOLUMNS","ADDMISSINGITEMS","CROSSJOIN","CURRENTGROUP",
            "DETAILROWS","DISTINCT column","DISTINCT table","EXCEPT","FILTERS","GENERATE","GENERATEALL","GENERATESERIES",
            "GROUPBY","IGNORE","INTERSECT","NATURALINNERJOIN","NATURALLEFTOUTERJOIN","ROLLUP","ROLLUPADDISSUBTOTAL","ROLLUPISSUBTOTAL",
            "ROLLUPGROUP","ROW","SELECTCOLUMNS","SUBSTITUTEWITHINDEX","SUMMARIZE","SUMMARIZECOLUMNS","Table Constructor","TOPN",
            "TREATAS","UNION","VALUES","COMBINEVALUES","CONCATENATE","CONCATENATEX","EXACT","FIND","FIXED","FORMAT","LEFT","LEN",
            "LOWER","MID","REPLACE","REPT","RIGHT","SEARCH","SUBSTITUTE","TRIM","UNICHAR","UNICODE","UPPER","VALUE",
            "CLOSINGBALANCEMONTH","CLOSINGBALANCEQUARTER","CLOSINGBALANCEYEAR","DATEADD","DATESBETWEEN","DATESINPERIOD","DATESMTD",
            "DATESQTD","DATESYTD","ENDOFMONTH","ENDOFQUARTER","ENDOFYEAR","FIRSTDATE","FIRSTNONBLANK","LASTDATE","LASTNONBLANK",
            "NEXTDAY","NEXTMONTH","NEXTQUARTER","NEXTYEAR","OPENINGBALANCEMONTH","OPENINGBALANCEQUARTER","OPENINGBALANCEYEAR",
            "PARALLELPERIOD","PREVIOUSDAY","PREVIOUSMONTH","PREVIOUSQUARTER","PREVIOUSYEAR","SAMEPERIODLASTYEAR","STARTOFMONTH",
            "STARTOFQUARTER","STARTOFYEAR","TOTALMTD","TOTALQTD","TOTALYTD",
            "BOOLEAN","CURRENCY","DATETIME","DOUBLE","INTEGER","STRING"
        };

        private static readonly string[] Comments = new string[]
        {
            "//", "--", "/*", "*/"
        };

        private static readonly string[] Operators = new string[]
        {
            "+", "-",  "*", "/", "^", "&", "|", "&&", "||"
        };

        public static readonly ClassificationType Keywords1ClassificationType = new ClassificationType("Keywords1");

        private static readonly string[] Keywords1 = new string[]
        {
            "VAR", "RETURN", "DATATABLE", "DEFINE", "EVALUATE", "ASC", "DESC"
        };

        private static readonly Dictionary<string, ClassificationType> WordsToClassificationType = new Dictionary<string, ClassificationType>();

        static DAXTagger()
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

            foreach (var comment in Keywords1)
            {
                WordsToClassificationType.Add(comment, Keywords1ClassificationType);
            }
        }

        public DAXTagger(Telerik.Windows.Controls.RadSyntaxEditor editor)
          : base(editor)
        {
            editor.TextFormatDefinitions.AddLast(ClassificationTypes.NumberLiteral, new TextFormatDefinition(new SolidColorBrush(Colors.Orange)));
            editor.TextFormatDefinitions.AddLast(ClassificationTypes.Comment, new TextFormatDefinition(new SolidColorBrush(Colors.Green)));
            editor.TextFormatDefinitions.AddLast(DAXTagger.Keywords1ClassificationType, new TextFormatDefinition(new SolidColorBrush(Colors.DarkBlue)));
        }

        protected override Dictionary<string, ClassificationType> GetWordsToClassificationTypes()
        {
            return DAXTagger.WordsToClassificationType;
        }

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

        protected override IList<string> SplitIntoWords(string value)
        {
            List<string> words = new List<string>();
            string word;
            int lastCharType = -1;
            int startIndex = 0;

            for (int i = 0; i < value.Length; i++)
            {
                int charType = GetCharType(value[i]);
                if (charType != lastCharType)
                {
                    word = value.Substring(startIndex, i - startIndex);
                    words.Add(word);
                    startIndex = i;
                    lastCharType = charType;
                }
                else if (value[i] == '#' && value[i - 1] == '#')
                {
                    word = value.Substring(startIndex, i - startIndex);
                    words.Add(word);
                    startIndex = i;
                    lastCharType = charType;
                }
            }

            word = value.Substring(startIndex, value.Length - startIndex);
            words.Add(word);

            return words;
        }

        protected override int GetCharType(char character)
        {
            if (character == '#' || character == '_')
            {
                return 4;
            }

            if (character == '/' || character == '-')
            {
                return 3;
            }

            if (char.IsWhiteSpace(character))
            {
                return 1;
            }

            if (char.IsPunctuation(character) || char.IsSymbol(character))
            {
                return 2;
            }

            return 0;
        }

        /// <summary>
        /// Called when a word is split during processing of a line string.
        /// </summary>
        protected override void OnWordSplit(int wordCharType, string word)
        {
            if (wordCharType == 3 && word.Length >= 2)
            {
                this.AddWord(word, ClassificationTypes.Comment);
            }
        }

        /// <summary>
        /// Rebuilds the MultilineTags collection.
        /// </summary>
        protected override void RebuildMultilineTags()
        {
            string wholeText = this.Document.CurrentSnapshot.GetText();
            this.MultilineTags.Clear();
            IList<TagSpan<ClassificationTag>> tags = TaggerHelper.GetTags(this.Document, wholeText, StringPattern, TaggerHelper.CommentsRegex);
            foreach (TagSpan<ClassificationTag> tag in tags)
            {
                this.MultilineTags.Add(tag);
            }
        }
    }
}