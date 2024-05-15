using System;
using System.Text;
using Telerik.Windows.Controls.Map;

namespace HowToImplementCustomMapProvider
{
    public class CustomTiledMapSource : TiledMapSource
    {
        public CustomTiledMapSource()
            : base(0, 15, 512, 512)
        {
        }

        public override void Initialize()
        {
            // Raise provider initialized event.
            this.RaiseInitializeCompleted();
        }

        protected override Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY)
        {
            int zoomLevel = ConvertTileToZoomLevel(tileLevel);

            string url = "http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer";

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}/tile/{1}/{2}/{3}", new object[] { url, zoomLevel, tilePositionY, tilePositionX });

            return new Uri(builder.ToString());
        }
    }
}
