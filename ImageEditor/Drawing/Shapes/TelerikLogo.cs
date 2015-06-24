using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Media.Imaging.Shapes;

namespace Drawing.Shapes
{
    public class TelerikLogo : IShape
    {
        public string DisplayName
        {
            get
            {
                return "Telerik";
            }
        }

        public Geometry GetShapeGeometry()
        {
            PathFigure outer = new PathFigure();
            outer.IsClosed = true;
            outer.StartPoint = new Point(0, 2.5);
            outer.Segments.Add(new LineSegment() { Point = new Point(2.5, 0) });
            outer.Segments.Add(new LineSegment() { Point = new Point(5, 2.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(7.5, 0) });
            outer.Segments.Add(new LineSegment() { Point = new Point(10, 2.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(9, 3.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(7.5, 2) });
            outer.Segments.Add(new LineSegment() { Point = new Point(6, 3.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(8.5, 6) });
            outer.Segments.Add(new LineSegment() { Point = new Point(5, 9.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(1.5, 6) });
            outer.Segments.Add(new LineSegment() { Point = new Point(4, 3.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(2.5, 2) });
            outer.Segments.Add(new LineSegment() { Point = new Point(1, 3.5) });

            PathFigure inner = new PathFigure();
            inner.StartPoint = new Point(3.5, 6);
            inner.IsClosed = true;
            inner.Segments.Add(new LineSegment() { Point = new Point(5, 7.5) });
            inner.Segments.Add(new LineSegment() { Point = new Point(6.5, 6) });
            inner.Segments.Add(new LineSegment() { Point = new Point(5, 4.5) });

            PathGeometry logoGeometry = new PathGeometry();
            logoGeometry.Figures.Add(inner);
            logoGeometry.Figures.Add(outer);

            return logoGeometry;
        }
    }
}