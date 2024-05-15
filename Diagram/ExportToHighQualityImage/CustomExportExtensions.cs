using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace ExportToHighQualityImage
{
    public static class CustomExportExtensions
    {
        private const int ImageTileRows = 6;
        private const int ImageTileColumns = 6;

        public static void ExportToImage(RadDiagram diagram, Stream stream, BitmapEncoder encoder = null, Rect? enclosingBounds = null, Size returnImageSize = new Size(), Brush backgroundBrush = null, Thickness margin = new Thickness(), double dpi = 96)
        {
            if (enclosingBounds == null)
            {
                enclosingBounds = diagram.CalculateEnclosingBounds();             
            }

            if (encoder == null)
            {
                encoder = new PngBitmapEncoder();
            }
            
            var image = CreateDiagramImage(diagram, enclosingBounds.Value, returnImageSize, backgroundBrush, margin, dpi);
            if (image != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
            }
        }

        private static BitmapSource CreateDiagramImage(RadDiagram diagram, Rect enclosingBounds, Size returnImageSize, Brush backgroundBrush, Thickness margin, double dpi)
        {
            var virtualizationService = diagram.ServiceLocator.GetService<IVirtualizationService>() as VirtualizationService;
            virtualizationService.ForceRealization();
            diagram.UpdateLayout();
            
            var itemsHost = diagram.FindChildByType<DiagramSurface>();
            BitmapSource image = CreateWriteableBitmap(itemsHost, enclosingBounds, returnImageSize, backgroundBrush, margin, dpi);

            virtualizationService.Virtualize();
            diagram.UpdateLayout();

            return image;
        }

        private static BitmapSource CreateWriteableBitmap(UIElement element, Rect enclosingBounds, Size returnImageSize, Brush backgroundBrush, Thickness margin, double dpi = 96)
        {
            var expandedBounds = enclosingBounds.InflateRect(margin.Left, margin.Top, margin.Right, margin.Bottom);

            if (element == null || IsSizeValid(returnImageSize) == false ||
                enclosingBounds.IsValidBounds() == false || expandedBounds.IsValidBounds() == false)
            {
                return null;
            }

            if (returnImageSize.Width <= 0 || returnImageSize.Height <= 0)
            {
                returnImageSize = expandedBounds.ToSize();
            }

            var scale = new ScaleTransform();
            if (expandedBounds.Width > 0 || expandedBounds.Height > 0)
            {
                scale.ScaleX = returnImageSize.Width / expandedBounds.Width;
                scale.ScaleY = returnImageSize.Height / expandedBounds.Height;
            }

            dpi = CoerceDpi(dpi);
            double dpiScale = dpi / 96.0;
            var scaledwidth = (int)(returnImageSize.Width * dpiScale);
            var scaledHeight = (int)(returnImageSize.Height * dpiScale);
                        
            var transformation = new TransformGroup();
            transformation.Children.Add(scale);

            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.PushTransform(transformation);
                if (backgroundBrush != null)
                {
                    drawingContext.DrawRectangle(backgroundBrush, null, new Rect(new Point(0, 0), expandedBounds.ToSize()));
                }

                DrawTiles(element, expandedBounds, drawingContext);

            }
          
            var renderTarget = new RenderTargetBitmap(scaledwidth, scaledHeight, dpi, dpi, PixelFormats.Pbgra32);          
            renderTarget.Render(drawingVisual);
            var bitmap = new WriteableBitmap(renderTarget);

            return bitmap;
        }

        private static Random r = new Random();

        private static void DrawTiles(UIElement renderSurface, Rect shapesBounds, DrawingContext drawingContext)
        {
            double tileWidth = Math.Ceiling(shapesBounds.Width / ImageTileColumns) + 1;
            double tileHeight = Math.Ceiling(shapesBounds.Height / ImageTileRows) + 1; 

            for (int rowIndex = 0; rowIndex < ImageTileRows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < ImageTileColumns; colIndex++)
                {
                    var x = Math.Floor(colIndex * tileWidth);
                    var y = Math.Floor(rowIndex * tileHeight);
                    var sourceX = Math.Floor(shapesBounds.Left) + x;
                    var sourceY = Math.Floor(shapesBounds.Top) + y;

                    var sourceBrush = new VisualBrush(renderSurface)
                    {                        
                        Stretch = Stretch.None,                        
                        Viewbox = new Rect(sourceX, sourceY, tileWidth, tileHeight),
                        ViewboxUnits = BrushMappingMode.Absolute
                    };

                    Rect drawingContextPortionRect = new Rect(x, y, tileWidth, tileHeight);

                    // The GuidelineSet is used in order to align the drawn tile to physical device pixels. 
                    // Otherwise, you can observe blurred lines ot the tiles borders which leads to faded vertical and horizontal lines that goes accross the exported picture.
                    GuidelineSet guidelines = new GuidelineSet();
                    guidelines.GuidelinesX.Add(drawingContextPortionRect.Left);
                    guidelines.GuidelinesX.Add(drawingContextPortionRect.Right);
                    guidelines.GuidelinesY.Add(drawingContextPortionRect.Top);
                    guidelines.GuidelinesY.Add(drawingContextPortionRect.Bottom);
                    drawingContext.PushGuidelineSet(guidelines);

                    drawingContext.DrawRectangle(sourceBrush, null, drawingContextPortionRect);
                }
            }
        }

        private static bool IsSizeValid(Size size)
        {
            return !(size.Width.IsNanOrInfinity() || size.Height.IsNanOrInfinity());
        }

        private static double CoerceDpi(double dpi)
        {
            return (dpi > 0 && !dpi.IsNanOrInfinity()) ? dpi : 96d;
        }
    }
}
