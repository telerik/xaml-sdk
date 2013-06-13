using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Pivot.Export;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.FormatProviders.Pdf;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace ExportPivotGrid
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
            ObservableCollection<ExportType> exportTypes = new ObservableCollection<ExportType>()
            {
                new ExportType { ImageSource = new Uri("/ExportPivotGrid;component/Icons/ExcelIcon.png", UriKind.RelativeOrAbsolute), ExportFormat = "Excel" },
                new ExportType { ImageSource = new Uri("/ExportPivotGrid;component/Icons/WordIcon.png", UriKind.RelativeOrAbsolute), ExportFormat = "Word" },
                new ExportType { ImageSource = new Uri("/ExportPivotGrid;component/Icons/PdfIcon.png", UriKind.RelativeOrAbsolute), ExportFormat = "PDF" },
                new ExportType { ImageSource = new Uri("/ExportPivotGrid;component/Icons/HtmlIcon.png", UriKind.RelativeOrAbsolute), ExportFormat = "HTML" },
            };

            this.ExportToListBox.ItemsSource = exportTypes;
        }


        private void ExportButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.ExportToListBox.SelectedItem == null)
            {
                return;
            }

            string exportTo = ((this.ExportToListBox.SelectedItem) as ExportType).ExportFormat;

            switch (exportTo)
            {
                case "Excel":
                    ExportToExcel();
                    break;
                case "Word":
                    ExportToWord();
                    break;
                case "PDF":
                    ExportToPdf();
                    break;
                case "HTML":
                    ExportToHtml();
                    break;
                default:
                    break;
            }

        }



        private void ExportToExcel()
        {
            this.BusyIndicator.IsBusy = true;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "xlsx";
            dialog.Filter = "Excel Workbook (xlsx) | *.xlsx |All Files (*.*) | *.*";

            var result = dialog.ShowDialog();
            if ((bool)result)
            {
                try
                {
                    using (var stream = dialog.OpenFile())
                    {
                        var workbook = GenerateWorkbook();

                        XlsxFormatProvider provider = new XlsxFormatProvider();
                        provider.Export(workbook, stream);
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.BusyIndicator.IsBusy = false;
        }

        private Workbook GenerateWorkbook()
        {

            var export = pivot.GenerateExport();

            Workbook workbook = new Workbook();
            workbook.History.IsEnabled = false;

            var worksheet = workbook.Worksheets.Add();

            workbook.SuspendLayoutUpdate();
            int rowCount = export.RowCount;
            int columnCount = export.ColumnCount;

            var allCells = worksheet.Cells[0, 0, rowCount - 1, columnCount - 1];
            allCells.SetFontFamily(new ThemableFontFamily(pivot.FontFamily));
            allCells.SetFontSize(12);
            allCells.SetFill(GenerateFill(pivot.Background));

            foreach (var cellInfo in export.Cells)
            {
                int rowStartIndex = cellInfo.Row;
                int rowEndIndex = rowStartIndex + cellInfo.RowSpan - 1;
                int columnStartIndex = cellInfo.Column;
                int columnEndIndex = columnStartIndex + cellInfo.ColumnSpan - 1;

                CellSelection cellSelection = worksheet.Cells[rowStartIndex, columnStartIndex];

                var value = cellInfo.Value;
                if (value != null)
                {
                    cellSelection.SetValue(Convert.ToString(value));
                    cellSelection.SetVerticalAlignment(RadVerticalAlignment.Center);
                    cellSelection.SetHorizontalAlignment(GetHorizontalAlignment(cellInfo.TextAlignment));
                    int indent = cellInfo.Indent;
                    if (indent > 0)
                    {
                        cellSelection.SetIndent(indent);
                    }
                }

                cellSelection = worksheet.Cells[rowStartIndex, columnStartIndex, rowEndIndex, columnEndIndex];

                SetCellProperties(cellInfo, cellSelection);
            }

            for (int i = 0; i < columnCount; i++)
            {
                var columnSelection = worksheet.Columns[i];
                columnSelection.AutoFitWidth();

                //NOTE: workaround for incorrect autofit.
                var newWidth = worksheet.Columns[i].GetWidth().Value.Value + 15;
                columnSelection.SetWidth(new ColumnWidth(newWidth, false));
            }

            workbook.ResumeLayoutUpdate();
            return workbook;
        }

        private RadHorizontalAlignment GetHorizontalAlignment(TextAlignment textAlignment)
        {
            switch (textAlignment)
            {
                case TextAlignment.Center:
                    return RadHorizontalAlignment.Center;

                case TextAlignment.Left:
                    return RadHorizontalAlignment.Left;

                case TextAlignment.Right:
                    return RadHorizontalAlignment.Right;

                case TextAlignment.Justify:
                default:
                    return RadHorizontalAlignment.Justify;
            }
        }

        private static void SetCellProperties(PivotExportCellInfo cellInfo, CellSelection cellSelection)
        {
            var fill = GenerateFill(cellInfo.Background);
            if (fill != null)
            {
                cellSelection.SetFill(fill);
            }

            SolidColorBrush solidBrush = cellInfo.Foreground as SolidColorBrush;
            if (solidBrush != null)
            {
                cellSelection.SetForeColor(new ThemableColor(solidBrush.Color));
            }

            if (cellInfo.FontWeight.HasValue && cellInfo.FontWeight.Value != FontWeights.Normal)
            {
                cellSelection.SetIsBold(true);
            }

            SolidColorBrush solidBorderBrush = cellInfo.BorderBrush as SolidColorBrush;
            if (solidBorderBrush != null && cellInfo.BorderThickness.HasValue)
            {
                var borderThickness = cellInfo.BorderThickness.Value;
                var color = new ThemableColor(solidBorderBrush.Color);
                //var leftBorder = new CellBorder(GetBorderStyle(borderThickness.Left), color);
                //var topBorder = new CellBorder(GetBorderStyle(borderThickness.Top), color);
                var rightBorder = new CellBorder(GetBorderStyle(borderThickness.Right), color);
                var bottomBorder = new CellBorder(GetBorderStyle(borderThickness.Bottom), color);
                var insideBorder = cellInfo.Background != null ? new CellBorder(CellBorderStyle.None, color) : null;
                cellSelection.SetBorders(new CellBorders(null, null, rightBorder, bottomBorder, insideBorder, insideBorder, null, null));
            }
        }

        private static CellBorderStyle GetBorderStyle(double thickness)
        {
            if (thickness < 1)
            {
                return CellBorderStyle.None;
            }
            else if (thickness < 2)
            {
                return CellBorderStyle.Thin;
            }
            else if (thickness < 3)
            {
                return CellBorderStyle.Medium;
            }
            else
            {
                return CellBorderStyle.Thick;
            }
        }

        private static IFill GenerateFill(Brush brush)
        {
            if (brush != null)
            {
                SolidColorBrush solidBrush = brush as SolidColorBrush;
                if (solidBrush != null)
                {
                    return PatternFill.CreateSolidFill(solidBrush.Color);
                }
            }

            return null;
        }

        #region Word, HTML and Pdf export

        private void ExportToWord()
        {
            RadDocument document = GenerateRadDocument();

            var provider = new DocxFormatProvider();
            ShowPrintPreviewDialog(document, provider);
        }

        private static void ShowPrintPreviewDialog(RadDocument document, IDocumentFormatProvider provider)
        {
            PrintPreview printPreview = new PrintPreview(document, provider);

            RadWindow window = new RadWindow();
            window.Content = printPreview;
            window.Header = "Print Preview";
            window.Height = 400;
            window.Width = 500;
#if SILVERLIGHT
            window.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
#else
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
#endif
            window.Show();
        }


        private void ExportToPdf()
        {
            RadDocument document = GenerateRadDocument();
            var provider = new PdfFormatProvider();
            ShowPrintPreviewDialog(document, provider);
        }

        private void ExportToHtml()
        {
            RadDocument document = GenerateRadDocument();
            var provider = new HtmlFormatProvider();
            ShowPrintPreviewDialog(document, provider);
        }

        private RadDocument GenerateRadDocument()
        {
            var export = pivot.GenerateExport();
            int rowCount = export.RowCount;
            int columnCount = export.ColumnCount;

            RadDocument document = new RadDocument();
            document.SectionDefaultPageMargin = new Telerik.Windows.Documents.Layout.Padding(10);
            document.LayoutMode = DocumentLayoutMode.Paged;
            document.SectionDefaultPageOrientation = PageOrientation.Landscape;
            document.Style.SpanProperties.FontFamily = pivot.FontFamily;
            document.Style.SpanProperties.FontSize = pivot.FontSize;
            document.Style.ParagraphProperties.SpacingAfter = 0;

            var section = new Telerik.Windows.Documents.Model.Section();
            document.Sections.Add(section);
            section.Blocks.Add(new Telerik.Windows.Documents.Model.Paragraph());

            var table = new Telerik.Windows.Documents.Model.Table(rowCount, columnCount);
            section.Blocks.Add(table);

            var tableRows = table.Rows.ToArray();
            foreach (var cellInfo in export.Cells)
            {
                int rowStartIndex = cellInfo.Row;
                int rowEndIndex = rowStartIndex + cellInfo.RowSpan - 1;
                int columnStartIndex = cellInfo.Column;
                int columnEndIndex = columnStartIndex + cellInfo.ColumnSpan - 1;

                var value = cellInfo.Value;
                var text = Convert.ToString(value);
                if (!string.IsNullOrWhiteSpace(text))
                {
                    var cells = tableRows[rowStartIndex].Cells.ToArray();
                    var cell = cells[columnStartIndex];
                    Paragraph paragraph = new Paragraph();
                    cell.Blocks.Add(paragraph);
                    var span = new Span(text);
                    paragraph.Inlines.Add(span);
                    paragraph.TextAlignment = GetTextAlignment(cellInfo.TextAlignment);

                    if (cellInfo.FontWeight.HasValue)
                    {
                        span.FontWeight = cellInfo.FontWeight.Value;
                    }

                    Color foreColor;
                    if (GetColor(cellInfo.Foreground, out foreColor))
                    {
                        span.ForeColor = foreColor;
                    }

                    cell.VerticalAlignment = GetVerticalAlignment(cellInfo.VerticalAlignment);
                    paragraph.LeftIndent = cellInfo.Indent * 20;
                }

                var borderThickness = cellInfo.BorderThickness;
                var borderBrush = cellInfo.BorderBrush;
                var background = cellInfo.Background;

                Color backColor;
                bool hasBackground = GetColor(cellInfo.Background, out backColor);

                if (cellInfo.RowSpan > 1 && cellInfo.ColumnSpan > 1)
                {
                    for (int k = rowStartIndex; k <= rowEndIndex; k++)
                    {
                        var cells = tableRows[k].Cells.ToArray();
                        for (int j = columnStartIndex; j <= columnEndIndex; j++)
                        {
                            var cell = cells[j];
                            if (hasBackground)
                            {
                                cell.Background = backColor;
                            }

                            cell.Borders = GetCellBorders(borderThickness, borderBrush, cell.Borders, k, rowStartIndex, rowEndIndex, j, columnStartIndex, columnEndIndex, hasBackground);
                        }

                    }
                }
                else if (cellInfo.RowSpan > 1)
                {
                    for (int j = rowStartIndex; j <= rowEndIndex; j++)
                    {
                        // TODO: check when ColumnSpan > 1;
                        var cell = tableRows[j].Cells.ToArray()[columnStartIndex];

                        Position position = j == rowStartIndex ? Position.First : ((j == rowEndIndex) ? Position.Last : Position.Middle);

                        cell.Borders = GetCellBorders(borderThickness, borderBrush, position, cell.Borders, true, cellInfo.Background != null);
                        if (hasBackground)
                        {
                            cell.Background = backColor;
                        }
                    }
                }
                else if (cellInfo.ColumnSpan > 1)
                {
                    var cells = tableRows[rowStartIndex].Cells.ToArray();
                    for (int j = columnStartIndex; j <= columnEndIndex; j++)
                    {
                        // TODO: check when RowSpan > 1;
                        var cell = cells[j];

                        Position position = j == columnStartIndex ? Position.First : ((j == columnEndIndex) ? Position.Last : Position.Middle);
                        if (hasBackground)
                        {
                            cell.Background = backColor;
                        }

                        cell.Borders = GetCellBorders(borderThickness, borderBrush, position, cell.Borders, false, hasBackground);
                    }
                }
            }

            return document;
        }

        private enum Position
        {
            First,
            Middle,
            Last
        }

        private bool GetColor(Brush brush, out Color color)
        {
            SolidColorBrush solidBrush = brush as SolidColorBrush;
            if (solidBrush != null)
            {
                color = solidBrush.Color;
                return true;
            }

            color = Colors.White;
            return false;
        }

        private TableCellBorders GetCellBorders(Thickness? borderThickness, Brush borderBrush, TableCellBorders cellBorders,
            int rowIndex, int rowStartIndex, int rowEndIndex, int columnIndex, int columnStartIndex, int columnEndIndex, bool hasBackground)
        {
            Color borderBrushColor;
            GetColor(borderBrush, out borderBrushColor);

            if (!borderThickness.HasValue)
            {
                return new TableCellBorders(new Telerik.Windows.Documents.Model.Border(BorderStyle.None));
            }

            var thickness = borderThickness.Value;
            Telerik.Windows.Documents.Model.Border topBorder = cellBorders.Top;
            Telerik.Windows.Documents.Model.Border bottomBorder = cellBorders.Bottom;
            Telerik.Windows.Documents.Model.Border leftBorder = cellBorders.Left;
            Telerik.Windows.Documents.Model.Border rightBorder = cellBorders.Right;

            if (rowIndex == rowStartIndex)
            {
                topBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Top, BorderStyle.Single, borderBrushColor);
            }

            if (rowIndex == rowEndIndex)
            {
                bottomBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Bottom, BorderStyle.Single, borderBrushColor);
            }

            if (rowStartIndex < rowIndex && rowIndex < rowEndIndex)
            {
                topBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Top;
                bottomBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Bottom;
            }

            if (columnIndex == columnStartIndex)
            {
                leftBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Left, BorderStyle.Single, borderBrushColor);
            }

            if (columnIndex == columnEndIndex)
            {
                rightBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Right, BorderStyle.Single, borderBrushColor);
            }

            if (columnStartIndex < columnIndex && columnIndex < columnEndIndex)
            {
                leftBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Left;
                rightBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Right;
            }

            return new TableCellBorders(leftBorder, topBorder, rightBorder, bottomBorder);
        }

        private TableCellBorders GetCellBorders(Thickness? borderThickness, Brush borderBrush, Position position, TableCellBorders cellBorders, bool isRow, bool hasBackground)
        {
            Color borderBrushColor;
            GetColor(borderBrush, out borderBrushColor);

            if (!borderThickness.HasValue)
            {
                return new TableCellBorders(new Telerik.Windows.Documents.Model.Border(BorderStyle.None));
            }

            var thickness = borderThickness.Value;
            if (isRow)
            {
                var leftBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Left, BorderStyle.Single, borderBrushColor);
                var rightBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Right, BorderStyle.Single, borderBrushColor);
                Telerik.Windows.Documents.Model.Border topBorder;
                Telerik.Windows.Documents.Model.Border bottomBorder;
                switch (position)
                {
                    case Position.First:
                        topBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Top, BorderStyle.Single, borderBrushColor);
                        bottomBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Bottom;
                        break;

                    case Position.Middle:
                        topBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Top;
                        bottomBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Bottom;
                        break;

                    case Position.Last:
                    default:
                        topBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Top;
                        bottomBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Bottom, BorderStyle.Single, borderBrushColor);
                        break;
                }

                return new TableCellBorders(leftBorder, topBorder, rightBorder, bottomBorder);
            }
            else
            {
                var topBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Top, BorderStyle.Single, borderBrushColor);
                var bottomBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Bottom, BorderStyle.Single, borderBrushColor);
                Telerik.Windows.Documents.Model.Border leftBorder;
                Telerik.Windows.Documents.Model.Border rightBorder;
                switch (position)
                {
                    case Position.First:
                        leftBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Left, BorderStyle.Single, borderBrushColor);
                        rightBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Right;
                        break;

                    case Position.Middle:
                        leftBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Left;
                        rightBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Right;
                        break;

                    case Position.Last:
                    default:
                        leftBorder = hasBackground ? new Telerik.Windows.Documents.Model.Border(BorderStyle.None) : cellBorders.Left; ;
                        rightBorder = new Telerik.Windows.Documents.Model.Border((float)thickness.Right, BorderStyle.Single, borderBrushColor);
                        break;
                }

                return new TableCellBorders(leftBorder, topBorder, rightBorder, bottomBorder);
            }
        }

        private Telerik.Windows.Documents.Layout.RadVerticalAlignment GetVerticalAlignment(VerticalAlignment verticalAlignment)
        {
            switch (verticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    return Telerik.Windows.Documents.Layout.RadVerticalAlignment.Bottom;

                case VerticalAlignment.Stretch:
                case VerticalAlignment.Center:
                    return Telerik.Windows.Documents.Layout.RadVerticalAlignment.Center;

                case VerticalAlignment.Top:
                default:
                    return Telerik.Windows.Documents.Layout.RadVerticalAlignment.Top;
            }
        }

        private Telerik.Windows.Documents.Layout.RadTextAlignment GetTextAlignment(TextAlignment textAlignment)
        {
            switch (textAlignment)
            {
                case TextAlignment.Center:
                    return Telerik.Windows.Documents.Layout.RadTextAlignment.Center;

                case TextAlignment.Justify:
                    return Telerik.Windows.Documents.Layout.RadTextAlignment.Justify;

                case TextAlignment.Right:
                    return Telerik.Windows.Documents.Layout.RadTextAlignment.Right;

                case TextAlignment.Left:
                default:
                    return Telerik.Windows.Documents.Layout.RadTextAlignment.Left;
            }
        }
        #endregion
    

    }
}
