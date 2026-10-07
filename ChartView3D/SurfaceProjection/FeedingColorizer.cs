using System;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Telerik.Windows.Controls.ChartView;

namespace SurfaceProjection
{
    public class FeedingColorizer : SurfaceSeries3DValueGradientColorizer
    {
        public PointCollection TextureCoordinates { get; set; }
        public Material Material { get; set; }

        public event EventHandler OnColorizingCompleted;

        public void RaiseColorizingCompleted()
        {
            if (OnColorizingCompleted != null)
            {
                OnColorizingCompleted(this, null);
            }
        }

        protected override void OnColorizationFinished()
        {
            RaiseColorizingCompleted();

            base.OnColorizationFinished();
        }

        public override Material GetMaterial(SurfaceSeries3DColorizerContext context)
        {
            var material = base.GetMaterial(context);
            this.Material = material;
            AssignTextureCoordinates(context);
            return material;
        }

        private void AssignTextureCoordinates(SurfaceSeries3DColorizerContext context)
        {
            this.TextureCoordinates = context.TextureCoordinates;
        }
    }
}
