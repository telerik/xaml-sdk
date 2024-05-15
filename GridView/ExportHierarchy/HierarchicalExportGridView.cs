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
        private List<ParentExportInfo> parentItemsDictionary = new List<ParentExportInfo>();
        private int headerRowCount;

        public HierarchicalExportGridView()
        {
            this.ElementExportingToDocument += HierarchicalExportGridView_ElementExportingToDocument;
            this.ElementExportedToDocument += HierarchicalExportGridView_ElementExportedToDocument;
        }

        private void HierarchicalExportGridView_ElementExportingToDocument(object sender, GridViewElementExportingToDocumentEventArgs e)
        {
            if (e.Element == ExportElement.HeaderRow)
            {
                (e.VisualParameters as GridViewDocumentVisualExportParameters).Style = new CellSelectionStyle()
                {
                    IsBold = true,
                    Fill = new PatternFill(PatternType.Solid, Color.FromArgb(255, 232, 232, 232), Colors.Transparent),
                };
            }
            else if (e.Element == ExportElement.Row)
            {
                (e.VisualParameters as GridViewDocumentVisualExportParameters).Style = new CellSelectionStyle()
                {
                    Fill = new PatternFill(PatternType.Solid, Color.FromArgb(255, 255, 142, 142), Colors.Transparent),
                };
            }
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
                        var originalIndex = gridView.Items.IndexOf(item);
                        var newIndex = originalIndex + subItemsCount;
                        subItemsCount += subItems.Count;
                        this.parentItemsDictionary.Add(new ParentExportInfo()
                        {
                            OriginalIndex = originalIndex,
                            ExportIndex = newIndex,
                            SubItems = subItems
                        });
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
                this.parentItemsDictionary.Clear();
            }
        }

        private Workbook GenerateWorkBook()
        {
            this.headerRowCount = this.ShowColumnHeaders ? 1 : 0;
            this.headerRowCount += this.ChildrenOfType<GridViewColumnGroupRow>().Count();
            Workbook workbook = null;
            workbook = this.ExportToWorkbook();

            var worksheet = workbook.ActiveWorksheet;
            worksheet.GroupingProperties.SummaryRowIsBelow = false;
            DataTemplate template = this.HierarchyChildTemplate;
            DependencyObject content = template.LoadContent();
            var childGrid = content as RadGridView;
            var parentItemCount = 0;

            foreach (var parentItem in parentItemsDictionary)
            {
                var rowIndex = parentItem.ExportIndex + headerRowCount + parentItemCount + 1;
                parentItemCount++;
                RowSelection selection = worksheet.Rows[rowIndex, rowIndex + parentItem.SubItems.Count];
                selection.Insert();
                for (var j = 0; j < childGrid.Columns.Count; j++)
                {
                    var column = childGrid.Columns[j] as GridViewDataColumn;
                    var header = column.Header != null ? column.Header.ToString() : column.DataMemberBinding.Path.Path;
                    var headerCell = worksheet.Cells[rowIndex, j];
                    headerCell.SetValueAsText(header);
                    var headerCellFill = new PatternFill(PatternType.Solid, Color.FromArgb(255, 150, 150, 150), Colors.Transparent);
                    headerCell.SetFill(headerCellFill);
                    headerCell.SetIsBold(true);

                    for (var i = 0; i < parentItem.SubItems.Count; i++)
                    {
                        var item = parentItem.SubItems[i];

                        var cell = worksheet.Cells[rowIndex + 1 + i, j];
                        var property = item.GetType().GetProperty(column.DataMemberBinding.Path.Path);
                        var value = property != null ? property.GetValue(item).ToString() : string.Empty;
                        cell.SetValueAsText(value);
                        var subItemCellFill = new PatternFill(PatternType.Solid, Color.FromArgb(255, 46, 204, 113), Colors.Transparent);
                        cell.SetFill(subItemCellFill);
                    }
                }

                selection.Group();
                var originalItem = this.Items[parentItem.OriginalIndex];
                var isExpanded = (bool)originalItem.GetType().GetProperty("IsExpanded").GetValue(originalItem);
                if (!isExpanded)
                {
                    selection.SetHidden(true);
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
