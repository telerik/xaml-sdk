using System.Collections.Generic;
using System.Windows.Media.Media3D;
using Telerik.Windows.Controls.ChartView;

namespace DefaultVisualGeometrySelector
{
    public class CustomGeometry3DSelector : Geometry3DSelector
    {
        private List<Geometry3D> geometries;
        private int index;

        public CustomGeometry3DSelector()
        {
            this.geometries = new List<Geometry3D>();
        }

        public List<Geometry3D> Geometries
        {
            get
            {
                return this.geometries;
            }
        }

        public override Geometry3D SelectGeometry(object context)
        {
            if (this.geometries.Count == 0)
            {
                return null;
            }

            return this.geometries[this.index++ % this.geometries.Count];
        }
    }
}
