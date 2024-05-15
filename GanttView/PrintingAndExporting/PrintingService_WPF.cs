using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;

namespace PrintingAndExporting
{
	class PrintingService
	{
		public static void Print(RadGanttView ganttView)
		{
			var printDialog = new PrintDialog();
			if (printDialog.ShowDialog() == true)
			{
                var exportImages = Enumerable.Empty<ImageInfo>();
				var printingSettings = new ImageExportSettings(
					new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight), 
					true, GanttArea.AllAreas);
				using (var export = ganttView.ExportingService.BeginExporting(printingSettings))
				{
					exportImages = export.ImageInfos;
				}

				var paginator = new GanttPaginator(exportImages);
				printDialog.PrintDocument(paginator, "Print demo");
			}
		}
	}
}
