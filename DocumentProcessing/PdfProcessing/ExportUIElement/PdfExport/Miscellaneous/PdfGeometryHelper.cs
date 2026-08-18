using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace ExportUIElement
{
    internal static class PdfGeometryHelper
    {
        private static readonly Dictionary<Type, Action<PathSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure>> segmentConverters;

        static PdfGeometryHelper()
        {
            segmentConverters = new Dictionary<Type, Action<PathSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure>>();
            segmentConverters.Add(typeof(ArcSegment), (path, pathFigure) => ToArcSegments(((ArcSegment)path), pathFigure));
            segmentConverters.Add(typeof(BezierSegment), (path, pathFigure) => ToBezierSegments(((BezierSegment)path), pathFigure));
            segmentConverters.Add(typeof(PolyBezierSegment), (path, pathFigure) => ConvertPolyBezierSegments(((PolyBezierSegment)path), pathFigure));
            segmentConverters.Add(typeof(LineSegment), (path, pathFigure) => ToLineSegments(((LineSegment)path), pathFigure));
            segmentConverters.Add(typeof(PolyLineSegment), (path, pathFigure) => ConvertPolyLineSegment(((PolyLineSegment)path), pathFigure));
            segmentConverters.Add(typeof(QuadraticBezierSegment), (path, pathFigure) => ToQuadraticBezierSegments(((QuadraticBezierSegment)path), pathFigure));
            segmentConverters.Add(typeof(PolyQuadraticBezierSegment), (path, pathFigure) => ConvertPolyQuadraticBezierSegment(((PolyQuadraticBezierSegment)path), pathFigure));
        }

        public static Telerik.Documents.Fixed.Model.Graphics.FillRule ConvertFillRule(FillRule fillRule)
        {
            switch (fillRule)
            {
                case FillRule.EvenOdd:
                    return Telerik.Documents.Fixed.Model.Graphics.FillRule.EvenOdd;
                case FillRule.Nonzero:
                    return Telerik.Documents.Fixed.Model.Graphics.FillRule.Nonzero;
                default:
                    throw new NotSupportedException(String.Format("Not supported fill rule: {0}", fillRule));
            }
        }

        public static Telerik.Documents.Fixed.Model.Graphics.GeometryBase ConvertGeometry(Geometry geometry)
        {
            PathGeometry pathGeometry = geometry as PathGeometry;
            if (pathGeometry != null)
            {
                return ConvertPathGeometry(pathGeometry);
            }

            RectangleGeometry rectangleGeometry = geometry as RectangleGeometry;
            if (rectangleGeometry != null)
            {
                return ConvertRectangleGeometry(rectangleGeometry);
            }

            EllipseGeometry ellipseGeometry = geometry as EllipseGeometry;
            if (ellipseGeometry != null)
            {
                return ConvertEllipseGeometry(ellipseGeometry);
            }

            StreamGeometry streamGeometry = geometry as StreamGeometry;
            if (streamGeometry != null)
            {
                return ConvertStreamGeometry(streamGeometry);
            }

            return null;
        }

        public static Telerik.Documents.Fixed.Model.Graphics.PathGeometry ConvertPathGeometry(PathGeometry pathGeometry)
        {
            var pdfPathGeometry = new Telerik.Documents.Fixed.Model.Graphics.PathGeometry();
            pdfPathGeometry.FillRule = ConvertFillRule(pathGeometry.FillRule);

            foreach (PathFigure figure in pathGeometry.Figures)
            {
                pdfPathGeometry.Figures.Add(ConvertPathFigure(figure));
            }

            return pdfPathGeometry;
        }

        public static Telerik.Documents.Fixed.Model.Graphics.PathGeometry ConvertStreamGeometry(StreamGeometry streamGeometry)
        {
            PathGeometry pathGeometry = streamGeometry.GetFlattenedPathGeometry();
            return ConvertPathGeometry(pathGeometry);
        }

        public static Telerik.Documents.Fixed.Model.Graphics.RectangleGeometry ConvertRectangleGeometry(RectangleGeometry rectangleGeometry)
        {
            return new Telerik.Documents.Fixed.Model.Graphics.RectangleGeometry(rectangleGeometry.Rect);
        }

        public static Telerik.Documents.Fixed.Model.Graphics.PathGeometry ConvertEllipseGeometry(EllipseGeometry ellipseGeometry)
        {
            var pathFigure = new Telerik.Documents.Fixed.Model.Graphics.PathFigure();
            pathFigure.StartPoint = new Point(ellipseGeometry.RadiusX, 0);

            var arcSegment = new Telerik.Documents.Fixed.Model.Graphics.ArcSegment();
            arcSegment.Point = pathFigure.StartPoint;
            arcSegment.RotationAngle = 180;
            arcSegment.RadiusX = ellipseGeometry.RadiusX;
            arcSegment.RadiusY = ellipseGeometry.RadiusY;
            pathFigure.Segments.Add(arcSegment);

            arcSegment = new Telerik.Documents.Fixed.Model.Graphics.ArcSegment();
            arcSegment.Point = new Point(ellipseGeometry.RadiusX, 2 * ellipseGeometry.RadiusY);
            arcSegment.RotationAngle = 180;
            arcSegment.RadiusX = ellipseGeometry.RadiusX;
            arcSegment.RadiusY = ellipseGeometry.RadiusY;
            pathFigure.Segments.Add(arcSegment);

            var pathGeometry = new Telerik.Documents.Fixed.Model.Graphics.PathGeometry();
            pathFigure.StartPoint = arcSegment.Point;
            pathGeometry.Figures.Add(pathFigure);

            return pathGeometry;
        }

        public static Telerik.Documents.Fixed.Model.Graphics.PathFigure ConvertPathFigure(PathFigure pathFigure)
        {
            var pdfFigure = new Telerik.Documents.Fixed.Model.Graphics.PathFigure();
            pdfFigure.IsClosed = pathFigure.IsClosed;
            pdfFigure.StartPoint = pathFigure.StartPoint;

            foreach (PathSegment segment in pathFigure.Segments)
            {
                ConvertPathSegments(segment, pdfFigure);
            }

            return pdfFigure;
        }

        public static void ConvertPathSegments(PathSegment pathSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure pathFigure)
        {
            Type segmentType = pathSegment.GetType();
            Action<PathSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure> converter;

            if (!segmentConverters.TryGetValue(segmentType, out converter))
            {
                throw new NotSupportedException(String.Format("Not supported PathSegment type: {0}", segmentType));
            }

            converter(pathSegment, pathFigure);
        }

        public static Telerik.Documents.Fixed.Model.Graphics.SweepDirection ConvertSweepDirection(SweepDirection sweepDirection)
        {
            return sweepDirection == SweepDirection.Clockwise ?
                Telerik.Documents.Fixed.Model.Graphics.SweepDirection.Clockwise :
                Telerik.Documents.Fixed.Model.Graphics.SweepDirection.Counterclockwise;
        }

        public static void ConvertPolyBezierSegments(PolyBezierSegment polyBezierSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure pathFigure)
        {
            var points = polyBezierSegment.Points;

            for (int index = 2; index < points.Count; index += 3)
            {
                pathFigure.Segments.AddBezierSegment(points[index - 2], points[index - 1], points[index]);
            }
        }

        public static void ConvertLineSegment(LineSegment lineSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure pathFigure)
        {
            pathFigure.Segments.AddLineSegment(lineSegment.Point);
        }

        public static void ConvertPolyLineSegment(PolyLineSegment polyLineSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure pathFigure)
        {
            foreach (Point point in polyLineSegment.Points)
            {
                pathFigure.Segments.AddLineSegment(point);
            }
        }

        public static void ConvertQuadraticBezierSegment(QuadraticBezierSegment quadraticBezierSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure pathFigure)
        {
            pathFigure.Segments.AddQuadraticBezierSegment(quadraticBezierSegment.Point1, quadraticBezierSegment.Point2);
        }

        public static void ConvertPolyQuadraticBezierSegment(PolyQuadraticBezierSegment polyQuadraticBezierSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure pathFigure)
        {
            var points = polyQuadraticBezierSegment.Points;

            for (int index = 1; index < points.Count; index += 2)
            {
                pathFigure.Segments.AddQuadraticBezierSegment(points[index - 1], points[index]);
            }
        }

        private static void ToArcSegments(ArcSegment arcSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure pathFigure)
        {
            var pdfArcSegment = pathFigure.Segments.AddArcSegment();
            pdfArcSegment.IsLargeArc = arcSegment.IsLargeArc;
            pdfArcSegment.SweepDirection = ConvertSweepDirection(arcSegment.SweepDirection);
            pdfArcSegment.RotationAngle = arcSegment.RotationAngle;
            pdfArcSegment.RadiusX = arcSegment.Size.Width;
            pdfArcSegment.RadiusY = arcSegment.Size.Height;
            pdfArcSegment.Point = arcSegment.Point;
        }

        private static IEnumerable<Telerik.Documents.Fixed.Model.Graphics.BezierSegment> ToBezierSegments(BezierSegment bezierSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure pathFigure)
        {
            pathFigure.Segments.AddBezierSegment(bezierSegment.Point1, bezierSegment.Point2, bezierSegment.Point3);
            return null;
        }

        private static void ToLineSegments(LineSegment lineSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure pathFigure)
        {
            ConvertLineSegment(lineSegment, pathFigure);
        }

        private static void ToQuadraticBezierSegments(QuadraticBezierSegment quadraticBezierSegment, Telerik.Documents.Fixed.Model.Graphics.PathFigure pathFigure)
        {
            ConvertQuadraticBezierSegment(quadraticBezierSegment, pathFigure);
        }
    }
}