using System;
using System.Diagnostics;
using System.IO;
#if NETCOREAPP
using Telerik.Documents.Common.Model;
using Telerik.Documents.Media;
#else
using System.Windows.Media;
using Telerik.Windows.Documents.Spreadsheet.Theming;
#endif
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Model.Filtering;
using Telerik.Windows.Documents.Spreadsheet.Model.Shapes;
using Telerik.Windows.Documents.Spreadsheet.PropertySystem;
using Telerik.Windows.Documents.Spreadsheet.Utilities;

namespace CreateModifyExport
{
    public class ReportGenerator
    {
        private Worksheet worksheet;

        public ReportGenerator()
        {
            this.CreateWorkbook();
            this.SetColumnHeaders();
            this.SetData();
            this.SetTotalsRows();
            this.SetDocumentTitle();

            this.SetWorkbookTheme();
            this.CreateWorkbookStyles();
            this.ApplyStyles();
            this.ApplyNumberFormats();
            this.ApplyColumnWidths();
            this.InsertCompanyLogo();

            this.worksheet.WorksheetPageSetup.PaperType = PaperTypes.A4;
            this.worksheet.WorksheetPageSetup.CenterHorizontally = true;

            this.ExportDirectory = AppDomain.CurrentDomain.BaseDirectory + "Reports/";
        }

        private string exportDirectory;
        public string ExportDirectory
        {
            get
            {
                return this.exportDirectory;
            }
            set
            {
                this.exportDirectory = value;
            }
        }

        public Workbook Workbook { get; private set; }

        public void ExportReports()
        {
            this.FilterByDepartment("Sales");
            this.ExportToPdf("SalesExpenses.pdf");

            this.FilterByDepartment("Marketing");
            this.ExportToPdf("MarketingExpenses.pdf");

            this.FilterByDepartment("Engineering");
            this.ExportToPdf("EngineeringExpenses.pdf");

            this.worksheet.Filter.FilterRange = null;

            Console.WriteLine("Reports generated.");

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = this.ExportDirectory,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        public void ExportReport(string departmentName, Stream stream)
        {
            this.FilterByDepartment(departmentName);
            this.ExportToPdf(stream);
            this.worksheet.Filter.FilterRange = null;
        }

        private void CreateWorkbook()
        {
            this.Workbook = new Workbook();
            this.worksheet = this.Workbook.Worksheets.Add();
            this.worksheet.Name = "Expense Report 2014";
        }

        private void SetColumnHeaders()
        {
            this.worksheet.Cells[5, 1].SetValue("Department");
            this.worksheet.Cells[5, 2].SetValue("Description");
            this.worksheet.Cells[5, 3].SetValue("Date");
            this.worksheet.Cells[5, 4].SetValue("Amount");
        }

        private void SetData()
        {
            int currentRow = 6;

            foreach (ExpenseModel model in ExpenseModel.GetExpenseData())
            {
                this.worksheet.Cells[currentRow, 1].SetValue(model.Department);
                this.worksheet.Cells[currentRow, 2].SetValue(model.Description);
                this.worksheet.Cells[currentRow, 3].SetValue(model.Date);
                this.worksheet.Cells[currentRow, 4].SetValue(model.Amount);
                currentRow++;
            }
        }

        private void SetTotalsRows()
        {
            this.worksheet.Cells[28, 1].SetValue("Sales Expenses");
            this.worksheet.Cells[29, 1].SetValue("Marketing Expenses");
            this.worksheet.Cells[30, 1].SetValue("Engineering Expenses");
            this.worksheet.Cells[31, 1].SetValue("Total Expenses");

            string listSeparator = SpreadsheetCultureInfo.ListSeparator;
            this.worksheet.Cells[28, 4].SetValue(string.Format("=SumIf(B7:B28{0}\"Sales\"{0}E7:E28)", listSeparator));
            this.worksheet.Cells[29, 4].SetValue(string.Format("=SumIf(B7:B28{0}\"Marketing\"{0}E7:E28)", listSeparator));
            this.worksheet.Cells[30, 4].SetValue(string.Format("=SumIf(B7:B28{0}\"Engineering\"{0}E7:E28)", listSeparator));
            this.worksheet.Cells[31, 4].SetValue("=Sum(E7:E28)");
        }

        private void SetDocumentTitle()
        {
            CellSelection companyNameCells = this.worksheet.Cells[1, 1, 1, 4];
            companyNameCells.SetValue("My Company");
            companyNameCells.SetHorizontalAlignment(RadHorizontalAlignment.Left);
            companyNameCells.Merge();

            CellSelection expenseReportCells = this.worksheet.Cells[2, 1, 2, 4];
            expenseReportCells.SetValue("Expense Report");
            expenseReportCells.SetHorizontalAlignment(RadHorizontalAlignment.Right);
            expenseReportCells.Merge();

            CellSelection periodCells = this.worksheet.Cells[3, 1, 3, 4];
            periodCells.SetValue("1 Jan 2014 - 31 Mar 2014");
            periodCells.SetHorizontalAlignment(RadHorizontalAlignment.Right);
            periodCells.Merge();
        }

        private void SetWorkbookTheme()
        {
            ThemeColorScheme colorScheme = new ThemeColorScheme("ExpenseReport",
                       Color.FromArgb(255, 65, 65, 65),    // Background1
                       Color.FromArgb(255, 240, 240, 240), // Text1
                       Color.FromArgb(255, 0, 0, 0),       // Background2
                       Color.FromArgb(255, 255, 255, 255), // Text2
                       Color.FromArgb(255, 116, 202, 218), // Accent1
                       Color.FromArgb(255, 146, 204, 70),  // Accent2
                       Color.FromArgb(255, 241, 96, 61),   // Accent3
                       Color.FromArgb(255, 143, 145, 158), // Accent4
                       Color.FromArgb(255, 141, 119, 251), // Accent5
                       Color.FromArgb(255, 91, 119, 153),  // Accent6
                       Color.FromArgb(255, 5, 99, 193),    // Hyperlink
                       Color.FromArgb(255, 149, 79, 114)); // Followed hyperlink

            ThemeFontScheme fontScheme = new ThemeFontScheme("ExpenseReport", "Cambria", "Segoe UI");
            DocumentTheme theme = new DocumentTheme("ExpenseReport", colorScheme, fontScheme);
            this.Workbook.Theme = theme;
        }

        private void CreateWorkbookStyles()
        {
            CellStyle normalStyle = this.Workbook.Styles["Normal"];
            normalStyle.Fill = PatternFill.CreateSolidFill(new ThemableColor(ThemeColorType.Background1));
            normalStyle.FontFamily = new ThemableFontFamily(ThemeFontType.Minor);
            normalStyle.FontSize = UnitHelper.PointToDip(10);
            normalStyle.VerticalAlignment = RadVerticalAlignment.Center;

            CellStyle companyNameStyle = this.Workbook.Styles.Add("CompanyNameStyle");
            companyNameStyle.FontFamily = new ThemableFontFamily(ThemeFontType.Major);
            companyNameStyle.FontSize = UnitHelper.PointToDip(48);
            companyNameStyle.HorizontalAlignment = RadHorizontalAlignment.Left;

            CellStyle expensePeriodStyle = this.Workbook.Styles.Add("ExpensePeriodStyle");
            expensePeriodStyle.FontFamily = new ThemableFontFamily("Segoe UI Light");
            expensePeriodStyle.FontSize = UnitHelper.PointToDip(20);
            expensePeriodStyle.HorizontalAlignment = RadHorizontalAlignment.Right;

            CellStyle columnHeadersStyle = this.Workbook.Styles.Add("ColumnHeadersStyle");
            columnHeadersStyle.FontFamily = new ThemableFontFamily(ThemeFontType.Major);
            columnHeadersStyle.BottomBorder = new CellBorder(CellBorderStyle.Thick, new ThemableColor(ThemeColorType.Accent2));
            columnHeadersStyle.FontSize = UnitHelper.PointToDip(14);

            CellStyle departmentTotalStyle = this.Workbook.Styles.Add("DepartmentTotalStyle");
            departmentTotalStyle.CopyPropertiesFrom(normalStyle);
            departmentTotalStyle.FontSize = UnitHelper.PointToDip(14);
            departmentTotalStyle.FontFamily = new ThemableFontFamily("Segoe UI Light");

            CellStyle totalStyle = this.Workbook.Styles.Add("TotalStyle");
            totalStyle.Fill = PatternFill.CreateSolidFill(new ThemableColor(ThemeColorType.Accent2));
            totalStyle.FontSize = UnitHelper.PointToDip(14);
            totalStyle.ForeColor = new ThemableColor(ThemeColorType.Background1);
        }

        private void ApplyStyles()
        {
            this.worksheet.Cells[1, 1, 1, 4].SetStyleName("CompanyNameStyle");
            this.worksheet.Cells[2, 1, 3, 4].SetStyleName("ExpensePeriodStyle");
            this.worksheet.Cells[5, 1, 5, 4].SetStyleName("ColumnHeadersStyle");
            this.worksheet.Cells[28, 1, 30, 4].SetStyleName("DepartmentTotalStyle");
            this.worksheet.Cells[31, 1, 31, 4].SetStyleName("TotalStyle");
        }

        private void ApplyNumberFormats()
        {
            string shortDateFormat = "d MMM yyyy";
            this.worksheet.Cells[6, 3, 31, 3].SetFormat(new CellValueFormat(shortDateFormat));

            string currencyFormat = "$#,##0.00";
            this.worksheet.Cells[6, 4, 31, 4].SetFormat(new CellValueFormat(currencyFormat));

            this.worksheet.Cells[5, 3, 5, 4].SetHorizontalAlignment(RadHorizontalAlignment.Right);
        }

        private void ApplyColumnWidths()
        {
            this.worksheet.Columns[0].SetWidth(new ColumnWidth(25, true));
            this.worksheet.Columns[1, 4].AutoFitWidth();
        }

        private void InsertCompanyLogo()
        {
            FloatingImage image = new FloatingImage(this.worksheet, new CellIndex(1, 4), 25, 10);

            using (Stream stream = File.OpenRead("Resources/MyCompanyLogo.jpg"))
            {
                image.ImageSource = new Telerik.Windows.Documents.Media.ImageSource(stream, "jpg");
            }

            this.worksheet.Images.Add(image);
        }

        private void FilterByDepartment(string departmentName)
        {
            this.worksheet.Filter.FilterRange = new CellRange(5, 1, 31, 4);

            string[] valuesToShow = new string[]
            {
                departmentName,
                String.Format("{0} Expenses", departmentName),
                "Total Expenses"
            };

            IFilter departmentFilter = new ValuesCollectionFilter(0, valuesToShow, true);

            this.worksheet.Filter.SetFilter(departmentFilter);
        }

        private void ExportToPdf(string fileName)
        {
            using (Stream fileStream = this.GetFileStream(fileName))
            {
                PdfFormatProvider provider = new PdfFormatProvider();
                provider.Export(this.Workbook, fileStream);
            }
        }

        private void ExportToPdf(Stream fileStream)
        {
            using (fileStream)
            {
                PdfFormatProvider provider = new PdfFormatProvider();
                provider.Export(this.Workbook, fileStream);
            }
        }

        private Stream GetFileStream(string fileName)
        {
            PrepareDirectory(this.ExportDirectory, fileName);
            string filePath = string.Format("{0}\\{1}", this.ExportDirectory, fileName);

            return new FileStream(filePath, FileMode.OpenOrCreate);
        }


        private static void PrepareDirectory(string filePath, string resultFile)
        {
            if (Directory.Exists(filePath))
            {
                if (File.Exists(resultFile))
                {
                    File.Delete(resultFile);
                }
            }
            else
            {
                Directory.CreateDirectory(filePath);
            }
        }
    }
}
