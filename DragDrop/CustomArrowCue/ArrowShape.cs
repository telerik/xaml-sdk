using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CustomArrowCue
{
    public sealed class ArrowShape : Shape
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public double HeadWidth { get; set; }
        public double HeadHeight { get; set; }

        private static Tuple<Point, Point> GetArrowPoints(Point startPoint, Point endPoint, double arrowWidth, double arrowHeight)
        {
            arrowHeight = arrowHeight / 2;
            double theta = Math.Atan2(startPoint.Y - endPoint.Y, startPoint.X - endPoint.X);
            double sint = Math.Round(Math.Sin(theta), 2);
            double cost = Math.Round(Math.Cos(theta), 2);

            Point leftPoint = new Point(endPoint.X + ((arrowWidth * cost) - (arrowHeight * sint)), endPoint.Y + ((arrowWidth * sint) + (arrowHeight * cost)));
            Point rightPoint = new Point(endPoint.X + ((arrowWidth * cost) + (arrowHeight * sint)), endPoint.Y - ((arrowHeight * cost) - (arrowWidth * sint)));

            return new Tuple<Point, Point>(leftPoint, rightPoint);
        }

        public void UpdateGeometry()
        {
            this.InvalidateVisual();
        }

        protected override Geometry DefiningGeometry
        {
            get { return this.CreateArrowGeometry(); }
        }

        private StreamGeometry CreateArrowGeometry()
        {
            StreamGeometry geometry = new StreamGeometry() { FillRule = FillRule.EvenOdd };
            using (StreamGeometryContext context = geometry.Open())
            {
                Point startPoint = new Point(X1, this.Y1);
                Point endPoint = new Point(X2, this.Y2);

                Tuple<Point, Point> arrowPoints = GetArrowPoints(startPoint, endPoint, this.HeadWidth, this.HeadHeight);
                Point leftPoint = arrowPoints.Item1;
                Point rightPoint = arrowPoints.Item2;

                context.BeginFigure(startPoint, true, false);
                context.LineTo(endPoint, true, true);
                context.LineTo(leftPoint, true, true);
                context.LineTo(endPoint, true, true);
                context.LineTo(rightPoint, true, true);
            }
            geometry.Freeze();
            return geometry;
        }
    }
}
