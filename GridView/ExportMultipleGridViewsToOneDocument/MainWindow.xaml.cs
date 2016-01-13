using System;
using System.IO;
using System.Linq;
using System.Windows;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        private void ExportToXlsxOnFileSystem(object sender, RoutedEventArgs e)
        {
            Workbook target = null;
            string fileName = null;

            //export the data from RadGridView
            Workbook playersDoc = this.playersGrid.ExportToWorkbook();

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Workbooks|*.xlsx";
            openDialog.Multiselect = false;

            //load XLSX document from the file system
            bool? dialogResult = openDialog.ShowDialog();
            if (dialogResult == true)
            {
                using (Stream stream = openDialog.OpenFile())
                {
                    target = new XlsxFormatProvider().Import(stream);
                    fileName = openDialog.FileName;
                }

                bool containsSameName = false;

                //check if target file contains sheet with the same name
                foreach (var worksheet in target.Worksheets)
                {
                    if (worksheet.Name == playersDoc.Sheets[0].Name)
                    {
                        containsSameName = true;
                        break;
                    }
                }

                if (containsSameName)
                {
                    //replate the content of the sheet with the exported data from RadGridView
                    target.Worksheets[playersDoc.Sheets[0].Name].CopyFrom(playersDoc.Sheets[0] as Worksheet);
                }
                else
                {
                    //create new sheet and add the exported data from RadGridView to it
                    Worksheet clonedSheet = target.Worksheets.Add();
                    clonedSheet.CopyFrom(playersDoc.Sheets[0] as Worksheet);
                    clonedSheet.Name = playersDoc.Sheets[0].Name;
                }

                //export the combined document back at the same path
                using (FileStream output = new FileStream(fileName, FileMode.Create))
                {
                    new XlsxFormatProvider().Export(target, output);
                }

                MessageBox.Show("Check updated file: " + fileName);
            }
        }

        void grid_ElementExportingToDocument(object sender, GridViewElementExportingToDocumentEventArgs e)
        {
            if (e.Element == ExportElement.HeaderCell || e.Element == ExportElement.CommonColumnHeader)
            {
                PatternFill fill = new PatternFill(PatternType.Solid, Colors.SkyBlue, Colors.Transparent);
                (e.VisualParameters as GridViewDocumentVisualExportParameters).Style = new CellSelectionStyle() { Fill = fill };
            }
        }
    }
}
