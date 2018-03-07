using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CustomRowAndColumnHeadings.HeaderConverters;
using CustomRowAndColumnHeadings.Resources;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace CustomRowAndColumnHeadings_SL
{
    public partial class MainPage : UserControl
    {
        private const string ResourceFilePath = "Resources/OrdersLog.xlsx";
        private readonly CellRange DocumentTableCellRange;

        public MainPage()
        {
            InitializeComponent();

            Workbook workbook = this.LoadWorkbook();
            this.radSpreadsheet.Workbook = workbook;

            this.DocumentTableCellRange = new CellRange(2, 0, 48, 5);
        }

        private void DefaultHeaders_Click(object sender, RoutedEventArgs e)
        {
            this.ShowGridlines();
            this.radSpreadsheet.ActiveWorksheet.HeaderNameRenderingConverter = null;
        }

        private void NumberedColumnHeaders_Click(object sender, RoutedEventArgs e)
        {
            this.ShowGridlines();
            this.radSpreadsheet.ActiveWorksheet.HeaderNameRenderingConverter = new NumberedColumnsHeaderNameRenderingConverter();
        }

        private void DynamicHeaders_Click(object sender, RoutedEventArgs e)
        {
            this.ShowGridlines();
            DynamicHeaderNameRenderincConverter renderingConverter = new DynamicHeaderNameRenderincConverter();
            renderingConverter.TableCellRange = DocumentTableCellRange;
            this.radSpreadsheet.ActiveWorksheet.HeaderNameRenderingConverter = renderingConverter;
        }

        private void HideHeaders_Click(object sender, RoutedEventArgs e)
        {
            this.HideGridlines();
        }
 
        private void HideGridlines()
        {
            this.radSpreadsheet.ActiveWorksheetEditor.ShowRowColumnHeadings = false;
        }

        private void ShowGridlines()
        {
            this.radSpreadsheet.ActiveWorksheetEditor.ShowRowColumnHeadings = true;
        }

        private Workbook LoadWorkbook()
        {
            Workbook result = null;

            try
            {
                XlsxFormatProvider formatProvider = new XlsxFormatProvider();

                using (Stream stream = ResourceHelper.GetResourceStream(ResourceFilePath))
                {
                    result = formatProvider.Import(stream);
                }
            }
            catch
            {
                return null;
            }

            return result;
        }
    }
}
