using System;
using System.Linq;
using Telerik.Windows.Media.Imaging;

namespace CustomWatermarkTool
{
    public class WatermarkCommandContext
    {
        public double Opacity { get; private set; }
        public double Rotation { get; private set; }
        public double Scale { get; private set; }
        public RadBitmap Image { get; private set; }

        public WatermarkCommandContext(double opacity, double rotation, double scale, RadBitmap image)
        {
            this.Opacity = opacity;
            this.Rotation = rotation;
            this.Scale = scale;
            this.Image = image;
        }
    }
}
