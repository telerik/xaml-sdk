using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders.Pdf;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Media.Imaging;

namespace ExportToPDF
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = new List<List<ChartData>>()
			{
				new List<ChartData>()
				{
					new ChartData { XCat = 1, YVal = 24 },
					new ChartData { XCat = 2, YVal = 9 },
					new ChartData { XCat = 3, YVal = 18 },
					new ChartData { XCat = 4, YVal = 31 },
					new ChartData { XCat = 5, YVal = 25 },
					new ChartData { XCat = 6, YVal = 13 },
					new ChartData { XCat = 7, YVal = 17 },
					new ChartData { XCat = 8, YVal = 33 },
					new ChartData { XCat = 9, YVal = 21 },
					new ChartData { XCat = 10, YVal = 27 },
				},
				new List<ChartData>()
				{
					new ChartData { XCat = 1, YVal = 4 },
					new ChartData { XCat = 2, YVal = 19 },
					new ChartData { XCat = 3, YVal = 28 },
					new ChartData { XCat = 4, YVal = 11 },
					new ChartData { XCat = 5, YVal = 15 },
					new ChartData { XCat = 6, YVal = 31 },
					new ChartData { XCat = 7, YVal = 27 },
					new ChartData { XCat = 8, YVal = 14 },
					new ChartData { XCat = 9, YVal = 19 },
					new ChartData { XCat = 10, YVal = 21 },
				}
			};
		}

		private void Export_Pdf_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = "*.pdf";
			dialog.Filter = "Adobe PDF Document (*.pdf)|*.pdf";

			if (dialog.ShowDialog() == true)
			{
				RadDocument document = this.CreateDocument();
				document.LayoutMode = DocumentLayoutMode.Paged;
				document.Measure(RadDocument.MAX_DOCUMENT_SIZE);
				document.Arrange(new RectangleF(PointF.Empty, document.DesiredSize));

				PdfFormatProvider provider = new PdfFormatProvider();

				using (Stream output = dialog.OpenFile())
				{
					provider.Export(document, output);
				}
			}
		}

		private RadDocument CreateDocument()
		{
			RadDocument document = new RadDocument();
			Section section = new Section();
			Paragraph paragraph = new Paragraph();

			MemoryStream ms = new MemoryStream();
			radChart.ExportToImage(ms, new PngBitmapEncoder());

			double imageWidth = radChart.ActualWidth;
			double imageHeight = radChart.ActualHeight;

			if (imageWidth > 625)
			{
				imageWidth = 625;
				imageHeight = radChart.ActualHeight * imageWidth / radChart.ActualWidth;
			}

			ImageInline image = new ImageInline(ms, new Size(imageWidth, imageHeight), "png");

			paragraph.Inlines.Add(image);
			section.Blocks.Add(paragraph);
			document.Sections.Add(section);

			ms.Close();

			return document;
		}
	}
}
