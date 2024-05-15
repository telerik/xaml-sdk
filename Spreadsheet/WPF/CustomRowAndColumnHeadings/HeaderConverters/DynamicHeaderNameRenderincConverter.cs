using System;
using System.Linq;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Utilities;

namespace CustomRowAndColumnHeadings.HeaderConverters
{
    public class DynamicHeaderNameRenderincConverter : HeaderNameRenderingConverterBase
    {
        public CellRange TableCellRange { get; set; }

        protected override string ConvertColumnIndexToNameOverride(HeaderNameRenderingConverterContext context, int columnIndex)
        {
            int firstVisibleRowIndex = context.VisibleRange.FromIndex.RowIndex;
            int firstTableRowIndex = this.TableCellRange.FromIndex.RowIndex;
            int lastTableRowIndex = this.TableCellRange.ToIndex.RowIndex;

            string result = base.ConvertColumnIndexToNameOverride(context, columnIndex);

            if ((firstVisibleRowIndex > firstTableRowIndex && firstVisibleRowIndex <= lastTableRowIndex) &&
                this.TableCellRange.Contains(firstVisibleRowIndex, columnIndex))
            {
                CellSelection selection = context.Worksheet.Cells[firstTableRowIndex, columnIndex];
                ICellValue cellValue = selection.GetValue().Value;
                CellValueFormat cellFormat = selection.GetFormat().Value;

                result = cellValue.GetResultValueAsString(cellFormat);
            }

            return result;
        }

        protected override string ConvertRowIndexToNameOverride(HeaderNameRenderingConverterContext context, int rowIndex)
        {
            int firstVisibleColumnIndex = context.VisibleRange.FromIndex.ColumnIndex;
            int firstTableColumnIndex = this.TableCellRange.FromIndex.ColumnIndex;
            int lastTableColumnIndex = this.TableCellRange.ToIndex.ColumnIndex;

            string result = base.ConvertRowIndexToNameOverride(context, rowIndex);

            if ((firstVisibleColumnIndex > firstTableColumnIndex && firstVisibleColumnIndex <= lastTableColumnIndex) &&
                this.TableCellRange.Contains(rowIndex, firstVisibleColumnIndex))
            {
                CellSelection selection = context.Worksheet.Cells[rowIndex, firstTableColumnIndex];
                ICellValue cellValue = selection.GetValue().Value;
                CellValueFormat cellFormat = selection.GetFormat().Value;

                result = cellValue.GetResultValueAsString(cellFormat);
            }

            return result;
        }
    }
}
