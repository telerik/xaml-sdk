using System.Diagnostics;
using System.IO;
#if NETCOREAPP
using Telerik.Documents.Core.Fonts;
using Telerik.Documents.Primitives;
using Telerik.Windows.Documents.Extensibility;
#else
using System.Windows.Media;
using System.Windows;
#endif
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Tables;
using Telerik.Windows.Documents.Fixed.Model.Fonts;
using Editing = Telerik.Windows.Documents.Fixed.Model.Editing;

namespace CreatePdfUsingRadFixedDocumentEditor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
#if NETCOREAPP
            FontsProviderBase fontsProvider = new FontsProvider();
            FixedExtensibilityManager.FontsProvider = fontsProvider;
#endif

            GenerateExportAndViewDocument(true, "resultWithStandardFonts.pdf");
            GenerateExportAndViewDocument(false, "resultWithEmbeddedFonts.pdf");
        }

        private static void GenerateExportAndViewDocument(bool useStandardFonts, string resultFileName)
        {
            RadFixedDocument document = GenerateSampleDocument(useStandardFonts);

            if (File.Exists(resultFileName))
            {
                File.Delete(resultFileName);
            }

            PdfFormatProvider provider = new PdfFormatProvider();
            File.WriteAllBytes(resultFileName, provider.Export(document, null));

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = resultFileName,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private static RadFixedDocument GenerateSampleDocument(bool useOnlyStandardFonts)
        {
            FontBase boldItalicFont, normalFont, serifBoldItalic;
            GetDemoFonts(useOnlyStandardFonts, out boldItalicFont, out normalFont, out serifBoldItalic);
            double paragraphFontSize = useOnlyStandardFonts ? 15 : 14;

            RadFixedDocument document = new RadFixedDocument();
            using (RadFixedDocumentEditor editor = new RadFixedDocumentEditor(document))
            {
                editor.ParagraphProperties.SpacingBefore = 10;
                editor.ParagraphProperties.HorizontalAlignment = Editing.Flow.HorizontalAlignment.Center;
                editor.InsertParagraph();
                editor.CharacterProperties.FontSize = 40;
                editor.CharacterProperties.Font = boldItalicFont;
                editor.InsertRun("Centaur Q3 2014 features");
                editor.InsertLineBreak();
                editor.CharacterProperties.FontSize = 26;
                editor.InsertRun("PdfProcessing");
                editor.InsertLineBreak();

                editor.ParagraphProperties.HorizontalAlignment = Editing.Flow.HorizontalAlignment.Left;
                editor.InsertParagraph();
                editor.CharacterProperties.Font = boldItalicFont;
                editor.CharacterProperties.FontSize = 20;
                editor.InsertRun("Simple paragraphs drawn with RadFixedDocumentEditor:");
                editor.InsertParagraph();
                editor.CharacterProperties.FontSize = paragraphFontSize;
                editor.CharacterProperties.Font = normalFont;
                editor.InsertRun(ContentGenerator.GetParagraphText(1));
                editor.InsertParagraph();
                editor.InsertRun(ContentGenerator.GetParagraphText(2));
                editor.InsertParagraph();
                editor.InsertRun(ContentGenerator.GetParagraphText(2));

                editor.InsertParagraph();
                editor.CharacterProperties.Font = boldItalicFont;
                editor.CharacterProperties.FontSize = 20;
                editor.InsertRun("Paragraph containing images");
                editor.InsertParagraph();
                editor.CharacterProperties.FontSize = paragraphFontSize;
                editor.CharacterProperties.Font = normalFont;
                editor.InsertRun("This paragraphs contains inline images like this one:");
                using (Stream sampleImage = ContentGenerator.GetSampleImageStream())
                {
                    var imageSource = new Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource(sampleImage);
                    editor.InsertImageInline(imageSource, new Size(40, 40));
                    editor.InsertRun(", this one:");
                    editor.InsertImageInline(imageSource, new Size(100, 100));
                    editor.InsertRun(" and this one:");
                    editor.InsertImageInline(imageSource, new Size(100, 60));
                    editor.InsertRun(ContentGenerator.GetParagraphText(2));
                }

                editor.InsertParagraph();
                editor.CharacterProperties.Font = boldItalicFont;
                editor.CharacterProperties.FontSize = 20;
                editor.InsertRun("Simple table example");
                Table simpleTable = ContentGenerator.GetSimpleTable(40);
                simpleTable.LayoutType = Editing.Flow.TableLayoutType.FixedWidth;
                editor.InsertTable(simpleTable);

                editor.InsertParagraph();
                editor.InsertRun("Complex table with images, geometries and merged cells.");
                Table complexTable = ContentGenerator.GetComplexTable(serifBoldItalic);
                complexTable.LayoutType = Editing.Flow.TableLayoutType.FixedWidth;
                editor.InsertTable(complexTable);

                editor.InsertParagraph();
                editor.InsertRun("THE END");
            }

            return document;
        }

        private static void GetDemoFonts(bool useOnlyStandardFonts, out FontBase boldItalicFont, out FontBase normalFont, out FontBase serifBoldItalic)
        {
            if (useOnlyStandardFonts)
            {
                boldItalicFont = FontsRepository.CourierBoldOblique;
                normalFont = FontsRepository.Courier;
                serifBoldItalic = FontsRepository.TimesBoldItalic;
            }
            else
            {
                FontsRepository.TryCreateFont(new FontFamily("Arial"), FontStyles.Italic, FontWeights.Bold, out boldItalicFont);
                FontsRepository.TryCreateFont(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Normal, out normalFont);
                FontsRepository.TryCreateFont(new FontFamily("Jokerman"), FontStyles.Italic, FontWeights.Bold, out serifBoldItalic);
            }
        }
    }
}
