using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using CustomFunctions.Functions;
using CustomFunctions.Resources;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;

namespace CustomFunctions
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            WorkbookFormatProvidersManager.RegisterFormatProvider(new PdfFormatProvider());
            WorkbookFormatProvidersManager.RegisterFormatProvider(new XlsxFormatProvider());

            this.RegisterCustomFunctions();

            this.LoadResourceFile("Resources/CustomFunctions.xlsx");
        }

        void LoadResourceFile(string filePath)
        {
            XlsxFormatProvider formatProvider = new XlsxFormatProvider();
            using (var stream = ResourceHelper.GetResourceStream(filePath))
            {
                this.radSpreadsheet.Workbook = formatProvider.Import(stream);
            }
        }

        void RegisterCustomFunctions()
        {
            FunctionManager.RegisterFunction(new Arguments());
            FunctionManager.RegisterFunction(new GeoMean());
            FunctionManager.RegisterFunction(new E());
            FunctionManager.RegisterFunction(new Add());
            FunctionManager.RegisterFunction(new RepeatString());
            FunctionManager.RegisterFunction(new Nand());
            FunctionManager.RegisterFunction(new CustomFunctions.Functions.Upper());
            FunctionManager.RegisterFunction(new CustomFunctions.Functions.Indirect());
        }
    }
}
