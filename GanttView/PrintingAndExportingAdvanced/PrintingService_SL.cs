using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PrintingAndExportingAdvanced
{
	public class PrintingService
	{
		public static void Print(IEnumerable<BitmapSource> exportImages)
		{
			var enumerator = exportImages.GetEnumerator();
			enumerator.MoveNext();
			var pd = new System.Windows.Printing.PrintDocument();
			pd.PrintPage += (s, e) =>
				{					
					e.PageVisual = PrintPage(enumerator.Current);
					enumerator.MoveNext();
					e.HasMorePages = enumerator.Current != null;
				};
			pd.Print("Gantt");
		}

		private static UIElement PrintPage(BitmapSource bitmap)
        {
            var bitmapSize = new System.Windows.Size(bitmap.PixelWidth, bitmap.PixelHeight);
            var image = new System.Windows.Controls.Image { Source = bitmap };
            image.Measure(bitmapSize);
            image.Arrange(new System.Windows.Rect(new System.Windows.Point(0, 0), bitmapSize));
			return image;
        }
	}
}
