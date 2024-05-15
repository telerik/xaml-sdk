using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using Telerik.Documents.SpreadsheetStreaming;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace FastGridExportWithSpreadStreamProcessing
{
    public class ExampleViewModel : ViewModelBase
    {
        private const int WidthOfIndentColumns = 20;

        private static readonly Color DefaultHeaderRowColor = Color.FromArgb(255, 127, 127, 127);
        private static readonly Color DefaultGroupHeaderRowColor = Color.FromArgb(255, 216, 216, 216);
        private static readonly Color DefaultDataRowColor = Color.FromArgb(255, 251, 247, 255);

        private List<Product> products;
        private ICommand exportCommand = null;
        private Color headerRowColor;
        private Color dataRowColor;
        private Color groupHeaderRowColor;
        private SpreadDocumentFormat selectedExportFormat;
        private List<CellRange> mergedCells;

        public RadGridView GridView { get; set; }

        public List<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                if (this.products != value)
                {
                    this.products = value;
                    this.OnPropertyChanged("Products");
                }
            }
        }

        public ICommand ExportCommand
        {
            get
            {
                return this.exportCommand;
            }
            set
            {
                if (this.exportCommand != value)
                {
                    this.exportCommand = value;
                    this.OnPropertyChanged("ExportCommand");
                }
            }
        }

        public Color HeaderRowColor
        {
            get
            {
                return this.headerRowColor;
            }
            set
            {
                if (this.headerRowColor != value)
                {
                    this.headerRowColor = value;
                    this.OnPropertyChanged("HeaderRowColor");
                }
            }
        }

        public Color DataRowColor
        {
            get
            {
                return this.dataRowColor;
            }
            set
            {
                if (this.dataRowColor != value)
                {
                    this.dataRowColor = value;
                    this.OnPropertyChanged("DataRowColor");
                }
            }
        }

        public Color GroupHeaderRowColor
        {
            get
            {
                return this.groupHeaderRowColor;
            }
            set
            {
                if (this.groupHeaderRowColor != value)
                {
                    this.groupHeaderRowColor = value;
                    this.OnPropertyChanged("GroupHeaderRowColor");
                }
            }
        }

        public IEnumerable<SpreadDocumentFormat> ExportFormats
        {
            get
            {
                return new SpreadDocumentFormat[] { SpreadDocumentFormat.Xlsx, SpreadDocumentFormat.Csv };
            }
        }

        public SpreadDocumentFormat SelectedExportFormat
        {
            get
            {
                return this.selectedExportFormat;
            }
            set
            {
                if (this.selectedExportFormat != value)
                {
                    this.selectedExportFormat = value;
                    this.OnPropertyChanged("SelectedExportFormat");
                }
            }
        }

        public ExampleViewModel()
        {
            this.Products = new Products().GetData(100000).ToList();

            this.SelectedExportFormat = this.ExportFormats.First();
            this.ExportCommand = new DelegateCommand(this.Export);

            this.HeaderRowColor = DefaultHeaderRowColor;
            this.DataRowColor = DefaultDataRowColor;
            this.GroupHeaderRowColor = DefaultGroupHeaderRowColor;
        }

        private void Export(object param)
        {
            this.mergedCells = new List<CellRange>();

            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = String.Format("{0} files|*.{1}|All files (*.*)|*.*", this.selectedExportFormat.ToString().ToLower(),
                this.selectedExportFormat.ToString().ToLower());

            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    DateTime start = DateTime.Now;

                    this.ExportWorkbook(this.GridView, stream);

                    MessageBox.Show(string.Format("100 000 rows exported for {0} ms", (DateTime.Now - start).TotalMilliseconds.ToString()));
                }
            }
        }

        private void ExportWorkbook(RadGridView grid, Stream stream)
        {
            IList<GridViewBoundColumnBase> columns = (from c in grid.Columns.OfType<GridViewBoundColumnBase>()
                                                      orderby c.DisplayIndex
                                                      select c).ToList();

            using (IWorkbookExporter workbook = SpreadExporter.CreateWorkbookExporter(this.selectedExportFormat, stream))
            {
                using (IWorksheetExporter worksheet = workbook.CreateWorksheetExporter("Sheet1"))
                {
                    this.SetWidthOfColumns(worksheet, grid.GroupDescriptors.Count, columns);

                    int rowIndex = 0;
                    if (grid.ShowColumnHeaders)
                    {
                        this.AddHeaderRow(worksheet, grid.GroupDescriptors.Count, columns);
                        rowIndex = 1;
                    }

                    if (grid.Items.Groups != null)
                    {
                        for (int i = 0; i < grid.Items.Groups.Count; i++)
                        {
                            QueryableCollectionViewGroup group = (QueryableCollectionViewGroup)grid.Items.Groups[i];
                            rowIndex = this.AddGroupRow(worksheet, 1, rowIndex, grid.GroupDescriptors.Count, group, columns);
                        }
                    }
                    else
                    {
                        this.AddDataRows(worksheet, 0, 0, grid.Items, columns);
                    }

                    foreach (CellRange range in this.mergedCells)
                    {
                        worksheet.MergeCells(range.FromRowIndex, range.FromColumnIndex, range.ToRowIndex, range.ToColumnIndex);
                    }
                }
            }
        }

        private void SetWidthOfColumns(IWorksheetExporter worksheet, int numberOfIndentColumns, IList<GridViewBoundColumnBase> columns)
        {
            for (int i = 0; i < numberOfIndentColumns; i++)
            {
                using (IColumnExporter column = worksheet.CreateColumnExporter())
                {
                    column.SetWidthInPixels(WidthOfIndentColumns);
                }
            }

            for (int i = 0; i < columns.Count; i++)
            {
                using (IColumnExporter column = worksheet.CreateColumnExporter())
                {
                    column.SetWidthInPixels(columns[i].Width.DisplayValue);
                }
            }
        }

        private void AddHeaderRow(IWorksheetExporter worksheet, int numberOfGroupDescriptors, IList<GridViewBoundColumnBase> columns)
        {
            int headerColumnStartIndex = numberOfGroupDescriptors;

            using (IRowExporter row = worksheet.CreateRowExporter())
            {
                SpreadCellFormat format = new SpreadCellFormat();
                format.Fill = SpreadPatternFill.CreateSolidFill(ColorToSpreadColor(this.HeaderRowColor));

                for (int i = 0; i < headerColumnStartIndex; i++)
                {
                    using (ICellExporter cell = row.CreateCellExporter())
                    {
                        cell.SetFormat(format);
                    }
                }

                for (int i = 0; i < columns.Count; i++)
                {
                    using (ICellExporter cell = row.CreateCellExporter())
                    {
                        cell.SetFormat(format);
                        cell.SetValue(columns[i].Header.ToString());
                    }
                }
            }
        }

        private static SpreadThemableColor ColorToSpreadColor(Color color)
        {
            return new SpreadThemableColor(new SpreadColor(color.R, color.G, color.B));
        }

        private void AddDataRows(IWorksheetExporter worksheet, int outlineLevel, int startColumnIndex, IList items, IList<GridViewBoundColumnBase> columns)
        {
            SpreadCellFormat format = new SpreadCellFormat();
            format.Fill = SpreadPatternFill.CreateSolidFill(ColorToSpreadColor(this.DataRowColor));

            SpreadCellFormat currencyFormat = new SpreadCellFormat();
            currencyFormat.Fill = format.Fill;
            currencyFormat.NumberFormat = "$#,##0.00";

            for (int rowIndex = 0; rowIndex < items.Count; rowIndex++)
            {
                using (IRowExporter row = worksheet.CreateRowExporter())
                {
                    row.SetOutlineLevel(outlineLevel);

                    for (int i = 0; i < startColumnIndex; i++)
                    {
                        using (ICellExporter cell = row.CreateCellExporter())
                        {
                            cell.SetFormat(format);
                        }
                    }

                    for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
                    {
                        using (ICellExporter cell = row.CreateCellExporter())
                        {
                            object value = columns[columnIndex].GetValueForItem(items[rowIndex]);

                            if (value is int)
                            {
                                cell.SetValue((int)value);
                                cell.SetFormat(format);
                            }
                            else
                            {
                                string stringValue = value.ToString();
                                if (stringValue.Contains('$'))
                                {
                                    stringValue = stringValue.Replace("$", string.Empty);
                                    double doubleValue = double.Parse(stringValue);
                                    cell.SetValue(doubleValue);
                                    cell.SetFormat(currencyFormat);
                                }
                                else
                                {
                                    cell.SetValue(stringValue);
                                    cell.SetFormat(format);
                                }
                            }
                        }
                    }
                }
            }
        }

        private int AddGroupRow(IWorksheetExporter worksheet, int outlineLevel, int rowIndex, int numberOfIndentCells,
            QueryableCollectionViewGroup group, IList<GridViewBoundColumnBase> columns)
        {
            int startColumnIndex = this.GetGroupLevel(group);
            this.mergedCells.Add(new CellRange(rowIndex, startColumnIndex, rowIndex, numberOfIndentCells + columns.Count - 1));

            SpreadCellFormat format = new SpreadCellFormat();
            format.Fill = SpreadPatternFill.CreateSolidFill(ColorToSpreadColor(this.GroupHeaderRowColor));
            format.HorizontalAlignment = SpreadHorizontalAlignment.Left;

            using (IRowExporter row = worksheet.CreateRowExporter())
            {
                row.SetOutlineLevel(outlineLevel - 1);

                row.SkipCells(startColumnIndex);

                for (int i = startColumnIndex; i < numberOfIndentCells + columns.Count - 1; i++)
                {
                    using (ICellExporter cell = row.CreateCellExporter())
                    {
                        cell.SetFormat(format);

                        if (group.Key is int)
                        {
                            cell.SetValue((int)group.Key);
                        }
                        else if (group.Key is double)
                        {
                            cell.SetValue((double)group.Key);
                        }
                        else
                        {
                            string cellValue = group.Key != null ? group.Key.ToString() : string.Empty;
                            cell.SetValue(cellValue);
                        }
                    }
                }
            }

            rowIndex++;
            startColumnIndex++;

            if (group.HasSubgroups)
            {
                foreach (IGroup subGroup in group.Subgroups)
                {
                    int newRowIndex = this.AddGroupRow(worksheet, outlineLevel + 1, rowIndex, numberOfIndentCells, subGroup as QueryableCollectionViewGroup, columns);
                    rowIndex = newRowIndex;
                }
            }
            else
            {
                this.AddDataRows(worksheet, outlineLevel, startColumnIndex, group.Items, columns);
                rowIndex += group.Items.Count;
            }

            return rowIndex;
        }

        private int GetGroupLevel(IGroup group)
        {
            int level = 0;

            IGroup parent = group.ParentGroup;

            while (parent != null)
            {
                level++;
                parent = parent.ParentGroup;
            }

            return level;
        }
    }
}
