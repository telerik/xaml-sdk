using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Windows.Documents.DocumentStructure;
using Telerik.Windows.Documents.Layout;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.UI.Layers;

namespace CustomLayerAndCustomAnnotation
{
    public class CustomRangeLayer : DecorationUILayerBase
    {
        public override string Name
        {
            get
            {
                return "CustomRangeLayer";
            }
        }

        protected virtual bool ShouldUpdateUI()
        {
            return true;
        }

        public override void UpdateUIViewPortOverride(UILayerUpdateContext context)
        {
            if (this.ShouldUpdateUI())
            {
                foreach (AnnotationMarkerLayoutBox marker in context.GetVisibleLayoutBoxes<AnnotationMarkerLayoutBox>(AnnotationBoxFilter))
                {
                    this.AddRangeBracket(marker);
                }
            }
        }

        protected void AddRangeBracket(AnnotationMarkerLayoutBox marker)
        {
            Color bracketColor = GetBracketColor();

            Point topPoint;
            Point bottomPoint;
            InlineLayoutBox inlineBox = marker;
            if (marker.AssociatedAnnotationMarker is AnnotationRangeStart)
            {
                inlineBox = (InlineLayoutBox)DocumentStructureCollection.GetNextSiblingForDocumentElement(marker, null);

                topPoint = new Point(
                    inlineBox.ControlBoundingRectangle.Left,
                    inlineBox.ControlBoundingRectangle.Top - (inlineBox.LineInfo.BaselineOffset - inlineBox.BaselineOffset));

                bottomPoint = new Point(topPoint.X, topPoint.Y + inlineBox.LineInfo.Height);
            }
            else
            {
                inlineBox = (InlineLayoutBox)DocumentStructureCollection.GetPreviousSiblingForDocumentElement(marker, null);

                topPoint = new Point(
                    inlineBox.ControlBoundingRectangle.Right,
                    inlineBox.ControlBoundingRectangle.Top - (inlineBox.LineInfo.BaselineOffset - inlineBox.BaselineOffset));

                bottomPoint = new Point(topPoint.X, topPoint.Y + inlineBox.LineInfo.Height);
            }


            Polyline polyline = new Polyline();
            Canvas.SetZIndex(polyline, 5);
            polyline.Stroke = new SolidColorBrush(bracketColor);
            polyline.StrokeThickness = 1;
            if (marker.AssociatedAnnotationMarker is AnnotationRangeStart)
            {
                polyline.Points.Add(new Point(topPoint.X + 2, topPoint.Y));
                polyline.Points.Add(topPoint);
                polyline.Points.Add(bottomPoint);
                polyline.Points.Add(new Point(bottomPoint.X + 2, bottomPoint.Y));
            }
            else
            {
                polyline.Points.Add(new Point(topPoint.X - 2, topPoint.Y));
                polyline.Points.Add(topPoint);
                polyline.Points.Add(bottomPoint);
                polyline.Points.Add(new Point(bottomPoint.X - 2, bottomPoint.Y));
            }

            base.AddDecorationElement(polyline);
        }

        private static bool AnnotationBoxFilter(AnnotationMarkerLayoutBox annotationMarkerBox)
        {
            return (annotationMarkerBox.AssociatedAnnotationMarker is CustomRangeStart || annotationMarkerBox.AssociatedAnnotationMarker is CustomRangeEnd);
        }

        private static Color GetBracketColor()
        {
            return Colors.Red;
        }
    }
}
