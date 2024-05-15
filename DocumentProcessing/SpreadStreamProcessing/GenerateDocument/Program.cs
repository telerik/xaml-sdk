using System;
using System.Diagnostics;
using System.IO;
using Telerik.Documents.SpreadsheetStreaming;

namespace GenerateDocuments
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Generating and saving document...");

            string filePath = "generated.xlsx";
            GenerateDocument(filePath);

            Console.ReadKey();
        }

        private static void GenerateDocument(string filePath)
        {
            using (FileStream stream = File.OpenWrite(filePath))
            {
                using (IWorkbookExporter workbook = SpreadExporter.CreateWorkbookExporter(SpreadDocumentFormat.Xlsx, stream))
                {
                    // Creating a style which would be used later in the code.
                    SpreadCellStyle style = workbook.CellStyles.Add("MyStyle");
                    style.Underline = SpreadUnderlineType.DoubleAccounting;
                    style.VerticalAlignment = SpreadVerticalAlignment.Top;

                    using (IWorksheetExporter worksheet = workbook.CreateWorksheetExporter("My sheet"))
                    {
                        // It is mandatory to export the worksheet view state before filling the worksheet with data.
                        using (IWorksheetViewExporter worksheetView = worksheet.CreateWorksheetViewExporter())
                        {
                            worksheetView.SetFirstVisibleCell(3, 0);

                            worksheetView.AddSelectionRange(9, 0, 13, 6);
                            worksheetView.SetActiveSelectionCell(11, 3);
                        }

                        // It is mandatory to export the column setting before exporting the row and cell data.
                        worksheet.SkipColumns(1);
                        using (IColumnExporter column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(80);
                        }

                        worksheet.SkipRows(3);
                        using (IRowExporter row = worksheet.CreateRowExporter())
                        {
                            row.SkipCells(3);
                            using (ICellExporter cell = row.CreateCellExporter())
                            {
                                cell.SetValue("Merged cell.");
                                cell.SetFormat(new SpreadCellFormat()
                                {
                                    CellStyle = style,
                                    HorizontalAlignment = SpreadHorizontalAlignment.Center,
                                    VerticalAlignment = SpreadVerticalAlignment.Center
                                });
                            }
                        }

                        using (IRowExporter row = worksheet.CreateRowExporter())
                        {
                            row.SetHeightInPixels(200);
                            using (ICellExporter cell = row.CreateCellExporter())
                            {
                                cell.SetValue(123.456);
                            }

                            using (ICellExporter cell = row.CreateCellExporter())
                            {
                                SpreadCellFormat format = new SpreadCellFormat()
                                {
                                    NumberFormat = "dd/mm/yyyy",
                                    IsBold = true
                                };
                                cell.SetFormat(format);
                                cell.SetValue(42370);
                            }
                        }

                        worksheet.MergeCells(3, 3, 6, 6);
                    }
                }
            }

            Console.WriteLine("Document generated.");

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = filePath,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
