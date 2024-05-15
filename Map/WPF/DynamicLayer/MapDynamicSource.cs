using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace DynamicLayer
{
    public class MapDynamicSource : IMapDynamicSource
    {
        public void ItemsRequest(object sender, ItemsRequestEventArgs e)
        {
            List<object> items = new List<object>();
            double minZoom = e.MinZoom;
            Location upperLeft = e.UpperLeft;
            Location lowerRight = e.LowerRight;
            HotSpot centerSpot = new HotSpot();
            centerSpot.X = 0.5;
            centerSpot.Y = 0.5;
            Location bulgariaLocation = new Location(42.7669999748468, 25.2819999307394);
            LocationRect currentRegion = new LocationRect(upperLeft, lowerRight);

            if (currentRegion.Contains(bulgariaLocation))
            {
                if (minZoom == 3)
                {
                    Ellipse ellipse = new Ellipse();
                    ellipse.Width = 15;
                    ellipse.Height = 15;
                    ellipse.Fill = new SolidColorBrush(Colors.Red);
                    ellipse.SetValue(MapLayer.LocationProperty, bulgariaLocation);
                    MapLayer.SetHotSpot(ellipse, centerSpot);
                    ToolTipService.SetToolTip(ellipse, "Bulgaria");
                    items.Add(ellipse);
                }
                else if (minZoom == 6)
                {
                    Ellipse sofiaEllipse = new Ellipse();
                    sofiaEllipse.Width = 20;
                    sofiaEllipse.Height = 20;
                    sofiaEllipse.Stroke = new SolidColorBrush(Colors.Red);
                    sofiaEllipse.Fill = new SolidColorBrush(Colors.Transparent);
                    sofiaEllipse.StrokeThickness = 3;
                    sofiaEllipse.SetValue(MapLayer.LocationProperty, new Location(42.6957539183824, 23.3327663758679));
                    MapLayer.SetHotSpot(sofiaEllipse, centerSpot);
                    ToolTipService.SetToolTip(sofiaEllipse, "Sofia");
                    items.Add(sofiaEllipse);
                }
            }
            e.CompleteItemsRequest(items);
        }
    }
}
