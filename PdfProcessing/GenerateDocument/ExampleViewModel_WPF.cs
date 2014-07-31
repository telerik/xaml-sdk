using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Data;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Graphics;
using FontFamily = System.Windows.Media.FontFamily;

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
                    MessageBox.Show("Saved.");

                }
            }
        }

        private RadFixedDocument CreateDocument()
        {
            RadFixedDocument document = new RadFixedDocument();
            RadFixedPage page = document.Pages.AddPage();
            page.Size = new Size(600, 800);
            FixedContentEditor editor = new FixedContentEditor(page);
            editor.Position.Translate(defaultLeftIndent, 50);
            using (Stream stream = FileHelper.GetSampleResourceStream("pdfProcessingWpf.jpg"))
            {
                editor.DrawImage(stream);
            }
            double currentTopOffset = 110;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            double maxWidth = page.Size.Width - defaultLeftIndent * 2;

            this.DrawDescription(editor, maxWidth);

            currentTopOffset += defaultLineHeight * 4;
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

            this.DrawText(editor, maxWidth);

            return document;
        }

        private void DrawDescription(FixedContentEditor editor, double maxWidth)
        {
            editor.GraphicProperties.FillColor = RgbColors.Black;
            editor.TextProperties.HorizontalAlignment = HorizontalTextAlignment.Left;
            editor.TextProperties.TextBlockWidth = maxWidth;
            using (editor.BeginText())
            {
                editor.TextProperties.FontSize = 14;
                editor.TextProperties.TrySetFont(new FontFamily("Calibri"), FontStyles.Italic, FontWeights.Bold);
                editor.DrawText("RadPdfProcessing");
                editor.TextProperties.TrySetFont(new System.Windows.Media.FontFamily("Calibri"));
                editor.DrawText(" is a document processing library that enables your application to import and export files to and from PDF format. The document model is entirely independent from UI and allows you to generate sleek documents with differently formatted text, images, shapes and more.");
            }
        }

        private void DrawText(FixedContentEditor editor, double maxWidth)
        {
            double currentTopOffset = 470;
            currentTopOffset += defaultLineHeight * 2;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            editor.TextProperties.FontSize = 11;
            using (editor.BeginText())
            {
                editor.TextProperties.TrySetFont(new FontFamily("Arial"));
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
                editor.TextProperties.TrySetFont(new FontFamily("Trebuchet MS"));
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
                editor.TextProperties.TrySetFont(new FontFamily("Algerian"));
                editor.DrawText("A ");
                using (editor.SaveTextProperties())
                {
                    editor.TextProperties.TextDecoration = Telerik.Windows.Documents.Fixed.Model.Text.TextDecorations.Underline;
                    editor.DrawText("wizard's");
                }
                editor.DrawText(" job is to vex chumps quickly in fog.");
            }

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            using (editor.BeginText())
            {
                editor.TextProperties.TrySetFont(new FontFamily("Lucida Calligraphy"));
                editor.DrawText("A wizard's job is to vex chumps quickly in fog.");
            }

            currentTopOffset += defaultLineHeight + 2;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            using (editor.BeginText())
            {
                editor.TextProperties.TrySetFont(new FontFamily("Consolas"));
                editor.DrawText("A wizard's job is to vex chumps ");
                using (editor.SaveTextProperties())
                {
                    editor.TextProperties.TrySetFont(new FontFamily("Consolas"), FontStyles.Normal, FontWeights.Bold);
                    editor.DrawText("quickly");
                }
                editor.DrawText(" in fog.");
            }

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            using (editor.BeginText())
            {
                editor.TextProperties.TrySetFont(new FontFamily("Arial"));
                editor.DrawText("Ταχίστη αλώπηξ βαφής ψημένη γη, δρασκελίζει υπέρ νωθρού κυνός.");
            }

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            using (editor.BeginText())
            {
                editor.DrawText("В чащах юга жил бы цитрус? Да, но фальшивый экземпляр!");
            }

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            editor.TextProperties.TextBlockWidth = maxWidth;
            using (editor.BeginText())
            {
                editor.DrawText("El pingüino Wenceslao hizo kilómetros bajo exhaustiva lluvia y frío; añoraba a su querido cachorro.");
            }

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);

            using (editor.BeginText())
            {
                editor.TextProperties.TrySetFont(new FontFamily("Malgun Gothic"));
                editor.DrawText("키스의 고유조건은 입술끼리 만나야 하고 특별한 기술은 필요치 않다.");
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
            editor.TextProperties.TrySetFont(new FontFamily("Calibri"));
            editor.TextProperties.HorizontalAlignment = HorizontalTextAlignment.Center;
            editor.TextProperties.VerticalAlignment = VerticalTextAlignment.Center;
            editor.TextProperties.TextBlockWidth = 96;
            editor.TextProperties.TextBlockHeight = 96;
            using (editor.SaveGraphicProperties())
            {
                editor.Position.Translate(291, 204);
                editor.GraphicProperties.FillColor = RgbColors.White;
                editor.DrawText("Fonts");
            }

            editor.Position.Translate(0, 0);
            editor.DrawEllipse(new Point(238, 274), 48, 48);
            using (editor.SaveGraphicProperties())
            {
                editor.Position.Translate(190, 226);
                editor.GraphicProperties.FillColor = RgbColors.White;
                editor.DrawText("Images");
            }

            editor.Position.Translate(0, 0);
            editor.DrawEllipse(new Point(307, 347), 48, 48);
            using (editor.SaveGraphicProperties())
            {
                editor.Position.Translate(259, 299);
                editor.GraphicProperties.FillColor = RgbColors.White;
                editor.DrawText("Shapes");
            }

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
            figure.Segments.AddLineSegment(new Point(332, 415));
            figure.Segments.AddArcSegment(new Point(268, 415), 16, 4);

            editor.DrawPath(funnel);

            using (editor.SaveGraphicProperties())
            {
                using (editor.SaveTextProperties())
                {
                    editor.Position.Translate(164, 455);
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
