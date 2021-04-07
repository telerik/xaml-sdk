using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
#if NETCOREAPP
using Telerik.Documents.Primitives;
#else
using System.Windows;
#endif
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Collections;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Lists;
using Telerik.Windows.Documents.Flow.Model.Shapes;
using Telerik.Windows.Documents.Flow.Model.StructuredDocumentTags;
using Telerik.Windows.Documents.Flow.Model.Styles;
using Telerik.Windows.Documents.Media;

namespace ContentControls
{
    class DocumentGenerator
    {
#if NETCOREAPP
        private static readonly string SampleDataFolder = "../../../SampleData/";
#else
        private static readonly string SampleDataFolder = "../../SampleData/";
#endif
        private static readonly string TemplatePath = SampleDataFolder + "CVTemplate.docx";
        private static readonly string ImagePath = SampleDataFolder + "TelerikNinja.png";
        private static readonly string Heading1StyleId = BuiltInStyleNames.GetHeadingStyleIdByIndex(1);


        public void Generate()
        {
            RadFlowDocument template = this.OpenSample();
            RadFlowDocument document = this.CreateDocument(template);

            this.Save(document);
        }

        private RadFlowDocument OpenSample()
        {
            using (Stream stream = File.OpenRead(TemplatePath))
            {
                DocxFormatProvider docxFormatProvider = new DocxFormatProvider();
                return docxFormatProvider.Import(stream);
            }
        }

        private void Save(RadFlowDocument document)
        {
            IFormatProvider<RadFlowDocument> formatProvider = new DocxFormatProvider();

            string path = "CVTemplate.docx";
            using (FileStream stream = File.OpenWrite(path))
            {
                formatProvider.Export(document, stream);
            }

            Console.WriteLine("Document generated.");

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true
            };

            Process.Start(psi);
        }

        private RadFlowDocument CreateDocument(RadFlowDocument template)
        {
            RadFlowDocument document = template;
            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(document);
            Section firstSection = document.Sections[0];

            this.PersonalDetailsSection(editor, firstSection);
            this.SummarySection(editor, firstSection);
            this.ExperienceSection(document, editor, firstSection);
            this.SkillsSection(editor, firstSection);

            return document;
        }

        private void PersonalDetailsSection(RadFlowDocumentEditor editor, Section firstSection)
        {
            double tabStopPosition = Unit.InchToDip(6);

            #region Name & Photo

            Paragraph nameAndPhotoParagraph = firstSection.Blocks.AddParagraph();
            nameAndPhotoParagraph.TabStops = nameAndPhotoParagraph.TabStops.Insert(new TabStop(tabStopPosition, TabStopType.Right));
            nameAndPhotoParagraph.StyleId = Heading1StyleId;


            Run nameRun = nameAndPhotoParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, nameRun, "Enter your name", SdtType.RichText);

            nameAndPhotoParagraph.Inlines.AddRun("\t");

            ImageInline imageInline = new ImageInline(firstSection.Document);
            imageInline.Image.Size = new Size(200, 200);
            imageInline.Image.ImageSource = new ImageSource(File.ReadAllBytes(ImagePath), ".png");
            nameAndPhotoParagraph.Inlines.Add(imageInline);
            this.SetupAndInsertSdt(editor, imageInline, null, SdtType.Picture);

            #endregion


            #region Role

            Paragraph roleParagraph = firstSection.Blocks.AddParagraph();
            Run roleRun = roleParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, roleRun, "Your desired role?", SdtType.RichText);

            #endregion


            #region Phone & Email

            Paragraph phoneAndEmailParagraph = firstSection.Blocks.AddParagraph();
            phoneAndEmailParagraph.TabStops = phoneAndEmailParagraph.TabStops.Insert(new TabStop(tabStopPosition, TabStopType.Right));

            Run phoneRun = phoneAndEmailParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, phoneRun, "Phone number", SdtType.RichText);

            phoneAndEmailParagraph.Inlines.AddRun("\t");

            Run emailRun = phoneAndEmailParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, emailRun, "Email address", SdtType.RichText);

            #endregion


            #region Website & Location

            Paragraph websiteAndLocationParagraph = firstSection.Blocks.AddParagraph();
            websiteAndLocationParagraph.TabStops = websiteAndLocationParagraph.TabStops.Insert(new TabStop(tabStopPosition, TabStopType.Right));

            InlineCollection websiteAndLocationInlines = websiteAndLocationParagraph.Inlines;
            Run websiteRun = websiteAndLocationInlines.AddRun();
            this.SetupAndInsertSdt(editor, websiteRun, "Website", SdtType.RichText);

            websiteAndLocationInlines.AddRun("\t");

            Run locationRun = websiteAndLocationInlines.AddRun();
            this.SetupAndInsertSdt(editor, locationRun, "Location", SdtType.RichText);

            #endregion
        }

        private void SummarySection(RadFlowDocumentEditor editor, Section firstSection)
        {
            Paragraph summaryTitleParagraph = editor.InsertParagraph();
            summaryTitleParagraph.StyleId = Heading1StyleId;
            summaryTitleParagraph.Inlines.AddRun("Summary");

            #region Summary

            Paragraph summaryContentParagraph = firstSection.Blocks.AddParagraph();
            Run summaryRun = summaryContentParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, summaryRun, "What`s the one thing you want someone to remember after reading your resume?", SdtType.RichText);

            #endregion
        }

        private void ExperienceSection(RadFlowDocument document, RadFlowDocumentEditor editor, Section firstSection)
        {
            Paragraph experienceTitleParagraph = editor.InsertParagraph();
            experienceTitleParagraph.StyleId = Heading1StyleId;
            experienceTitleParagraph.Inlines.AddRun("Experience");

            #region Title

            Paragraph titleContentParagraph = firstSection.Blocks.AddParagraph();
            Run titleRun = titleContentParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, titleRun, "Title", SdtType.RichText);

            #endregion


            #region Company Name

            Paragraph companyNameParagraph = firstSection.Blocks.AddParagraph();
            Run companyRun = companyNameParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, companyRun, "Company name", SdtType.RichText);

            #endregion


            #region Period

            Paragraph periodParagraph = firstSection.Blocks.AddParagraph();

            Run startDateRun = periodParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, startDateRun, "Select start date", SdtType.Date);

            periodParagraph.Inlines.AddRun(" - ");

            Run endDateRun = periodParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, endDateRun, "Select end date", SdtType.Date);

            #endregion


            #region Company Description

            Paragraph companyDescriptionParagraph = firstSection.Blocks.AddParagraph();
            Run companyDescriptionRun = companyDescriptionParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, companyDescriptionRun, "Company Description", SdtType.RichText);

            #region Achievements

            List list = document.Lists.Add(ListTemplateType.BulletDefault);

            Paragraph achievementsParagraph = firstSection.Blocks.AddParagraph();
            achievementsParagraph.ListId = list.Id;
            Run achievementsRun = achievementsParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, achievementsRun, "What did you achieve in this role?", SdtType.RichText);

            editor.InsertStructuredDocumentTag(SdtType.RepeatingSectionItem, achievementsParagraph, achievementsParagraph);
            this.SetupAndInsertSdt(editor, achievementsParagraph.Inlines.First(), null, SdtType.RepeatingSection, achievementsParagraph);

            #endregion

            #endregion
        }

        private void SkillsSection(RadFlowDocumentEditor editor, Section firstSection)
        {
            Paragraph skillsTitleParagpraph = editor.InsertParagraph();
            skillsTitleParagpraph.StyleId = Heading1StyleId;
            skillsTitleParagpraph.Inlines.AddRun("Skills");

            #region Tool/Technology

            Paragraph skillsContentParagraph = firstSection.Blocks.AddParagraph();
            Run skillRun = skillsContentParagraph.Inlines.AddRun();
            this.SetupAndInsertSdt(editor, skillRun, "Tool/Technology", SdtType.RichText);

            editor.InsertStructuredDocumentTag(SdtType.RepeatingSectionItem, skillsContentParagraph, skillsContentParagraph);
            this.SetupAndInsertSdt(editor, skillsContentParagraph.Inlines.First(), null, SdtType.RepeatingSection, skillsContentParagraph);

            #endregion
        }

        private void SetupAndInsertSdt(RadFlowDocumentEditor editor, InlineBase inline, string text, SdtType sdtType, DocumentElementBase start = null, DocumentElementBase end = null)
        {
            if (sdtType == SdtType.RepeatingSection)
            {
                editor.MoveToParagraphStart(inline.Paragraph);
            }
            else
            {
                editor.MoveToInlineStart(inline);
            }

            SdtProperties sdt = new SdtProperties(sdtType)
            {
                // All SDTs should not allow deleting them
                Lock = Lock.SdtLocked
            };

            if (!string.IsNullOrEmpty(text))
            {
                // Add a placeholder if text for it is available.
                sdt.Placeholder = new Placeholder()
                {
                    PlaceholderText = text,
                    ShowPlaceholder = true
                };
            }

            if (start == null)
            {
                editor.InsertStructuredDocumentTag(sdt);
            }
            else
            {
                end = end ?? start;
                editor.InsertStructuredDocumentTag(sdt, start, end);
            }
        }
    }
}
