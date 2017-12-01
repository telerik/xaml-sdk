using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Model.Shapes;

namespace ExportImageColumn
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		public Example()
		{
			InitializeComponent();
		}

		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			this.Export(this.clubsGrid, "xlsx", new XlsxFormatProvider());
		}

		private void Button2_Click(object sender, RoutedEventArgs e)
		{
			this.Export(this.clubsGrid, "pdf", new PdfFormatProvider());
		}

		private void Export(GridViewDataControl grid, string extension, BinaryWorkbookFormatProviderBase provider)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = "." + extension;

			if (dialog.ShowDialog() == true)
			{
				using (var output = dialog.OpenFile())
				{
					provider.Export(this.ExportWorkbook(this.clubsGrid), output);
				}
			}
		}

		private Workbook ExportWorkbook(GridViewDataControl grid)
		{
			var imageExtension = "png";
			var imageWidth = 20;
			var imageHeight = 23;
			var padding = 5;

			Workbook workbook = grid.ExportToWorkbook();
			Worksheet worksheet = workbook.ActiveWorksheet;
			var usedRows = worksheet.UsedCellRange.RowCount;
			var usedColumns = worksheet.UsedCellRange.ColumnCount;

			for (int i = 0; i < usedRows; i++)
			{
				for (int j = 0; j < usedColumns; j++)
				{
					var cell = worksheet.Cells[i, j];
					var cellValue = cell.GetValue().Value.RawValue;
					if (cellValue.Contains(string.Format(".{0}", imageExtension)))
					{
						FloatingImage image = new FloatingImage(worksheet, new CellIndex(i, j), padding, padding);

						Stream stream = File.Open("../../" + cellValue, FileMode.Open, FileAccess.Read);

						using (stream)
						{
							image.ImageSource = new Telerik.Windows.Documents.Media.ImageSource(stream, imageExtension);
						}

						image.Width = imageWidth;
						image.Height = imageHeight;

						worksheet.Shapes.Add(image);
						cell.SetValue(string.Empty);
						worksheet.Rows[i].SetHeight(new RowHeight(imageHeight + padding * 2, true));
					}
				}
			}

			return workbook;
		}
	}
}
