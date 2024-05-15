using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using GenerateDocuments.SampleData;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Utilities;

namespace GenerateDocuments
{
    public class ExampleViewModel : ViewModelBase
    {
        private static readonly int IndexColumnQuantity = 5;
        private static readonly int IndexColumnUnitPrice = 6;
        private static readonly int IndexColumnSubTotal = 7;
        private static readonly int IndexRowItemStart = 1;

        private static readonly string AccountFormatString = GenerateCultureDependentFormatString();
        private static readonly ThemableColor InvoiceBackground = new ThemableColor(Color.FromArgb(255, 44, 62, 80));
        private static readonly ThemableColor InvoiceHeaderForeground = new ThemableColor(Color.FromArgb(255, 255, 255, 255));

        private readonly Products data;

        private List<Product> products;
        private ICommand exportCommand = null;
        private string selectedExportFormat;

        public double Total
        {
            get
            {
                return this.CalculateTotal();
            }
        }

        public List<Product> Products
        {
            get { return this.products; }
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
                    OnPropertyChanged("ExportCommand");
                }
            }
        }

        public IEnumerable<string> ExportFormats
        {
            get
            {
                return FileHelper.ExportFormats;
            }
        }

        public string SelectedExportFormat
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
            this.data = new Products();
            this.GenerateData();

            this.SelectedExportFormat = this.ExportFormats.First();
            this.ExportCommand = new DelegateCommand(this.Export);
        }

        private void GenerateData()
        {
            this.Products = SampleData.Products.GetData(20).ToList();
        }

        private double CalculateTotal()
        {
            double totalAmount = 0;

            foreach (var product in this.products)
            {
                totalAmount += product.SubTotal;
            }

            return totalAmount;
        }

        private void Export(object param)
        {
            Workbook workbook = this.CreateWorkbook();

            FileHelper.SaveDocument(workbook, this.selectedExportFormat);
        }

        private Workbook CreateWorkbook()
        {
            Workbook workbook = new Workbook();
            workbook.Sheets.Add(SheetType.Worksheet);

            Worksheet worksheet = workbook.ActiveWorksheet;

            this.PrepareInvoiceDocument(worksheet, this.Products.Count);

            int currentRow = IndexRowItemStart + 1;
            foreach (Product product in this.Products)
            {
                worksheet.Cells[currentRow, 0].SetValue(product.Name);
                worksheet.Cells[currentRow, IndexColumnQuantity].SetValue(product.Quantity);
                worksheet.Cells[currentRow, IndexColumnUnitPrice].SetValue(product.UnitPrice);
                worksheet.Cells[currentRow, IndexColumnSubTotal].SetValue(product.SubTotal);

                currentRow++;
            }

            return workbook;
        }

        private void PrepareInvoiceDocument(Worksheet worksheet, int itemsCount)
        {
            int lastItemIndexRow = IndexRowItemStart + itemsCount;

            CellIndex firstUsedCellIndex = new CellIndex(0, 0);
            CellIndex lastUsedCellIndex = new CellIndex(lastItemIndexRow + 1, IndexColumnSubTotal);
            CellBorder border = new CellBorder(CellBorderStyle.DashDot, InvoiceBackground);
            worksheet.Cells[firstUsedCellIndex, lastUsedCellIndex].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));

            worksheet.Cells[firstUsedCellIndex].SetValue("INVOICE");
            worksheet.Cells[firstUsedCellIndex].SetFontSize(20);
            worksheet.Cells[firstUsedCellIndex].SetHorizontalAlignment(RadHorizontalAlignment.Center);
            worksheet.Cells[0, 0, 0, IndexColumnSubTotal].MergeAcross();

            worksheet.Columns[IndexColumnUnitPrice].SetWidth(new ColumnWidth(120, true));
            worksheet.Columns[IndexColumnSubTotal].SetWidth(new ColumnWidth(120, true));

            worksheet.Cells[IndexRowItemStart, 0, lastItemIndexRow, IndexColumnQuantity - 1].MergeAcross();
            worksheet.Cells[IndexRowItemStart, 0].SetValue("Item");
            worksheet.Cells[IndexRowItemStart, IndexColumnQuantity].SetValue("QTY");
            worksheet.Cells[IndexRowItemStart, IndexColumnUnitPrice].SetValue("Unit Price");
            worksheet.Cells[IndexRowItemStart, IndexColumnSubTotal].SetValue("SubTotal");

            worksheet.Cells[IndexRowItemStart, 0, IndexRowItemStart, IndexColumnSubTotal].SetFill
                (new GradientFill(GradientType.Horizontal, InvoiceBackground, InvoiceBackground));
            worksheet.Cells[IndexRowItemStart, 0, IndexRowItemStart, IndexColumnSubTotal].SetForeColor(InvoiceHeaderForeground);
            worksheet.Cells[IndexRowItemStart, IndexColumnUnitPrice, lastItemIndexRow, IndexColumnSubTotal].SetFormat(
                new CellValueFormat(AccountFormatString));

            worksheet.Cells[lastItemIndexRow + 1, 6].SetValue("TOTAL: ");
            worksheet.Cells[lastItemIndexRow + 1, 7].SetFormat(new CellValueFormat(AccountFormatString));

            string subTotalColumnCellRange = NameConverter.ConvertCellRangeToName(
                new CellIndex(IndexRowItemStart + 1, IndexColumnSubTotal),
                new CellIndex(lastItemIndexRow, IndexColumnSubTotal));

            worksheet.Cells[lastItemIndexRow + 1, IndexColumnSubTotal].SetValue(string.Format("=SUM({0})", subTotalColumnCellRange));

            worksheet.Cells[lastItemIndexRow + 1, IndexColumnUnitPrice, lastItemIndexRow + 1, IndexColumnSubTotal].SetFontSize(20);
        }

        private static string GenerateCultureDependentFormatString()
        {
            string gS = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            string dS = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            return "_($* #" + gS + "##0" + dS + "00_);_($* (#" + gS + "##0" + dS + "00);_($* \"-\"??_);_(@_)";
        }
    }
}
