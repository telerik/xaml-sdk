using System;
using Telerik.Windows.Controls.Map;

namespace HowToImplementCustomMapProvider
{
    public class CustomTiledMapProvider : TiledProvider, ICloneable
    {
        public CustomTiledMapProvider()
            : base()
        {
            CustomTiledMapSource source = new CustomTiledMapSource();
            this.MapSources.Add(source.UniqueId, source);
        }

        public override ISpatialReference SpatialReference
        {
            get
            {
                return new MercatorProjection();
            }
        }

        public new object Clone()
        {
            CustomTiledMapProvider clone = new CustomTiledMapProvider();
            this.InheritCurrentSource(clone);
            this.InheritParameters(clone);

            return clone;
        }
    }
}
