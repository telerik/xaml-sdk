using System;
using System.Linq;
using Telerik.Windows.Documents.Layout;
using Telerik.Windows.Documents.Model;

namespace CustomField
{
    public class CustomField : CodeBasedField
    {
        private static readonly string FieldType = "CUSTOMFIELD";
   
        public override string FieldTypeName
        {
            get
            {
                return CustomField.FieldType;
            }
        }
       
        static CustomField()
        {
            CodeBasedFieldFactory.RegisterFieldType(CustomField.FieldType, () => new CustomField());
        }

        public override Field CreateInstance()
        {
            return new CustomField();
        }

        protected override DocumentFragment GetResultFragment()
        {
            int totalPageCount = 1;
            int totalPageInCurrentSection = 1;
            int currentPageInCurrentSection = 1;

            if (this.EvaluationContext != null)
            {
                totalPageCount = this.GetTotalPageCount();

                LayoutBox box = this.EvaluationContext.AssociatedLayoutBoxInMainDocument ?? this.FieldStart.FirstLayoutBox;

                currentPageInCurrentSection = this.GetCurrentPageFromSectionPages(box);
                totalPageInCurrentSection = this.GetPageCountInSection(box);
            }

            return DocumentFragment.CreateFromInline(new Span(
                string.Format("Total pages in the document: {0}. Pages in current section: {1} / {2}", 
                               totalPageCount, currentPageInCurrentSection, totalPageInCurrentSection)));
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

            foreach (var section in document.Sections)
            {
                pageCounter += section.GetAssociatedLayoutBoxes().Count();
            }

            return pageCounter;
        }
    }
}
