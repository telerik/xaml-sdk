using System.Windows.Media;
using Telerik.Windows.Controls.TreeMap;

namespace Mappings
{
	public class FileCustomMapping : CustomMapping
	{
		protected override void Apply(RadTreeMapItem treemapItem, object dataItem)
		{
			File file = dataItem as File;
			if (dataItem is File)
			{
				treemapItem.Background = new SolidColorBrush(Colors.Red);
			}
			else
			{
				treemapItem.Background = new SolidColorBrush(Colors.Blue);
			}
		}

		protected override void Clear(RadTreeMapItem treemapItem, object dataItem)
		{
			treemapItem.ClearValue(RadTreeMapItem.BackgroundProperty);
		}
	}
}
