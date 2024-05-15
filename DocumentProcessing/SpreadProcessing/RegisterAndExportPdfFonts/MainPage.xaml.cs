using RegisterAndExportPdfFonts.PdfFontsServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Spreadsheet;
using Telerik.Windows.Documents.Fixed.Model.Fonts;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Model.Protection;

namespace RegisterAndExportPdfFonts
{
    public partial class MainPage : UserControl
    {
        private SaveFileDialog saveFileDialog;
        private readonly PdfFontsServiceClient client;
        private readonly BeginEndUpdateCounter pdfExportCounter;

        public MainPage()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();

            InitializeComponent();

            this.pdfExportCounter = new BeginEndUpdateCounter(this.ExportPdf);
            this.client = new PdfFontsServiceClient();
            this.client.GetFontDataCompleted += GetFontDataCompleted;

            this.GenerateSampleWorksheet();
        }

        private Worksheet Worksheet
        {
            get
            {
                return this.radSpreadsheet.ActiveWorksheet;
            }
        }

        private void GenerateSampleWorksheet()
        {
            int currentRow = 0;

            foreach (string fontFamilyName in this.GetSpreadsheetFontFamilyNames(10))
            {
                this.Worksheet.Cells[new CellIndex(currentRow, 0)].SetValue(fontFamilyName);
                this.SetCellValue("expotación", new CellIndex(currentRow, 1), fontFamilyName, false, false);

                currentRow++;
            }

            this.Worksheet.Columns[this.Worksheet.UsedCellRange].AutoFitWidth();
            this.Worksheet.Protect("protection password", WorksheetProtectionOptions.Default);
        }

        private IEnumerable<string> GetSpreadsheetFontFamilyNames(int count)
        {
            int currentIndex = 0;

            foreach (FontFamilyInfo fontFamilyInfo in this.radSpreadsheet.FontsProvider.RegisteredFonts)
            {
                if (currentIndex++ >= count)
                {
                    break;
                }

                yield return fontFamilyInfo.FontFamily.ToString();
            }
        }

        private void SetCellValue(string value, CellIndex cellIndex, string fontFamilyName, bool isItalic, bool isBold)
        {
            CellSelection cell = this.Worksheet.Cells[cellIndex];
            cell.SetValue(value);
            cell.SetFontFamily(new ThemableFontFamily(fontFamilyName));
            cell.SetIsItalic(isItalic);
            cell.SetIsBold(isBold);
        }

        private void GetFontDataCompleted(object sender, GetFontDataCompletedEventArgs e)
        {
            this.pdfExportCounter.EndUpdate();
            System.Diagnostics.Debug.WriteLine("updated font:{0}", e.Result.FontFamilyName);

            if (e.Error == null && e.Result.IsValid)
            {
                PdfFontsServiceFontData result = e.Result;

                FontFamily fontFamily = new FontFamily(result.FontFamilyName);
                FontStyle fontStyle = GetFontStyle(result.IsItalic);
                FontWeight fontWeight = GetFontWeight(result.IsBold);

                FontsRepository.RegisterFont(fontFamily, fontStyle, fontWeight, result.Bytes);
            }
        }

        private static FontStyle GetFontStyle(bool isItalic)
        {
            return isItalic ? FontStyles.Italic : FontStyles.Normal;
        }

        private static FontWeight GetFontWeight(bool isBold)
        {
            return isBold ? FontWeights.Bold : FontWeights.Normal;
        }

        private void ExportPdfClick(object sender, RoutedEventArgs e)
        {
            this.saveFileDialog = new SaveFileDialog();
            this.saveFileDialog.Filter = "Pdf documents|*.pdf";

            if (this.saveFileDialog.ShowDialog() == true)
            {
                this.pdfExportCounter.PauseActionExecution();
                this.RegisterMissingPdfFontsBeforeExport();
                this.pdfExportCounter.ResumeActionExecution();
            }
        }

        private void ExportPdf()
        {
            using (Stream stream = this.saveFileDialog.OpenFile())
            {
                PdfFormatProvider formatProvider = new PdfFormatProvider();
                formatProvider.Export(this.radSpreadsheet.Workbook, stream);
            }
        }

        private void RegisterMissingPdfFontsBeforeExport()
        {
            CellRange usedCellRange = this.Worksheet.UsedCellRange;

            for (int row = 0; row <= usedCellRange.ToIndex.RowIndex; row++)
            {
                for (int column = 0; column <= usedCellRange.ToIndex.ColumnIndex; column++)
                {
                    CellSelection cell = this.Worksheet.Cells[new CellIndex(row, column)];
                    string fontFamilyName = cell.GetFontFamily().Value.GetActualValue(this.Worksheet.Workbook.Theme).ToString();
                    bool isItalic = cell.GetIsItalic().Value;
                    bool isBold = cell.GetIsBold().Value;

                    this.RegisterFont(fontFamilyName, isItalic, isBold);
                }
            }
        }

        private void RegisterFont(string fontFamilyName, bool isItalic, bool isBold)
        {
            FontBase font;
            if (!FontsRepository.TryCreateFont(new FontFamily(fontFamilyName), GetFontStyle(isItalic), GetFontWeight(isBold), out font))
            {
                this.pdfExportCounter.BeginUpdate();
                this.client.GetFontDataAsync(fontFamilyName, isItalic, isBold);
            }
        }

        private void RadSpreadsheetMessageShowing(object sender, MessageShowingEventArgs e)
        {
            string protectionError = LocalizationManager.GetString("Spreadsheet_ProtectedWorksheet_Error");

            if (e.Content.Equals(protectionError, StringComparison.CurrentCulture))
            {
                e.IsHandled = true;
            }
        }
    }
}
