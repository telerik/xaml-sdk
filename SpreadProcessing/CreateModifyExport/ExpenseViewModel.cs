using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Model.Filtering;
using Telerik.Windows.Documents.Spreadsheet.Model.Shapes;
using Telerik.Windows.Documents.Spreadsheet.PropertySystem;
using Telerik.Windows.Documents.Spreadsheet.Theming;
using Telerik.Windows.Documents.Spreadsheet.Utilities;

namespace CreateModifyExport
{
    public class ExpenseViewModel : ViewModelBase
    {
        private Worksheet worksheet;
        private Workbook workbook;

        public Workbook Workbook
        {
            get
            {
                return this.workbook;
            }
        }

        public ExpenseViewModel()
        {
            this.CreateWorkbook();
            this.SetColumnHeaders();
            this.SetData();
            this.SetTotalsRows();
            this.SetDocumentTitle();
            this.worksheet.Columns[worksheet.UsedCellRange].AutoFitWidth();

            this.SetWorkbookTheme();
            this.CreateWorkbookStyles();
            this.ApplyStyles();
            this.ApplyNumberFormats();
            this.ApplyColumnWidths();
            this.InsertCompanyLogo();

            this.worksheet.WorksheetPageSetup.PaperType = PaperTypes.A4;
            this.worksheet.WorksheetPageSetup.CenterHorizontally = true;

            // The code below cannot be executed in Silverlight because of a SecurityException.
            // Instead, the file streams should be created using SaveFileDialog in Silverlight.
#if !SILVERLIGHT
            this.FilterByDepartment("Sales");
            this.ExportToPdf("SalesExpenses.pdf");

            this.FilterByDepartment("Marketing");
            this.ExportToPdf("MarketingExpenses.pdf");

            this.FilterByDepartment("Engineering");
            this.ExportToPdf("EngineeringExpenses.pdf");

            this.worksheet.Filter.FilterRange = null;
#endif
        }

        private void CreateWorkbook()
        {
            this.workbook = new Workbook();
            this.worksheet = workbook.Worksheets.Add();
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

            string listSeparator = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator;
            this.worksheet.Cells[28, 4].SetValue(string.Format("=SumIf(B7:B28{0}\"Sales\"{0}E7:E28)", listSeparator));
            this.worksheet.Cells[29, 4].SetValue(string.Format("=SumIf(B7:B28{0}\"Marketing\"{0}E7:E28)", listSeparator));
            this.worksheet.Cells[30, 4].SetValue(string.Format("=SumIf(B7:B28{0}\"Engineering\"{0}E7:E28)", listSeparator));
            this.worksheet.Cells[31, 4].SetValue("=Sum(E7:E28)");
        }

        private void SetDocumentTitle()
        {
            CellSelection companyNameCells = worksheet.Cells[1, 1, 1, 4];
            companyNameCells.Merge();
            companyNameCells.SetValue("My Company");
            companyNameCells.SetHorizontalAlignment(RadHorizontalAlignment.Left);

            CellSelection expenseReportCells = worksheet.Cells[2, 1, 2, 4];
            expenseReportCells.Merge();
            expenseReportCells.SetValue("Expense Report");
            expenseReportCells.SetHorizontalAlignment(RadHorizontalAlignment.Right);

            CellSelection periodCells = worksheet.Cells[3, 1, 3, 4];
            periodCells.Merge();
            periodCells.SetValue("1 Jan 2014 - 31 Mar 2014");
            periodCells.SetHorizontalAlignment(RadHorizontalAlignment.Right);
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
            this.workbook.Theme = theme;
        }

        private void CreateWorkbookStyles()
        {
            CellStyle normalStyle = this.workbook.Styles["Normal"];
            normalStyle.Fill = PatternFill.CreateSolidFill(new ThemableColor(ThemeColorType.Background1));
            normalStyle.FontFamily = new ThemableFontFamily(ThemeFontType.Minor);
            normalStyle.FontSize = UnitHelper.PointToDip(10);
            normalStyle.VerticalAlignment = RadVerticalAlignment.Center;

            CellStyle companyNameStyle = this.workbook.Styles.Add("CompanyNameStyle");
            companyNameStyle.FontFamily = new ThemableFontFamily(ThemeFontType.Major);
            companyNameStyle.FontSize = UnitHelper.PointToDip(48);
            companyNameStyle.HorizontalAlignment = RadHorizontalAlignment.Left;

            CellStyle expensePeriodStyle = this.workbook.Styles.Add("ExpensePeriodStyle");
            expensePeriodStyle.FontFamily = new ThemableFontFamily("Segoe UI Light");
            expensePeriodStyle.FontSize = UnitHelper.PointToDip(20);
            expensePeriodStyle.HorizontalAlignment = RadHorizontalAlignment.Right;

            CellStyle columnHeadersStyle = this.workbook.Styles.Add("ColumnHeadersStyle");
            columnHeadersStyle.BottomBorder = new CellBorder(CellBorderStyle.Thick, new ThemableColor(ThemeColorType.Accent2));
            columnHeadersStyle.FontSize = UnitHelper.PointToDip(14);

            CellStyle departmentTotalStyle = this.workbook.Styles.Add("DepartmentTotalStyle");
            departmentTotalStyle.CopyPropertiesFrom(normalStyle);
            departmentTotalStyle.FontSize = UnitHelper.PointToDip(14);
            departmentTotalStyle.FontFamily = new ThemableFontFamily("Segoe UI Light");

            CellStyle totalStyle = this.workbook.Styles.Add("TotalStyle");
            totalStyle.Fill = PatternFill.CreateSolidFill(new ThemableColor(ThemeColorType.Accent2));
            totalStyle.FontSize = UnitHelper.PointToDip(14);
            totalStyle.ForeColor = new ThemableColor(ThemeColorType.Background1);
        }

        private void ApplyStyles()
        {
            worksheet.Cells[1, 1, 1, 4].SetStyleName("CompanyNameStyle");
            worksheet.Cells[2, 1, 3, 4].SetStyleName("ExpensePeriodStyle");
            worksheet.Cells[5, 1, 5, 4].SetStyleName("ColumnHeadersStyle");
            worksheet.Cells[28, 1, 30, 4].SetStyleName("DepartmentTotalStyle");
            worksheet.Cells[31, 1, 31, 4].SetStyleName("TotalStyle");
        }

        private void ApplyNumberFormats()
        {
            string shortDateFormat = "d MMM yyyy";
            worksheet.Cells[6, 3, 31, 3].SetFormat(new CellValueFormat(shortDateFormat));

            string currencyFormat = "$#,##0.00";
            worksheet.Cells[6, 4, 31, 4].SetFormat(new CellValueFormat(currencyFormat));

            worksheet.Cells[5, 3, 5, 4].SetHorizontalAlignment(RadHorizontalAlignment.Right);
        }

        private void ApplyColumnWidths()
        {
            worksheet.Columns[0].SetWidth(new ColumnWidth(25, true));
            worksheet.Columns[1, 4].AutoFitWidth();
        }

        private void InsertCompanyLogo()
        {
            FloatingImage image = new FloatingImage(worksheet, new CellIndex(1, 4), 25, 10);

            using (Stream stream = GetResourceStream("Resources/MyCompanyLogo.jpg"))
            {
                image.ImageSource = new Telerik.Windows.Documents.Media.ImageSource(stream, "jpg");
            }

            image.Width = 65;
            image.Height = 65;

            worksheet.Shapes.Add(image);
        }

        public static Stream GetResourceStream(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(ExpenseViewModel).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return Application.GetResourceStream(resourceUri).Stream;
        }

        public void FilterByDepartment(string departmentName)
        {
            worksheet.Filter.FilterRange = new CellRange(5, 1, 31, 4);

            string[] valuesToShow = new string[]
            {
                departmentName,
                String.Format("{0} Expenses", departmentName),
                "Total Expenses"
            };

            IFilter departmentFilter = new ValuesCollectionFilter(0, valuesToShow, true);

            worksheet.Filter.SetFilter(departmentFilter);
        }

        public void ExportToPdf(string fileName)
        {
            using (Stream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                PdfFormatProvider provider = new PdfFormatProvider();
                provider.Export(workbook, fileStream);
            }
        }
    }
}
