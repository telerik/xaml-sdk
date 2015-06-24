using Telerik.Windows.Controls;
using System;
using System.Linq;
using System.Windows.Media;
using System.Windows;

namespace CustomConnectionCaps
{
    public class CustomConnection : RadDiagramConnection
    {
        protected override PathFigure CreateSourceCapGeometry(System.Windows.Point startPoint, System.Windows.Point endPoint, ref System.Windows.Point baseLineStart)
        {
            baseLineStart = startPoint;

            var size = this.SourceCapSize;
            var topLeft = new Point(startPoint.X - size.Width / 2, startPoint.Y - size.Height / 2);

            var result = new PathFigure();
            result.StartPoint = topLeft;

            var rect = new PolyLineSegment();
            rect.Points.Add(topLeft);
            rect.Points.Add(new Point(topLeft.X + size.Width, topLeft.Y));
            rect.Points.Add(new Point(topLeft.X + size.Width, topLeft.Y + size.Height));
            rect.Points.Add(new Point(topLeft.X, topLeft.Y + size.Height));

            result.Segments.Add(rect);

            return result;
        }

        protected override PathFigure CreateTargetCapGeometry(Point startPoint, Point endPoint, ref Point baseLineEnd)
        {
            var result = new PathFigure();
            result.StartPoint = startPoint;

            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X, startPoint.Y - 2) });
            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X + 4, startPoint.Y - 5) });
            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X + 6, startPoint.Y - 3) });
            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X + 2, startPoint.Y) });

            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X + 6, startPoint.Y + 3) });
            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X + 4, startPoint.Y + 5) });
            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X, startPoint.Y + 2) });

            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X - 4, startPoint.Y + 5) });
            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X - 6, startPoint.Y + 3) });
            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X - 2, startPoint.Y) });

            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X - 6, startPoint.Y - 3) });
            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X - 4, startPoint.Y - 5) });
            result.Segments.Add(new LineSegment() { Point = new Point(startPoint.X, startPoint.Y - 2) });
            return result;
        }
    }
}
