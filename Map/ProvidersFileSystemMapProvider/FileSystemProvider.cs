using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.Map;

namespace FileSystemMapProvider
{
	/// <summary>
	/// Map provider which reads map tiles from the file system.
	/// </summary>
	public class FileSystemProvider : TiledProvider
	{

		/// <summary>
		/// Initializes a new instance of the MyMapProvider class.
		/// </summary>
		/// <param name="tilePathFormat">Format string to access tiles in file system.</param>
		public FileSystemProvider(string tilePathFormat, bool isGrayScale, bool isInverted, bool isCustomColor)
			: base()
		{
            FileSystemTileSource source = new FileSystemTileSource(tilePathFormat) { IsGrayScale = isGrayScale, IsInverted = isInverted, IsCustomColor = isCustomColor};
			this.MapSources.Add(source.UniqueId, source);
		}

		/// <summary>
		/// Returns the SpatialReference for the map provider.
		/// </summary>
		public override ISpatialReference SpatialReference
		{
			get
			{
				return new MercatorProjection();
			}
		}
	}
}
