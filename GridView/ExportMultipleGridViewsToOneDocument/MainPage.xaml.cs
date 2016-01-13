using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace ExportMultipleGridViewsToOneDocument
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.playersGrid.ElementExportingToDocument += grid_ElementExportingToDocument;
            this.clubsGrid.ElementExportingToDocument += grid_ElementExportingToDocument;
        }

        private void ExportPdf(object sender, RoutedEventArgs e)
        {
            //export the combined PDF
            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = "pdf",
                Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", "pdf", "Pdf"),
                FilterIndex = 1
            };

            if (dialog.ShowDialog() == true)
            {
                //export the data from the two RadGridVies instances in separate documents
                RadFixedDocument playersDoc = this.playersGrid.ExportToRadFixedDocument();
                RadFixedDocument clubsDoc = this.clubsGrid.ExportToRadFixedDocument();

                //merge the second document into the first one
                playersDoc.Merge(clubsDoc);

                using (Stream stream = dialog.OpenFile())
                {
                    new PdfFormatProvider().Export(playersDoc, stream);
                }
            }
        }

        private void ExportXlsx(object sender, RoutedEventArgs e)
        {
            //export the combined PDF
            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = "xlsx",
                Filter = String.Format("Workbooks (*.{0})|*.{0}|All files (*.*)|*.*", "xlsx"),
                FilterIndex = 1
            };

            if (dialog.ShowDialog() == true)
            {

                //export the data from the two RadGridVies instances in separate documents
                Workbook playersDoc = this.playersGrid.ExportToWorkbook();
                Workbook clubsDoc = this.clubsGrid.ExportToWorkbook();

                //merge the second document into the first one
                Worksheet clonedSheet = playersDoc.Worksheets.Add();
                clonedSheet.CopyFrom(clubsDoc.Sheets[0] as Worksheet);

                using (Stream stream = dialog.OpenFile())
                {
                    new XlsxFormatProvider().Export(playersDoc, stream);
                }
            }
        }

        void grid_ElementExportingToDocument(object sender, GridViewElementExportingToDocumentEventArgs e)
        {
            if (e.Element == ExportElement.HeaderCell || e.Element == ExportElement.CommonColumnHeader)
            {
                PatternFill fill = new PatternFill(PatternType.Solid, Colors.Blue, Colors.Transparent);
                (e.VisualParameters as GridViewDocumentVisualExportParameters).Style = new CellSelectionStyle() { Fill = fill };
            }
        }
    }
}
