using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;

namespace ImageColumnFiltering
{
	public class ImageFilterColumn : GridViewImageColumn
	{
		/// <summary>
		/// Gets the filtering display function.
		/// </summary>
		/// <value>The filtering display function.</value>
		/// <remarks>This function is used by the filtering control distinct values list.
		/// It accepts a raw data value and returns what will become the content of the
		/// distinct value checkbox.</remarks>
		protected override Func<object, object> FilteringDisplayFunc
		{
			get { return ImageFilterColumn.ConvertUriStringToImage; }
		}

		public static object ConvertUriStringToImage(object uriString)
		{
			var image = new Image();
			image.Source = new BitmapImage(new Uri(uriString.ToString(), UriKind.Relative));
			return image;
		}
	}
}
