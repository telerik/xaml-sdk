using System.Windows.Media;
using System.Windows.Media.Media3D;
using Telerik.Windows.Controls.ChartView;

namespace SurfaceProjection
{
    public class ProjectionColorizer : SurfaceSeries3DDataPointColorizer
    {
        public Material CustomMaterial { get; set; }
        public PointCollection TextureCoordinates { get; set; }

        public override Material GetMaterial(SurfaceSeries3DColorizerContext context)
        {
            return this.CustomMaterial;
        }

        public override PointCollection GetTextureCoordinates(SurfaceSeries3DColorizerContext context)
        {
            return this.TextureCoordinates;
        }
    }
}
