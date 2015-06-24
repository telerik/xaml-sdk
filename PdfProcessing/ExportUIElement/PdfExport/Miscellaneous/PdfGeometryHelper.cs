using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace ExportUIElement
{
    internal static class PdfGeometryHelper
    {
        private static readonly Dictionary<Type, Func<PathSegment, IEnumerable<Telerik.Windows.Documents.Fixed.Model.Graphics.PathSegment>>> segmentConverters;

        static PdfGeometryHelper()
        {
            segmentConverters = new Dictionary<Type, Func<PathSegment, IEnumerable<Telerik.Windows.Documents.Fixed.Model.Graphics.PathSegment>>>();
            segmentConverters.Add(typeof(ArcSegment), (path) => ToArcSegments(((ArcSegment)path)));
            segmentConverters.Add(typeof(BezierSegment), (path) => ToBezierSegments(((BezierSegment)path)));
            segmentConverters.Add(typeof(PolyBezierSegment), (path) => ConvertPolyBezierSegments(((PolyBezierSegment)path)));
            segmentConverters.Add(typeof(LineSegment), (path) => ToLineSegments(((LineSegment)path)));
            segmentConverters.Add(typeof(PolyLineSegment), (path) => ConvertPolyLineSegment(((PolyLineSegment)path)));
            segmentConverters.Add(typeof(QuadraticBezierSegment), (path) => ToQuadraticBezierSegments(((QuadraticBezierSegment)path)));
            segmentConverters.Add(typeof(PolyQuadraticBezierSegment), (path) => ConvertPolyQuadraticBezierSegment(((PolyQuadraticBezierSegment)path)));
        }

        public static Telerik.Windows.Documents.Fixed.Model.Graphics.FillRule ConvertFillRule(FillRule fillRule)
        {
            switch (fillRule)
            {
                case FillRule.EvenOdd:
                    return Telerik.Windows.Documents.Fixed.Model.Graphics.FillRule.EvenOdd;
                case FillRule.Nonzero:
                    return Telerik.Windows.Documents.Fixed.Model.Graphics.FillRule.Nonzero;
                default:
                    throw new NotSupportedException(String.Format("Not supported fill rule: {0}", fillRule));
            }
        }

        public static Telerik.Windows.Documents.Fixed.Model.Graphics.GeometryBase ConvertGeometry(Geometry geometry)
        {
#if SILVERLIGHT
            var cloner = new Telerik.Windows.Controls.GeometryCloneConverter();
            geometry = (Geometry)cloner.Convert(geometry, null, null, null);
#endif

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

#if WPF
            StreamGeometry streamGeometry = geometry as StreamGeometry;
            if (streamGeometry != null)
            {
                return ConvertStreamGeometry(streamGeometry);
            }
#endif

            return null;
        }

        public static Telerik.Windows.Documents.Fixed.Model.Graphics.PathGeometry ConvertPathGeometry(PathGeometry pathGeometry)
        {
            var pdfPathGeometry = new Telerik.Windows.Documents.Fixed.Model.Graphics.PathGeometry();
            pdfPathGeometry.FillRule = ConvertFillRule(pathGeometry.FillRule);

            foreach (PathFigure figure in pathGeometry.Figures)
            {
                pdfPathGeometry.Figures.Add(ConvertPathFigure(figure));
            }

            return pdfPathGeometry;
        }

#if WPF
        public static Telerik.Windows.Documents.Fixed.Model.Graphics.PathGeometry ConvertStreamGeometry(StreamGeometry streamGeometry)
        {
            PathGeometry pathGeometry = streamGeometry.GetFlattenedPathGeometry();
            return ConvertPathGeometry(pathGeometry);
        }
#endif

        public static Telerik.Windows.Documents.Fixed.Model.Graphics.RectangleGeometry ConvertRectangleGeometry(RectangleGeometry rectangleGeometry)
        {
            return new Telerik.Windows.Documents.Fixed.Model.Graphics.RectangleGeometry(rectangleGeometry.Rect);
        }

        public static Telerik.Windows.Documents.Fixed.Model.Graphics.PathGeometry ConvertEllipseGeometry(EllipseGeometry ellipseGeometry)
        {
            var pathFigure = new Telerik.Windows.Documents.Fixed.Model.Graphics.PathFigure();
            pathFigure.StartPoint = new Point(ellipseGeometry.RadiusX, 0);

            var arcSegment = new Telerik.Windows.Documents.Fixed.Model.Graphics.ArcSegment();
            arcSegment.Point = pathFigure.StartPoint;
            arcSegment.RotationAngle = 180;
            arcSegment.RadiusX = ellipseGeometry.RadiusX;
            arcSegment.RadiusY = ellipseGeometry.RadiusY;
            pathFigure.Segments.Add(arcSegment);

            arcSegment = new Telerik.Windows.Documents.Fixed.Model.Graphics.ArcSegment();
            arcSegment.Point = new Point(ellipseGeometry.RadiusX, 2 * ellipseGeometry.RadiusY);
            arcSegment.RotationAngle = 180;
            arcSegment.RadiusX = ellipseGeometry.RadiusX;
            arcSegment.RadiusY = ellipseGeometry.RadiusY;
            pathFigure.Segments.Add(arcSegment);

            var pathGeometry = new Telerik.Windows.Documents.Fixed.Model.Graphics.PathGeometry();
            pathFigure.StartPoint = arcSegment.Point;
            pathGeometry.Figures.Add(pathFigure);

            return pathGeometry;
        }

        public static Telerik.Windows.Documents.Fixed.Model.Graphics.PathFigure ConvertPathFigure(PathFigure pathFigure)
        {
            var pdfFigure = new Telerik.Windows.Documents.Fixed.Model.Graphics.PathFigure();
            pdfFigure.IsClosed = pathFigure.IsClosed;
            pdfFigure.StartPoint = pathFigure.StartPoint;

            foreach (PathSegment segment in pathFigure.Segments)
            {
                foreach (var pdfSegment in ConvertPathSegments(segment))
                {
                    pdfFigure.Segments.Add(pdfSegment);
                }
            }

            return pdfFigure;
        }

        public static IEnumerable<Telerik.Windows.Documents.Fixed.Model.Graphics.PathSegment> ConvertPathSegments(PathSegment pathSegment)
        {
            Type segmentType = pathSegment.GetType();
            Func<PathSegment, IEnumerable<Telerik.Windows.Documents.Fixed.Model.Graphics.PathSegment>> converter;

            if (!segmentConverters.TryGetValue(segmentType, out converter))
            {
                throw new NotSupportedException(String.Format("Not supported PathSegment type: {0}", segmentType));
            }

            return converter(pathSegment);
        }

        public static Telerik.Windows.Documents.Fixed.Model.Graphics.SweepDirection ConvertSweepDirection(SweepDirection sweepDirection)
        {
            return sweepDirection == SweepDirection.Clockwise ?
                Telerik.Windows.Documents.Fixed.Model.Graphics.SweepDirection.Clockwise :
                Telerik.Windows.Documents.Fixed.Model.Graphics.SweepDirection.Counterclockwise;
        }

        public static Telerik.Windows.Documents.Fixed.Model.Graphics.ArcSegment ConvertArcSegment(ArcSegment arcSegment)
        {
            var pdfArcSegment = new Telerik.Windows.Documents.Fixed.Model.Graphics.ArcSegment();
            pdfArcSegment.IsLargeArc = arcSegment.IsLargeArc;
            pdfArcSegment.SweepDirection = ConvertSweepDirection(arcSegment.SweepDirection);
            pdfArcSegment.RotationAngle = arcSegment.RotationAngle;
            pdfArcSegment.RadiusX = arcSegment.Size.Width;
            pdfArcSegment.RadiusY = arcSegment.Size.Height;
            pdfArcSegment.Point = arcSegment.Point;

            return pdfArcSegment;
        }

        public static Telerik.Windows.Documents.Fixed.Model.Graphics.BezierSegment ConvertBezierSegment(BezierSegment bezierSegment)
        {
            var pdfBezierSegment = new Telerik.Windows.Documents.Fixed.Model.Graphics.BezierSegment();
            pdfBezierSegment.Point1 = bezierSegment.Point1;
            pdfBezierSegment.Point2 = bezierSegment.Point2;
            pdfBezierSegment.Point3 = bezierSegment.Point3;

            return pdfBezierSegment;
        }

        public static IEnumerable<Telerik.Windows.Documents.Fixed.Model.Graphics.BezierSegment> ConvertPolyBezierSegments(PolyBezierSegment polyBezierSegment)
        {
            var points = polyBezierSegment.Points;

            for (int index = 2; index < points.Count; index += 3)
            {
                var pdfBezierSegment = new Telerik.Windows.Documents.Fixed.Model.Graphics.BezierSegment();
                pdfBezierSegment.Point1 = points[index - 2];
                pdfBezierSegment.Point2 = points[index - 1];
                pdfBezierSegment.Point3 = points[index];

                yield return pdfBezierSegment;
            }
        }

        public static Telerik.Windows.Documents.Fixed.Model.Graphics.LineSegment ConvertLineSegment(LineSegment lineSegment)
        {
            var pdfLineSegment = new Telerik.Windows.Documents.Fixed.Model.Graphics.LineSegment();
            pdfLineSegment.Point = lineSegment.Point;

            return pdfLineSegment;
        }

        public static IEnumerable<Telerik.Windows.Documents.Fixed.Model.Graphics.LineSegment> ConvertPolyLineSegment(PolyLineSegment polyLineSegment)
        {
            foreach (Point point in polyLineSegment.Points)
            {
                var pdfLineSegment = new Telerik.Windows.Documents.Fixed.Model.Graphics.LineSegment();
                pdfLineSegment.Point = point;

                yield return pdfLineSegment;
            }
        }

        public static Telerik.Windows.Documents.Fixed.Model.Graphics.QuadraticBezierSegment ConvertQuadraticBezierSegment(QuadraticBezierSegment quadraticBezierSegment)
        {
            var pdfQuadraticBezierSegment = new Telerik.Windows.Documents.Fixed.Model.Graphics.QuadraticBezierSegment();
            pdfQuadraticBezierSegment.Point1 = quadraticBezierSegment.Point1;
            pdfQuadraticBezierSegment.Point2 = quadraticBezierSegment.Point2;

            return pdfQuadraticBezierSegment;
        }

        public static IEnumerable<Telerik.Windows.Documents.Fixed.Model.Graphics.QuadraticBezierSegment> ConvertPolyQuadraticBezierSegment(PolyQuadraticBezierSegment polyQuadraticBezierSegment)
        {
            var points = polyQuadraticBezierSegment.Points;

            for (int index = 1; index < points.Count; index += 2)
            {
                var pdfQuadraticBezierSegment = new Telerik.Windows.Documents.Fixed.Model.Graphics.QuadraticBezierSegment();
                pdfQuadraticBezierSegment.Point1 = points[index - 1];
                pdfQuadraticBezierSegment.Point2 = points[index];

                yield return pdfQuadraticBezierSegment;
            }
        }

        private static IEnumerable<Telerik.Windows.Documents.Fixed.Model.Graphics.ArcSegment> ToArcSegments(ArcSegment arcSegment)
        {
            yield return ConvertArcSegment(arcSegment);
        }

        private static IEnumerable<Telerik.Windows.Documents.Fixed.Model.Graphics.BezierSegment> ToBezierSegments(BezierSegment bezierSegment)
        {
            yield return ConvertBezierSegment(bezierSegment);
        }

        private static IEnumerable<Telerik.Windows.Documents.Fixed.Model.Graphics.LineSegment> ToLineSegments(LineSegment lineSegment)
        {
            yield return ConvertLineSegment(lineSegment);
        }

        private static IEnumerable<Telerik.Windows.Documents.Fixed.Model.Graphics.QuadraticBezierSegment> ToQuadraticBezierSegments(QuadraticBezierSegment quadraticBezierSegment)
        {
            yield return ConvertQuadraticBezierSegment(quadraticBezierSegment);
        }
    }
}