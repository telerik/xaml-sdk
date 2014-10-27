using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Data;

namespace ExportToPDF
{
    public class EditorInfo
    {
        public EditorInfo(RadFixedPage page, IPosition position, RadDiagramItem element, Rect bounds, Brush stroke, double angle)
        {
            this.Page = page;
            this.Position = position;
            this.Background = element.Background;
            this.StrokeThickness = element.StrokeThickness;
            this.StrokeDashArray = element.StrokeDashArray;
            this.Opacity = element.Opacity;
            this.Stroke = stroke;
            this.Bounds = bounds;
            this.Angle = angle;
        }

        public Brush Background { get; set; }
        public Brush Stroke { get; set; }
        public double Opacity { get; set; }
        public DoubleCollection StrokeDashArray { get; set; }
        public Rect Bounds { get; set; }
        public double StrokeThickness { get; set; }
        public double Angle { get; set; }
        public IPosition Position { get; set; }
        public RadFixedPage Page { get; set; }
    }
}
