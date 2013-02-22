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

            if (tilePositionY > Math.Pow(2, zoomLevel - 1) - 1)
            {
                return null;
            }

            string url = "http://services.arcgisonline.com/ArcGIS/rest/services/ESRI_Imagery_World_2D/MapServer";

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}/tile/{1}/{2}/{3}", new object[] { url, zoomLevel - 1, tilePositionY, tilePositionX });

            return new Uri(builder.ToString());
        }
    }
}
