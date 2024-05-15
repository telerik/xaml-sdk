using System.Linq;
using Telerik.Windows.Documents.Layout;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Model.Fields;
using Telerik.Windows.Documents.Model.Fields.CodeExpressions;

namespace CustomField
{
    public enum NumberFormattingTypes
    {
        Roman,
        Latin
    }

    public class CustomPageField : CodeBasedField
    {
        #region Constants

        private static readonly string FieldType = "CUSTOMPAGEFIELD";
        private static readonly string RomanFormattingSwitch = @"\r";
        private static readonly string LatinFormattingSwitch = @"\l";

        #endregion


        #region Fields

        private NumberFormattingTypes? pageNumberFormattingType;
        private readonly FieldProperty documentName;

        #endregion


        #region Constructors

        public CustomPageField()
        {
            CodeBasedFieldFactory.RegisterFieldType(FieldType, () => new CustomPageField());

            this.documentName = new FieldProperty(this, DocumentNameProperty);
        }

        #endregion


        #region Properties

        public override string FieldTypeName
        {
            get
            {
                return FieldType;
            }
        }

        [XamlSerializable]
        public NumberFormattingTypes? NumberFormattingType
        {
            get
            {
                return this.pageNumberFormattingType;
            }
            set
            {
                if (this.pageNumberFormattingType != value)
                {
                    this.pageNumberFormattingType = value;
                    this.InvalidateCode();
                }
            }
        }

        public static readonly FieldPropertyDefinition DocumentNameProperty = new FieldPropertyDefinition("MyProperty");

        [XamlSerializable]
        public string DocumentName
        {
            get
            {
                return this.documentName.GetValue();
            }
            set
            {
                if (!this.documentName.IsNestedField && this.documentName.GetValue() == value)
                {
                    return;
                }

                this.documentName.SetValue(value);
                this.InvalidateCode();
            }
        }

        #endregion


        #region Methods

        public override Field CreateInstance()
        {
            return new CustomPageField();
        }

        public override void CopyPropertiesFrom(Field fromField)
        {
            base.CopyPropertiesFrom(fromField);

            CustomPageField fromPageField = (CustomPageField)fromField;
            this.NumberFormattingType = fromPageField.NumberFormattingType;
            this.documentName.CopyPropertiesFrom(fromPageField.documentName);
        }

        protected override void CopyPropertiesFromCodeExpression(FieldCodeExpression fieldCodeExpression)
        {
            base.CopyPropertiesFromCodeExpression(fieldCodeExpression);
            this.documentName.SetValue(fieldCodeExpression.FieldArgumentNode);

            if (fieldCodeExpression.ContainsSwitch(RomanFormattingSwitch))
            {
                this.NumberFormattingType = NumberFormattingTypes.Roman;
            }
            else if (fieldCodeExpression.ContainsSwitch(LatinFormattingSwitch))
            {
                this.NumberFormattingType = NumberFormattingTypes.Latin;
            }
        }

        protected override void BuildCodeOverride()
        {
            base.BuildCodeOverride();
            this.CodeBuilder.SetFieldArgument(this.documentName);

            if (this.pageNumberFormattingType.HasValue)
            {
                switch (this.pageNumberFormattingType.Value)
                {
                    case NumberFormattingTypes.Roman:
                        this.CodeBuilder.AddSwitch(RomanFormattingSwitch);
                        break;
                    case NumberFormattingTypes.Latin:
                        this.CodeBuilder.AddSwitch(LatinFormattingSwitch);
                        break;
                    default:
                        break;
                }
            }
        }

        protected override DocumentFragment GetResultFragment()
        {
            int totalPagesCount = 1;
            int totalPagesInCurrentSection = 1;
            int currentPageInCurrentSection = 1;

            if (this.EvaluationContext != null)
            {
                totalPagesCount = this.GetTotalPageCount();

                LayoutBox box = this.EvaluationContext.AssociatedLayoutBoxInMainDocument ?? this.FieldStart.FirstLayoutBox;
                currentPageInCurrentSection = this.GetCurrentPageFromSectionPages(box);
                totalPagesInCurrentSection = this.GetPageCountInSection(box);
            }

            string totalPagesCountWithFormatting = this.GetNumberWithFormatting(totalPagesCount);
            string totalPagesInCurrentSectionWithFormatting = this.GetNumberWithFormatting(currentPageInCurrentSection);
            string currentPageInCurrentSectionWithFormatting = this.GetNumberWithFormatting(totalPagesInCurrentSection);

            string textToInsert = !string.IsNullOrEmpty(this.DocumentName)
                ? string.Format("Total pages in the document ({0}): {1}. Pages in current section: {2} / {3}",
                                    this.DocumentName,
                                    totalPagesCountWithFormatting,
                                    totalPagesInCurrentSectionWithFormatting,
                                    currentPageInCurrentSectionWithFormatting)
                : string.Format("Total pages in the document: {0}. Pages in current section: {1} / {2}",
                                    totalPagesCountWithFormatting,
                                    totalPagesInCurrentSectionWithFormatting,
                                    currentPageInCurrentSectionWithFormatting);

            Span spanToInsert = new Span(textToInsert);

            Span currentSpan = this.EvaluationContext.Document.CaretPosition.GetCurrentSpan();
            if (currentSpan != null)
            {
                spanToInsert.CopyPropertiesFrom(currentSpan);
            }

            return DocumentFragment.CreateFromInline(spanToInsert);
        }

        private string GetNumberWithFormatting(int pageNumber)
        {
            return this.NumberFormattingType == NumberFormattingTypes.Roman ? MathUtils.ToRoman(pageNumber) : pageNumber.ToString();
        }

        private int GetCurrentPageFromSectionPages(LayoutBox box)
        {
            int pageCounter = 1;

            SectionLayoutBox fieldLayotBox = (box as SectionLayoutBox) ?? box.GetCurrentSectionBox();

            foreach (SectionLayoutBox layoutBox in fieldLayotBox.AssociatedSection.GetAssociatedLayoutBoxes())
            {
                if (layoutBox.PageNumber == fieldLayotBox.PageNumber)
                {
                    break;
                }

                pageCounter++;
            }

            return pageCounter;
        }

        private int GetPageCountInSection(LayoutBox box)
        {
            SectionLayoutBox fieldSectionBox = (box as SectionLayoutBox) ?? box.GetCurrentSectionBox();
            int pagesNumber = fieldSectionBox.AssociatedSection.GetAssociatedLayoutBoxes().Count();

            return pagesNumber;
        }

        private int GetTotalPageCount()
        {
            RadDocument document = this.EvaluationContext.MainDocument ?? this.Document;
            int pageCounter = 0;

            foreach (Section section in document.Sections)
            {
                pageCounter += section.GetAssociatedLayoutBoxes().Count();
            }

            return pageCounter;
        }

        #endregion
    }
}
