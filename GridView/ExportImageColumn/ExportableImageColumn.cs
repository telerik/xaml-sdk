using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;

namespace ExportImageColumn
{
	public class ExportableImageColumn : GridViewImageColumn
	{
		protected override object GetCellContent(object item)
		{
			return ConvertUriStringToImage(item);
		}

		public static object ConvertUriStringToImage(object uriString)
		{
			var image = new Image();
			image.Source = new BitmapImage(new Uri(uriString.ToString(), UriKind.Relative));
			return image;
		}
	}
}
