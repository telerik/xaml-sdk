using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace ExportHierarchy
{
    public class HierarchicalExportGridView : RadGridView
    {
        private int subItemsCount = 0;
        private Dictionary<int, IList> subItemsDictionary = new Dictionary<int, IList>();
        private int headerRowCount;

        public HierarchicalExportGridView()
        {
            this.ElementExportedToDocument += HierarchicalExportGridView_ElementExportedToDocument;
        }

        private void HierarchicalExportGridView_ElementExportedToDocument(object sender, GridViewElementExportedToDocumentEventArgs e)
        {
            if (e.Element == ExportElement.Row && e.DataContext != null)
            {
                var gridView = sender as RadGridView;
                var item = e.DataContext;
                var relation = gridView.ChildTableDefinitions.First().Relation as PropertyRelation;
                if (relation != null)
                {
                    var property = relation.ParentPropertyName;
                    var subItems = item.GetType().GetProperty(property).GetValue(item) as IList;
                    if (subItems.Count > 0)
                    {
                        var index = gridView.Items.IndexOf(item) + 1 + subItemsCount;
                        subItemsCount += subItems.Count;
                        this.subItemsDictionary.Add(index, subItems);
                    }
                }
            }
        }

        public void Export(string format)
        {
            var workbook = this.GenerateWorkBook();
            var extension = format;

            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = extension,
                FileName = "File",
                Filter = extension + "files (*." + extension + ")|*." + extension + "|All files (*.*)|*.*",
                FilterIndex = 1
            };

            if (dialog.ShowDialog() == true)
            {
                BinaryWorkbookFormatProviderBase provider;
                if (format == "xlsx")
                {
                    provider = new XlsxFormatProvider();
                }
                else
                {
                    provider = new PdfFormatProvider();
                }

                using (Stream output = dialog.OpenFile())
                {
                    provider.Export(workbook, output);
                }

                this.subItemsCount = 0;
                this.subItemsDictionary.Clear();
            }
        }

        private Workbook GenerateWorkBook()
        {
            this.headerRowCount = this.ShowColumnHeaders ? 1 : 0;
            this.headerRowCount += this.ChildrenOfType<GridViewColumnGroupRow>().Count();
            Workbook workbook = null;
            workbook = this.ExportToWorkbook();

            var worksheet = workbook.ActiveWorksheet;
            DataTemplate dt = this.HierarchyChildTemplate;
            DependencyObject dio = dt.LoadContent();
            var childGrid = dio as RadGridView;

            foreach (var subItem in subItemsDictionary)
            {
                var rowIndex = subItem.Key;
                RowSelection selection = worksheet.Rows[rowIndex + 1, rowIndex + subItem.Value.Count];
                selection.Insert();
                for (var i = 0; i < subItem.Value.Count; i++)
                {
                    var item = subItem.Value[i];
                    for (var j = 0; j < childGrid.Columns.Count; j++)
                    {
                        var column = childGrid.Columns[j] as GridViewDataColumn;
                        var cell = worksheet.Cells[rowIndex + 1 + i, j];
                        var property = item.GetType().GetProperty(column.DataMemberBinding.Path.Path);
                        var value = property != null ? property.GetValue(item).ToString() : string.Empty;
                        cell.SetValueAsText(value);
                        var solidPatternFill = new PatternFill(PatternType.Solid, Color.FromArgb(255, 46, 204, 113), Colors.Transparent);
                        cell.SetFill(solidPatternFill);
                    }
                }
            }

            for (var j = 0; j < childGrid.Columns.Count; j++)
            {
                worksheet.Columns[j].AutoFitWidth();
            }

            return workbook;
        }
    }
}
