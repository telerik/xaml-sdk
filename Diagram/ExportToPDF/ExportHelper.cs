using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Data;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Text;
using G = Telerik.Windows.Documents.Fixed.Model.Graphics;

namespace ExportToPDF
{
    public static class ExportHelper
    {
        public static RadFixedPage ExportDiagram(RadDiagram diagram, Rect pageSize)
        {
            RadFixedPage page = new RadFixedPage();
            page.Size = pageSize.ToSize();

            var orderedContainers = diagram.Items.Select(i => diagram.ContainerGenerator.ContainerFromItem(i)).OrderBy(c => c.ZIndex);
            foreach (var container in orderedContainers)
            {
                if (container.Visibility != Visibility.Visible) continue;

                var shape = container as RadDiagramShape;
                if (shape != null)
                {
                    ExportShape(shape, pageSize, page);
                    continue;
                }

                var textShape = container as RadDiagramTextShape;
                if (textShape != null)
                {
                    ExportTextShape(textShape, pageSize, page);
                    continue;
                }

                var containerShape = container as RadDiagramContainerShape;
                if (containerShape != null)
                {
                    ExportContainerShape(containerShape, pageSize, page);
                    continue;
                }

                var connection = container as RadDiagramConnection;
                if (connection != null)
                {
                    ExportConnection(connection, pageSize, page);
                    continue;
                }
            }

            return page;
        }

        private static void ExportTextShape(RadDiagramTextShape shape, Rect enclosingBounds, RadFixedPage page)
        {
            var bounds = new Rect(shape.Bounds.X - enclosingBounds.X, shape.Bounds.Y - enclosingBounds.Y, shape.Bounds.Width, shape.Bounds.Height);

            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(new RotateTransform() { Angle = shape.RotationAngle, CenterX = bounds.Width / 2, CenterY = bounds.Height / 2 });
            transformGroup.Children.Add(new TranslateTransform() { X = bounds.X, Y = bounds.Y });

            var position = new MatrixPosition(transformGroup.Value);

            FixedContentEditor containerEditor = CreateEditor(new EditorInfo(page, position, shape, bounds, shape.BorderBrush, shape.RotationAngle), true);
            containerEditor.DrawRectangle(new Rect(new Point(), bounds.ToSize()));

            if (shape.Content != null)
            {
                var center = bounds.Center();
                ExportContent(shape, bounds, shape.RotationAngle, page, (s) => { return new Point(bounds.Center().X - s.Width / 2, center.Y - s.Height / 2); });
            }
        }

        private static void ExportConnection(RadDiagramConnection connection, Rect enclosingBounds, RadFixedPage page)
        {
            var bounds = new Rect(connection.Bounds.X - enclosingBounds.X, connection.Bounds.Y - enclosingBounds.Y, connection.Bounds.Width, connection.Bounds.Height);

            var pathGeometry = connection.Geometry as PathGeometry;
            var pathBounds = connection.ConnectionType == ConnectionType.Bezier ? pathGeometry.Bounds : new Rect();

            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(new TranslateTransform() { X = bounds.X - pathBounds.X, Y = bounds.Y - pathBounds.Y });
            var position = new MatrixPosition(transformGroup.Value);

            EditorInfo info = new EditorInfo(page, position, connection, bounds, connection.Stroke, 0);
            FixedContentEditor editor = CreateEditor(info, false);
            FixedContentEditor filledEditor = CreateEditor(info, true);

            ExportGeometry(editor, filledEditor, pathGeometry, true);

            if (connection.Content != null)
            {
                var center = bounds.Center();
                ExportContent(connection, bounds, 0, page, (s) => { return new Point(bounds.Center().X - s.Width / 2, center.Y - s.Height / 2); });
            }
        }

        private static void ExportContainerShape(RadDiagramContainerShape container, Rect enclosingBounds, RadFixedPage page)
        {
            var bounds = new Rect(container.Bounds.X - enclosingBounds.X, container.Bounds.Y - enclosingBounds.Y, container.Bounds.Width, container.Bounds.Height);

            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(new RotateTransform() { Angle = container.RotationAngle, CenterX = bounds.Width / 2, CenterY = bounds.Height / 2 });
            transformGroup.Children.Add(new TranslateTransform() { X = bounds.X, Y = bounds.Y });

            var position = new MatrixPosition(transformGroup.Value);

            FixedContentEditor containerEditor = CreateEditor(new EditorInfo(page, position, container, bounds, container.BorderBrush, container.RotationAngle), true);
            containerEditor.DrawRectangle(new Rect(new Point(), bounds.ToSize()));

            containerEditor.GraphicProperties.StrokeThickness = 0.5;
            var headerHeight = container.ContentBounds.Y - container.Bounds.Y - DiagramConstants.ContainerMargin;
            containerEditor.DrawRectangle(new Rect(new Point(0, headerHeight), new Size(bounds.Width, 0.5)));

            if (container.IsCollapsible)
            {
                var buttonTop = headerHeight / 2 - 2.5;
                var buttonLeft = bounds.Width - 20;
                if (container.IsCollapsed)
                {
                    containerEditor.DrawLine(new Point(buttonLeft, buttonTop), new Point(buttonLeft + 4, buttonTop + 4));
                    containerEditor.DrawLine(new Point(buttonLeft + 4, buttonTop + 4), new Point(buttonLeft + 8, buttonTop));
                    if (container.CollapsedContent != null)
                    {
                        var contentHeight = container.ActualHeight - headerHeight;
                        ExportContent(container, bounds, container.RotationAngle, page, (s) => { return new Point(bounds.Center().X - s.Width / 2, bounds.Bottom - contentHeight / 2 - s.Height / 2); }, container.CollapsedContent.ToString());
                    }
                }
                else
                {
                    containerEditor.DrawLine(new Point(buttonLeft, buttonTop + 4), new Point(buttonLeft + 4, buttonTop));
                    containerEditor.DrawLine(new Point(buttonLeft + 4, buttonTop), new Point(buttonLeft + 8, buttonTop + 4));
                }
            }

            if (container.Content != null)
            {
                ExportContent(container, bounds, container.RotationAngle, page, (s) => { return new Point(bounds.Center().X - s.Width / 2, bounds.Top + headerHeight / 2 - s.Height / 2); });
            }
        }

        private static void ExportShape(RadDiagramShape shape, Rect enclosingBounds, RadFixedPage page)
        {
            var bounds = new Rect(shape.Bounds.X - enclosingBounds.X, shape.Bounds.Y - enclosingBounds.Y, shape.Bounds.Width, shape.Bounds.Height);

            var pathGeometry = shape.Geometry as PathGeometry;
            var transformGroup = new TransformGroup();
#if WPF
            if (pathGeometry == null)
            {
                var streamGeometry = shape.Geometry as StreamGeometry;
                if (streamGeometry != null)
                    pathGeometry = streamGeometry.AsPathGeometry();
            }
#endif

            var geometrySize = shape.Geometry.Bounds.ToSize();
            if (IsValidSize(geometrySize) && (geometrySize.Width != bounds.Width || geometrySize.Width != bounds.Width))
                transformGroup.Children.Add(new ScaleTransform() { ScaleX = bounds.Width / geometrySize.Width, ScaleY = bounds.Height / geometrySize.Height });
            transformGroup.Children.Add(new RotateTransform() { Angle = shape.RotationAngle, CenterX = bounds.Width / 2, CenterY = bounds.Height / 2 });
            transformGroup.Children.Add(new TranslateTransform() { X = bounds.X, Y = bounds.Y });

            var position = new MatrixPosition(transformGroup.Value);

            EditorInfo info = new EditorInfo(page, position, shape, bounds, shape.BorderBrush, shape.RotationAngle);
            FixedContentEditor editor = CreateEditor(info, false);
            FixedContentEditor filledEditor = CreateEditor(info, true);

            ExportGeometry(editor, filledEditor, pathGeometry);

            if (shape.Content != null)
            {
                var center = bounds.Center();
                ExportContent(shape, bounds, shape.RotationAngle, page, (s) => { return new Point(center.X - s.Width / 2, center.Y - s.Height / 2); });
            }
        }

        private static void ExportContent(ContentControl control, Rect bounds, double angle, RadFixedPage page, Func<Size, Point> positionFunc, string contentString = null)
        {
            string text = contentString ?? control.Content.ToString();
            if (string.IsNullOrWhiteSpace(text)) return;
            FixedContentEditor textEditor = new FixedContentEditor(page);
            var block = new Block();

            // Set the text and graphic properties.
            block.TextProperties.FontSize = control.FontSize;
            block.TextProperties.RenderingMode = RenderingMode.Fill;
            block.TextProperties.TrySetFont(control.FontFamily, control.FontStyle, control.FontWeight);
            block.GraphicProperties.FillColor = ColorHelper.GetColor(control.Foreground, control.Opacity, bounds);
            block.GraphicProperties.StrokeColor = block.GraphicProperties.FillColor;

            // Measure the text.
            block.InsertText(text);
            var boundsSize = bounds.ToSize();
            var availableSize = new Size(boundsSize.Width - control.Padding.Left - control.Padding.Right, boundsSize.Width - control.Padding.Top - control.Padding.Bottom);
            var textSize = block.Measure(availableSize);
            var position = positionFunc(textSize);
            var textGroup = new TransformGroup();
            textGroup.Children.Add(new RotateTransform() { Angle = angle, CenterX = textSize.Width / 2, CenterY = textSize.Height / 2 });
            textGroup.Children.Add(new TranslateTransform() { X = position.X, Y = position.Y });
            textEditor.Position = new MatrixPosition(textGroup.Value);

            textEditor.DrawBlock(block, availableSize);
        }

        private static void ExportGeometry(FixedContentEditor editor, FixedContentEditor filledEditor, Geometry geometry, bool isConnection = false)
        {
            // We need two editors because there might be filled and not filled figures.
#if WPF
            var pathGeometry = geometry as PathGeometry;
#else
            var pathGeometry = GeometryParser.GetGeometry(geometry.ToString()) as PathGeometry;
#endif
            if (pathGeometry != null)
            {
                var path = new G.PathGeometry();
                var filledPath = new G.PathGeometry();
                for (int i = 0; i < pathGeometry.Figures.Count; i++)
                {
                    var figure = pathGeometry.Figures[i];
                    var newFigure = new G.PathFigure();
                    newFigure.StartPoint = figure.StartPoint;
                    newFigure.IsClosed = figure.IsClosed;
                    foreach (var segment in figure.Segments)
                    {
                        var arc = segment as ArcSegment;
                        if (arc != null)
                        {
                            var newS = new G.ArcSegment();
                            newS.Point = arc.Point;
                            newS.RadiusX = arc.Size.Width;
                            newS.RadiusY = arc.Size.Height;
                            newS.RotationAngle = arc.RotationAngle;
                            // why new enum ?
                            if (arc.SweepDirection == SweepDirection.Clockwise)
                                newS.SweepDirection = G.SweepDirection.Clockwise;
                            else
                                newS.SweepDirection = G.SweepDirection.Counterclockwise;
                            newS.IsLargeArc = arc.IsLargeArc;
                            newFigure.Segments.Add(newS);
                            continue;
                        }

                        var bezier = segment as BezierSegment;
                        if (bezier != null)
                        {
                            var newS = new G.BezierSegment();
                            newS.Point1 = bezier.Point1;
                            newS.Point2 = bezier.Point2;
                            newS.Point3 = bezier.Point3;
                            newFigure.StartPoint = newFigure.StartPoint;
                            newFigure.Segments.Add(newS);
                            continue;
                        }

                        var polyLine = segment as PolyLineSegment;
                        if (polyLine != null)
                        {
                            foreach (var point in polyLine.Points)
                            {
                                var newS = new G.LineSegment();
                                newS.Point = point;
                                newFigure.Segments.Add(newS);
                            }
                            continue;
                        }

                        var line = segment as LineSegment;
                        if (line != null)
                        {
                            var newS = new G.LineSegment();
                            newS.Point = line.Point;
                            newFigure.Segments.Add(newS);
                            continue;
                        }

                        var quadraticBezier = segment as QuadraticBezierSegment;
                        if (quadraticBezier != null)
                        {
                            var newS = new G.QuadraticBezierSegment();
                            newS.Point1 = quadraticBezier.Point1;
                            newS.Point2 = quadraticBezier.Point2;
                            newFigure.Segments.Add(newS);
                            continue;
                        }
                    }
#if SILVERLIGHT
                    if (isConnection)
                    {
                        var realGeometry = geometry as PathGeometry;
                        if (realGeometry != null && realGeometry.Figures.Count > i)
                        {
                            if (realGeometry.Figures[i].IsFilled)
                                filledPath.Figures.Add(newFigure);
                            else
                                path.Figures.Add(newFigure);
                            continue;
                        }
                    }
#endif
                    if (figure.IsFilled)
                        filledPath.Figures.Add(newFigure);
                    else
                        path.Figures.Add(newFigure);
                }

                // why new enum ?
                if (pathGeometry.FillRule == FillRule.EvenOdd)
                {
                    path.FillRule = G.FillRule.EvenOdd;
                    filledPath.FillRule = G.FillRule.EvenOdd;
                }
                else
                {
                    path.FillRule = G.FillRule.Nonzero;
                    filledPath.FillRule = G.FillRule.Nonzero;
                }

                if (filledPath.Figures.Count > 0)
                    filledEditor.DrawPath(filledPath);
                if (path.Figures.Count > 0)
                    editor.DrawPath(path);
            }
        }

        private static bool IsValidSize(Size geometrySize)
        {
            return geometrySize.Width != 0 && geometrySize.Height != 0 && !geometrySize.Width.IsNanOrInfinity() && !geometrySize.Height.IsNanOrInfinity();
        }

        private static FixedContentEditor CreateEditor(EditorInfo info, bool isFilled)
        {
            FixedContentEditor geometryEditor = new FixedContentEditor(info.Page, info.Position);
            geometryEditor.GraphicProperties.IsFilled = isFilled;
            geometryEditor.GraphicProperties.FillColor = ColorHelper.GetColor(info.Background, info.Opacity, info.Bounds, info.Angle);
            geometryEditor.GraphicProperties.StrokeColor = ColorHelper.GetColor(info.Stroke, info.Opacity, info.Bounds, info.Angle);
            geometryEditor.GraphicProperties.StrokeDashArray = info.StrokeDashArray;
            geometryEditor.GraphicProperties.StrokeThickness = info.StrokeThickness;
            return geometryEditor;
        }
    }
}
