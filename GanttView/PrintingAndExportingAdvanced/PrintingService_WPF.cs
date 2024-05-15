using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PrintingAndExportingAdvanced
{
	public class PrintingService
	{
		public static void Print(IEnumerable<BitmapSource> exportImages)
		{
			var printDialog = new PrintDialog();
			if (printDialog.ShowDialog() == true)
			{
				var paginator = new GanttPaginator(exportImages);
				printDialog.PrintDocument(paginator, "Print demo");
			}
		}
	}
}
