using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Data;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Fonts;
using Telerik.Windows.Documents.Fixed.Model.Graphics;

namespace GenerateDocument
{
    public class ExampleViewModel
    {
        private static readonly double defaultLeftIndent = 50;
        private static readonly double defaultLineHeight = 18;

        public ExampleViewModel()
        {
            this.Save = new DelegateCommand(this.Export);
        }

        public ICommand Save { get; private set; }

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
            editor.TextProperties.LineHeight = defaultLineHeight;
            double maxWidth = page.Size.Width - defaultLeftIndent * 2;

            this.DrawDescription(editor, maxWidth);

            currentTopOffset += defaultLineHeight * 5;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);

            using (editor.SaveGraphicProperties())
            {
                using (editor.SaveTextProperties())
                {
                    this.DrawFunnelFigure(editor);
                }
            }

            editor.Position.Translate(defaultLeftIndent * 4, page.Size.Height - 100);
            using (Stream stream = FileHelper.GetSampleResourceStream("telerik.jpg"))
            {
                editor.DrawImage(stream);
            }

            this.DrawText(editor);

            return document;
        }

        private void DrawDescription(FixedContentEditor editor, double maxWidth)
        {
            editor.GraphicProperties.FillColor = RgbColors.Black;
            editor.TextProperties.HorizontalAlignment = HorizontalTextAlignment.Left;
            editor.TextProperties.TextBlockWidth = maxWidth;
            using (editor.BeginText())
            {
                editor.TextProperties.Font = FontsRepository.HelveticaBoldOblique;
                editor.DrawText("RadPdfProcessing");
                editor.TextProperties.Font = FontsRepository.Helvetica;
                editor.DrawText(" is a document processing library that enables your application to import and export files to and from PDF format. The document model is entirely independent from UI and allows you to generate sleek documents with differently formatted text, images, shapes and more.");
            }
        }

        private void DrawText(FixedContentEditor editor)
        {
            double currentTopOffset = 500;
            currentTopOffset += defaultLineHeight * 2;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            using (editor.BeginText())
            {
                editor.TextProperties.Font = FontsRepository.Helvetica;
                editor.DrawText("A wizard's job is to vex ");
                using (editor.SaveGraphicProperties())
                {
                    editor.GraphicProperties.FillColor = new RgbColor(255, 146, 208, 80);
                    editor.DrawText("chumps");
                }
                editor.DrawText(" quickly in fog.");
            }

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            using (editor.BeginText())
            {
                editor.TextProperties.Font = FontsRepository.TimesRoman;
                editor.DrawText("A wizard's job is to vex chumps ");
                using (editor.SaveTextProperties())
                {
                    editor.TextProperties.TextDecoration = Telerik.Windows.Documents.Fixed.Model.Text.TextDecorations.Underline;
                    editor.DrawText("quickly");
                }
                editor.DrawText(" in fog.");
            }

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            using (editor.BeginText())
            {
                editor.TextProperties.Font = FontsRepository.Courier;
                editor.DrawText("A ");
                using (editor.SaveTextProperties())
                {
                    editor.TextProperties.Font = FontsRepository.CourierBoldOblique;
                    editor.TextProperties.TextDecoration = Telerik.Windows.Documents.Fixed.Model.Text.TextDecorations.Underline;
                    editor.DrawText("wizard's");
                }
                editor.DrawText(" job is to vex chumps quickly in fog.");
            }
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
            editor.TextProperties.Font = FontsRepository.Helvetica;
            editor.TextProperties.HorizontalAlignment = HorizontalTextAlignment.Center;
            editor.TextProperties.VerticalAlignment = VerticalTextAlignment.Center;
            editor.TextProperties.TextBlockWidth = 96;
            editor.TextProperties.TextBlockHeight = 96;
            using (editor.SaveGraphicProperties())
            {
                editor.Position.Translate(291, 229);
                editor.GraphicProperties.FillColor = RgbColors.White;
                editor.DrawText("Fonts");
            }

            editor.Position.Translate(0, 0);
            editor.DrawEllipse(new Point(238, 299), 48, 48);
            using (editor.SaveGraphicProperties())
            {
                editor.Position.Translate(190, 251);
                editor.GraphicProperties.FillColor = RgbColors.White;
                editor.DrawText("Images");
            }

            editor.Position.Translate(0, 0);
            editor.DrawEllipse(new Point(307, 372), 48, 48);
            using (editor.SaveGraphicProperties())
            {
                editor.Position.Translate(259, 324);
                editor.GraphicProperties.FillColor = RgbColors.White;
                editor.DrawText("Shapes");
            }

            editor.Position.Translate(0, 0);
            PathGeometry arrow = new PathGeometry();
            PathFigure figure = arrow.Figures.AddPathFigure();
            figure.StartPoint = new Point(287, 447);
            figure.IsClosed = true;
            figure.Segments.AddLineSegment(new Point(287, 463));
            figure.Segments.AddLineSegment(new Point(278, 463));
            figure.Segments.AddLineSegment(new Point(300, 479));
            figure.Segments.AddLineSegment(new Point(322, 463));
            figure.Segments.AddLineSegment(new Point(313, 463));
            figure.Segments.AddLineSegment(new Point(313, 447));

            editor.DrawPath(arrow);

            editor.GraphicProperties.FillColor = new RgbColor(80, 255, 255, 255);
            editor.GraphicProperties.IsStroked = true;
            editor.GraphicProperties.StrokeThickness = 1;
            editor.GraphicProperties.StrokeColor = new RgbColor(91, 155, 223);

            PathGeometry funnel = new PathGeometry();
            funnel.FillRule = FillRule.EvenOdd;
            figure = funnel.Figures.AddPathFigure();
            figure.IsClosed = true;
            figure.StartPoint = new Point(164, 270);
            figure.Segments.AddArcSegment(new Point(436, 270), 136, 48);
            figure.Segments.AddArcSegment(new Point(164, 270), 136, 48);

            figure = funnel.Figures.AddPathFigure();
            figure.IsClosed = true;
            figure.StartPoint = new Point(151, 270);
            figure.Segments.AddArcSegment(new Point(449, 270), 149, 61);
            figure.Segments.AddLineSegment(new Point(332, 440));
            figure.Segments.AddArcSegment(new Point(268, 440), 16, 4);

            editor.DrawPath(funnel);

            using (editor.SaveGraphicProperties())
            {
                using (editor.SaveTextProperties())
                {
                    editor.Position.Translate(164, 484);
                    editor.GraphicProperties.FillColor = RgbColors.Black;
                    editor.TextProperties.HorizontalAlignment = HorizontalTextAlignment.Center;
                    editor.TextProperties.VerticalAlignment = VerticalTextAlignment.Top;
                    editor.TextProperties.TextBlockWidth = 272;
                    editor.TextProperties.FontSize = 18;
                    editor.DrawText("PDF");
                }
            }
        }
    }
}