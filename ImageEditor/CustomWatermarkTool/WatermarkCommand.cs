using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.Commands;

namespace CustomWatermarkTool
{
    public class WatermarkCommand : IImageCommand
    {
        public RadBitmap Execute(RadBitmap source, object context)
        {
            WatermarkCommandContext myContext = (WatermarkCommandContext)context;

            Grid grid = new Grid();
            grid.Children.Add(new Image()
            {
                Source = source.Bitmap,
                Stretch = Stretch.None
            });

            Image image = new Image()
            {
                Source = myContext.Image.Bitmap,
                Stretch = Stretch.None,
                Opacity = myContext.Opacity,
            };

            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = myContext.Scale;
            scaleTransform.ScaleY = myContext.Scale;

            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = myContext.Rotation;

            TransformGroup transform = new TransformGroup();
            transform.Children.Add(rotateTransform);
            transform.Children.Add(scaleTransform);

            image.RenderTransform = transform;
            image.RenderTransformOrigin = new Point(0.5, 0.5);

            grid.Children.Add(image);

            return new RadBitmap(source.Width, source.Height, grid);
        }
    }
}
