using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Spreadsheet.Controls;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace PrintPreviewWithSpreadsheet
{
    public static class PrintAndExportExtensions
    {
        private static RadSpreadsheet spreadsheet;
        public static void PrintPreview(this RadGridView grid)
        {
            spreadsheet = CreateSpreadsheet(grid);
            var printPreviewControl = CreatePrintPreviewControl(spreadsheet);
            var window = CreatePreviewWindow(spreadsheet, printPreviewControl);

            window.ShowDialog();
        }

        private static RadWindow CreatePreviewWindow(FrameworkElement spreadsheet, FrameworkElement previewControl)
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0) });
            grid.RowDefinitions.Add(new RowDefinition());
            grid.Children.Add(spreadsheet);
            grid.Children.Add(previewControl);

            Grid.SetRow(previewControl, 1);

            return new RadWindow()
            {
                Content = grid,
                Width = 900,
                Height = 600,
                Header = "Print Preview",
#if wpf
                WindowStartupLocation = WindowStartupLocation.CenterScreen
#endif
            };
        }

        public static PrintPreviewControl CreatePrintPreviewControl(RadSpreadsheet spreadsheet)
        {
            return new PrintPreviewControl()
            {
                RadSpreadsheet = spreadsheet
            };
        }

        private static RadSpreadsheet CreateSpreadsheet(RadGridView grid)
        {
            return new RadSpreadsheet()
            {
                Workbook = CreateWorkBook(grid)
            };
        }

        private static Workbook CreateWorkBook(RadGridView grid)
        {
            Workbook book = null;

            using (var stream = new MemoryStream())
            {
                if (grid != null)
                {
                    grid.ExportToXlsx(stream, new GridViewDocumentExportOptions()
                    {
                        ShowColumnFooters = grid.ShowColumnFooters,
                        ShowColumnHeaders = grid.ShowColumnHeaders,
                        ShowGroupFooters = grid.ShowGroupFooters,
                        ExportDefaultStyles = true
                    });
                }

                stream.Position = 0;

                book = new XlsxFormatProvider().Import(stream);
            }

            return book;
        }
    }
}
