using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace PrintingAndExportingAdvanced
{
	class GanttPaginator : DocumentPaginator
	{
		private IList<BitmapSource> exportImages;

		public GanttPaginator(IEnumerable<BitmapSource> exportImages)
		{
			this.exportImages = exportImages.ToList();
		}

		public override DocumentPage GetPage(int pageNumber)
		{
			var bitmap = this.exportImages[pageNumber];
			var imageSize = new Size(bitmap.Width, bitmap.Height);
			var image = new Image { Source = bitmap };
			image.Measure(imageSize);
			image.Arrange(new Rect(imageSize));
			image.UpdateLayout();
			return new DocumentPage(image);
		}

		public override bool IsPageCountValid
		{
			get { return true; }
		}

		public override int PageCount
		{
			get { return exportImages.Count(); }
		}

		public override System.Windows.Size PageSize
		{
			get
			{
				return new Size(796.8, 1123.2);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override IDocumentPaginatorSource Source
		{
			get { return null; }
		}
	}
}
