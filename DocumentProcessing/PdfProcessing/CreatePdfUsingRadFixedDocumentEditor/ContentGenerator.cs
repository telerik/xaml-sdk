using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
#if NETCOREAPP
using Telerik.Documents.Primitives;
#else
using System.Windows;
#endif
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Tables;
using Telerik.Windows.Documents.Fixed.Model.Fonts;
using Telerik.Windows.Documents.Fixed.Model.Graphics;

namespace CreatePdfUsingRadFixedDocumentEditor
{
    public static class ContentGenerator
    {
        private const string LoremIpsumText = @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi. Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum. Typi non habent claritatem insitam; est usus legentis in iis qui facit eorum claritatem. Investigationes demonstraverunt lectores legere me lius quod ii legunt saepius. Claritas est etiam processus dynamicus, qui sequitur mutationem consuetudium lectorum. Mirum est notare quam littera gothica, quam nunc putamus parum claram, anteposuerit litterarum formas humanitatis per seacula quarta decima et quinta decima. Eodem modo typi, qui nunc nobis videntur parum clari, fiant sollemnes in futurum. ";
        private const string GoldenSpiralApproximationText = @"A golden spiral can be approximated by first starting with a rectangle for which the ratio between its length and width is the golden ratio.";

        public static string GetParagraphText(int repeatCount)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < repeatCount; i++)
            {
                builder.Append(LoremIpsumText);
            }

            return builder.ToString();
        }

        public static Table GetSimpleTable(int productRows)
        {
            Table table = new Table();

            RgbColor headerColor = new RgbColor(79, 129, 189);
            RgbColor bordersColor = new RgbColor(149, 179, 215);
            RgbColor alternatingRowColor = new RgbColor(219, 229, 241);

            Border border = new Border(1, BorderStyle.Single, bordersColor);

            table.Borders = new TableBorders(border);
            table.DefaultCellProperties.Borders = new TableCellBorders(border, border, border, border);
            table.DefaultCellProperties.Padding = new Thickness(2);

            TableRow headerRow = table.Rows.AddTableRow();
            TableCell headerCell = headerRow.Cells.AddTableCell();
            headerCell.Borders = new TableCellBorders(new Border(BorderStyle.None));
            headerCell.ColumnSpan = 5;
            Block headerBlock = headerCell.Blocks.AddBlock();
            headerBlock.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
            using (Stream companyImage = GetResourceStream("Resources/abCompany.jpg"))
            {
                headerBlock.InsertImage(companyImage);
            }

            TableRow quartersRow = table.Rows.AddTableRow();
            TableCell cell = quartersRow.Cells.AddTableCell();
            cell.Background = headerColor;
            cell.Borders = new TableCellBorders(border, border, border, border, null, border);

            for (int i = 0; i < 4; i++)
            {
                TableCell quarterCell = quartersRow.Cells.AddTableCell();
                quarterCell.Background = headerColor;

                Block quarterBlock = quarterCell.Blocks.AddBlock();
                quarterBlock.GraphicProperties.FillColor = RgbColors.White;
                quarterBlock.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
                quarterBlock.InsertText(String.Format("Q{0}", i + 1));
            }

            List<Product> products = Product.GetProducts(productRows);

            for (int i = 0; i < products.Count; i++)
            {
                RgbColor rowColor = i % 2 == 0 ? alternatingRowColor : RgbColors.White;
                Product product = products[i];

                TableRow productRow = table.Rows.AddTableRow();
                TableCell productNameCell = productRow.Cells.AddTableCell();
                productNameCell.Background = rowColor;
                Block nameBlock = productNameCell.Blocks.AddBlock();
                nameBlock.InsertText(product.Name);

                for (int quarterIndex = 0; quarterIndex < 4; quarterIndex++)
                {
                    TableCell quarterAmountCell = productRow.Cells.AddTableCell();
                    quarterAmountCell.Background = rowColor;
                    Block amountBlock = quarterAmountCell.Blocks.AddBlock();
                    amountBlock.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Right;
                    amountBlock.InsertText(string.Format("{0:C}", product.Q[quarterIndex]));
                }
            }

            return table;
        }

        public static Table GetComplexTable(FontBase bigTextFont)
        {
            Table table = new Table();
            table.Margin = new Thickness(0, 10, 0, 0);
            table.Borders = new TableBorders(new Border(10, RgbColors.Black));
            Border defaultBorder = new Border(3, RgbColors.Black);
            table.DefaultCellProperties.Borders = new TableCellBorders(defaultBorder, defaultBorder, defaultBorder, defaultBorder);
            table.DefaultCellProperties.Padding = new Thickness(10);
            table.BorderSpacing = 5;
            table.BorderCollapse = BorderCollapse.Separate;

            TableRow row = table.Rows.AddTableRow();

            TableCell cell = row.Cells.AddTableCell();
            cell.Background = new RgbColor(255, 230, 230);
            cell.RowSpan = 2;
            cell.ColumnSpan = 2;
            Border redBorder = new Border(5, new RgbColor(255, 0, 0));
            cell.Borders = new TableCellBorders(redBorder, redBorder, redBorder, redBorder);
            Block block = cell.Blocks.AddBlock();
            block.InsertText("This is a ");
            using (block.SaveGraphicProperties())
            {
                block.GraphicProperties.FillColor = new RgbColor(0, 0, 255);
                block.InsertHyperlinkStart(new Uri("http://en.wikipedia.org/wiki/Golden_spiral"));
                block.InsertText("Golden spiral");
                block.InsertHyperlinkEnd();
            }
            block.InsertText(" approximation: ");

            PathGeometry spiralGeometry, rectanglesGeometry;

            using (block.SaveGraphicProperties())
            {
                block.GraphicProperties.IsFilled = false;
                block.GraphicProperties.IsStroked = true;
                GetGoldenSpiralGeometry(10, out spiralGeometry, out rectanglesGeometry);
                block.GraphicProperties.StrokeColor = new RgbColor(0, 0, 255);
                block.GraphicProperties.StrokeThickness = 1;
                rectanglesGeometry.Figures.Add(spiralGeometry.Figures[0]);
                block.InsertPath(rectanglesGeometry);
            }

            block.InsertText(GoldenSpiralApproximationText);

            using (Stream sampleImage = GetSampleImageStream())
            {
                cell = row.Cells.AddTableCell();
                cell.Blocks.AddBlock().InsertImage(sampleImage, new Size(200, 124));
            }

            table.Rows.AddTableRow().Cells.AddTableCell().Blocks.AddBlock().InsertText(GoldenSpiralApproximationText);

            row = table.Rows.AddTableRow();

            cell = row.Cells.AddTableCell();
            block = cell.Blocks.AddBlock();
            block.TextProperties.FontSize = 30;
            block.TextProperties.Font = bigTextFont;
            block.InsertText("Red Golden Spiral:");

            cell = row.Cells.AddTableCell();
            Border greenBorder = new Border(10, new RgbColor(0, 150, 0));
            cell.Borders = new TableCellBorders(greenBorder, greenBorder, greenBorder, greenBorder);
            cell.Background = new RgbColor(230, 255, 230);
            cell.ColumnSpan = 2;
            block = cell.Blocks.AddBlock();
            block.GraphicProperties.IsFilled = false;
            block.GraphicProperties.IsStroked = true;
            block.GraphicProperties.StrokeColor = new RgbColor(255, 0, 0);
            block.GraphicProperties.StrokeThickness = 4;
            block.InsertPath(spiralGeometry);

            return table;
        }

        private static void GetGoldenSpiralGeometry(int arcsCount, out PathGeometry spiralGeometry, out PathGeometry rectanglesGeometry)
        {
            double sin45 = 1 / Math.Sqrt(2);

            Vector[] directions =
            {
                new Vector(1, -1) * sin45,
                new Vector(1, 1) * sin45,
                new Vector(-1, 1) * sin45,
                new Vector(-1, -1) * sin45
            };

            double length = 100;
            double fi = (Math.Sqrt(5) - 1) / 2;

            spiralGeometry = new PathGeometry();
            rectanglesGeometry = new PathGeometry();
            PathFigure spiralFigure = spiralGeometry.Figures.AddPathFigure();
            spiralFigure.StartPoint = new Point(0, length);
            Point previousPoint = spiralFigure.StartPoint;

            for (int i = 0; i < arcsCount; i++)
            {
                Vector delta = directions[i % 4] * (length / sin45);
                Point point = previousPoint + delta;

                PathFigure rectanglePath = rectanglesGeometry.Figures.AddPathFigure();
                double x = Math.Min(previousPoint.X, point.X);
                double y = Math.Min(previousPoint.Y, point.Y);
                double width = Math.Max(Math.Max(previousPoint.X, point.X) - x, 0);
                double height = Math.Max(Math.Max(previousPoint.Y, point.Y) - y, 0);
                Rect rect = new Rect(x, y, width, height);
                rectanglePath.StartPoint = rect.TopLeft;
                rectanglePath.Segments.AddLineSegment().Point = rect.TopRight;
                rectanglePath.Segments.AddLineSegment().Point = rect.BottomRight;
                rectanglePath.Segments.AddLineSegment().Point = rect.BottomLeft;
                rectanglePath.IsClosed = true;

                ArcSegment arc = spiralFigure.Segments.AddArcSegment();
                arc.RadiusX = length;
                arc.RadiusY = length;
                arc.SweepDirection = SweepDirection.Clockwise;
                arc.RotationAngle = 45;
                arc.Point = point;

                previousPoint = point;
                length *= fi;
            }
        }

        public static Stream GetSampleImageStream()
        {
            return GetResourceStream("Resources/SEB-Ninja.jpg");
        }

        private static Stream GetResourceStream(string relativePath)
        {
            return File.OpenRead(relativePath);
        }
    }
}
