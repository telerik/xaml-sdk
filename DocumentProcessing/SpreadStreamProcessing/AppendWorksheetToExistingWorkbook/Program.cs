using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Telerik.Documents.SpreadsheetStreaming;

namespace AppendWorksheetToExistingWorkbook
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Enter path to an existing .xlsx file: ");

            string fileName = Console.ReadLine();
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Such file does not exist. Press any key to exit.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Adding new worksheet to the existing workbook...");

            AddWorksheetToExistingDocument(fileName);

            Console.ReadKey();
        }

        private static void AddWorksheetToExistingDocument(string filePath)
        {
            using (FileStream stream = File.Open(filePath, FileMode.Open))
            {
                // Pass SpreadExportMode.Append parameter, and the created workbook exporter will preserve all of the existing worksheets.
                using (IWorkbookExporter workbook = SpreadExporter.CreateWorkbookExporter(SpreadDocumentFormat.Xlsx, stream, SpreadExportMode.Append))
                {
                    string sheetName = "Sheet name here";

                    IEnumerable<string> importedSheetsNames = workbook.GetSheetInfos().Select(sheetInfo => sheetInfo.Name);
                    if (importedSheetsNames.Contains(sheetName))
                    {
                        Console.WriteLine("Sheet with that name already exists in the workbook.");
                        return;
                    }

                    using (IWorksheetExporter worksheet = workbook.CreateWorksheetExporter(sheetName))
                    {
                        using (IRowExporter row = worksheet.CreateRowExporter())
                        {
                            using (ICellExporter cell = row.CreateCellExporter())
                            {
                                cell.SetValue("value 1");
                            }
                            using (ICellExporter cell = row.CreateCellExporter())
                            {
                                cell.SetValue("value 2");
                            }
                            using (ICellExporter cell = row.CreateCellExporter())
                            {
                                cell.SetValue("value 3");
                            }
                        }
                        using (IRowExporter row = worksheet.CreateRowExporter())
                        {
                            using (ICellExporter cell = row.CreateCellExporter())
                            {
                                cell.SetValue("value 4");
                            }
                            using (ICellExporter cell = row.CreateCellExporter())
                            {
                                cell.SetValue("value 5");
                            }
                            using (ICellExporter cell = row.CreateCellExporter())
                            {
                                cell.SetValue("value 6");

                            }
                        }
                    }
                }
            }

            Console.WriteLine("Document modified.");

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = filePath,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
