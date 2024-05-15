using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Flow;
using Telerik.Windows.Documents.Fixed.Model.Fonts;
using Telerik.Windows.Documents.Fixed.Model.Graphics;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace GenerateDocument
{
    public class ExampleViewModel
    {
        private static readonly double defaultLeftIndent = 50;
        private static readonly double defaultLineHeight = 16;

        public ExampleViewModel()
        {
            this.Save = new DelegateCommand(this.Export);
        }

        public ICommand Save { get; private set; }

        private static void CenterText(FixedContentEditor editor, string text)
        {
            Block block = new Block();
            block.HorizontalAlignment = HorizontalAlignment.Center;
            block.VerticalAlignment = VerticalAlignment.Center;
            block.GraphicProperties.FillColor = RgbColors.White;
            block.InsertText(text);

            editor.DrawBlock(block, new Size(96, 96));
        }

        private void Export(object param)
        {
            PdfFormatProvider formatProvider = new PdfFormatProvider();
            formatProvider.ExportSettings.ImageQuality = ImageQuality.High;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = String.Format("{0} files|*.{1}", "Pdf", "pdf");

            if (dialog.ShowDialog() == true)
            {
                using (var stream = dialog.OpenFile())
                {
                    RadFixedDocument document = this.CreateDocument();
                    formatProvider.Export(document, stream);
                }
            }
        }

        private RadFixedDocument CreateDocument()
        {
            RadFixedDocument document = new RadFixedDocument();
            RadFixedPage page = document.Pages.AddPage();
            page.Size = new Size(600, 750);
            FixedContentEditor editor = new FixedContentEditor(page);
            editor.Position.Translate(defaultLeftIndent, 50);
            using (Stream stream = FileHelper.GetSampleResourceStream("pdfProcessingSilverlight.jpg"))
            {
                editor.DrawImage(stream);
            }
            double currentTopOffset = 110;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            editor.TextProperties.FontSize = 14;
            double maxWidth = page.Size.Width - defaultLeftIndent * 2;

            this.DrawDescription(editor, maxWidth);

            currentTopOffset += defaultLineHeight * 4;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            using (editor.SaveProperties())
            {
                this.DrawFunnelFigure(editor);
            }

            editor.Position.Translate(defaultLeftIndent * 4, page.Size.Height - 100);
            using (Stream stream = FileHelper.GetSampleResourceStream("telerik.jpg"))
            {
                editor.DrawImage(stream);
            }

            this.DrawText(editor, maxWidth);

            return document;
        }

        private void DrawDescription(FixedContentEditor editor, double maxWidth)
        {
            Block block = new Block();
            block.GraphicProperties.FillColor = RgbColors.Black;
            block.HorizontalAlignment = HorizontalAlignment.Left;
            block.TextProperties.Font = FontsRepository.HelveticaBoldOblique;
            block.InsertText("RadPdfProcessing");
            block.TextProperties.Font = FontsRepository.Helvetica;
            block.InsertText(" is a document processing library that enables your application to import and export files to and from PDF format. The document model is entirely independent from UI and allows you to generate sleek documents with differently formatted text, images, shapes and more.");

            editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));
        }

        private void DrawText(FixedContentEditor editor, double maxWidth)
        {
            double currentTopOffset = 500;
            currentTopOffset += defaultLineHeight * 2;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            Block block = new Block();
            block.TextProperties.Font = FontsRepository.Helvetica;
            block.InsertText("A wizard's job is to vex ");
            using (block.GraphicProperties.Save())
            {
                block.GraphicProperties.FillColor = new RgbColor(255, 146, 208, 80);
                block.InsertText("chumps");
            }

            block.InsertText(" quickly in fog.");
            editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            block = new Block();
            block.TextProperties.Font = FontsRepository.TimesRoman;
            block.InsertText("A wizard's job is to vex chumps ");
            using (block.TextProperties.Save())
            {
                block.TextProperties.UnderlinePattern = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.UnderlinePattern.Single;
                block.TextProperties.UnderlineColor = editor.GraphicProperties.FillColor;
                block.InsertText("quickly");
            }

            block.InsertText(" in fog.");
            editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            block = new Block();
            block.TextProperties.Font = FontsRepository.Courier;
            block.InsertText("A ");
            using (block.TextProperties.Save())
            {
                block.TextProperties.Font = FontsRepository.CourierBoldOblique;
                block.TextProperties.UnderlinePattern = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.UnderlinePattern.Single;
                block.TextProperties.UnderlineColor = editor.GraphicProperties.FillColor;
                block.InsertText("wizard's");
            }

            block.InsertText(" job is to vex chumps quickly in fog.");
            editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));
        }

        private void DrawFunnelFigure(FixedContentEditor editor)
        {
            editor.GraphicProperties.IsStroked = false;
            editor.GraphicProperties.FillColor = new RgbColor(231, 238, 247);
            editor.DrawEllipse(new Point(250, 70), 136, 48);

            editor.GraphicProperties.IsStroked = true;
            editor.GraphicProperties.StrokeColor = RgbColors.White;
            editor.GraphicProperties.StrokeThickness = 1;
            editor.GraphicProperties.FillColor = new RgbColor(91, 155, 223);
            editor.DrawEllipse(new Point(289, 77), 48, 48);

            editor.Position.Translate(291, 204);
            CenterText(editor, "Fonts");

            editor.Position.Translate(0, 0);
            editor.DrawEllipse(new Point(238, 274), 48, 48);
            editor.Position.Translate(190, 226);
            CenterText(editor, "Images");

            editor.Position.Translate(0, 0);
            editor.DrawEllipse(new Point(307, 347), 48, 48);
            editor.Position.Translate(259, 299);
            CenterText(editor, "Shapes");

            editor.Position.Translate(0, 0);
            PathGeometry arrow = new PathGeometry();
            PathFigure figure = arrow.Figures.AddPathFigure();
            figure.StartPoint = new Point(287, 422);
            figure.IsClosed = true;
            figure.Segments.AddLineSegment(new Point(287, 438));
            figure.Segments.AddLineSegment(new Point(278, 438));
            figure.Segments.AddLineSegment(new Point(300, 454));
            figure.Segments.AddLineSegment(new Point(322, 438));
            figure.Segments.AddLineSegment(new Point(313, 438));
            figure.Segments.AddLineSegment(new Point(313, 422));

            editor.DrawPath(arrow);

            editor.GraphicProperties.FillColor = new RgbColor(80, 255, 255, 255);
            editor.GraphicProperties.IsStroked = true;
            editor.GraphicProperties.StrokeThickness = 1;
            editor.GraphicProperties.StrokeColor = new RgbColor(91, 155, 223);

            PathGeometry funnel = new PathGeometry();
            funnel.FillRule = FillRule.EvenOdd;
            figure = funnel.Figures.AddPathFigure();
            figure.IsClosed = true;
            figure.StartPoint = new Point(164, 245);
            figure.Segments.AddArcSegment(new Point(436, 245), 136, 48);
            figure.Segments.AddArcSegment(new Point(164, 245), 136, 48);

            figure = funnel.Figures.AddPathFigure();
            figure.IsClosed = true;
            figure.StartPoint = new Point(151, 245);
            figure.Segments.AddArcSegment(new Point(449, 245), 149, 61);
            figure.Segments.AddLineSegment(new Point(332, 415)); figure.Segments.AddArcSegment(new Point(268, 415), 16, 4);

            editor.DrawPath(funnel);

            editor.Position.Translate(164, 455);
            Block block = new Block();
            block.TextProperties.Font = editor.TextProperties.Font;
            block.GraphicProperties.FillColor = RgbColors.Black;
            block.HorizontalAlignment = HorizontalAlignment.Center;
            block.VerticalAlignment = VerticalAlignment.Top;
            block.TextProperties.FontSize = 18;
            block.InsertText("PDF");
            editor.DrawBlock(block, new Size(272, double.PositiveInfinity));
        }
    }
}