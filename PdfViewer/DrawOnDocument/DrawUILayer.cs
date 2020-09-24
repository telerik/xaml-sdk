using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Graphics;
using Telerik.Windows.Documents.Fixed.UI.Layers;
using Telerik.Windows.Documents.Fixed.Utilities.Rendering;

namespace DrawOnDocument
{
    public class DrawUILayer : IUILayer
    {
        private static readonly string LayerName = "DrawUILayer";
        private readonly Canvas canvas;
        private UILayerInitializeContext context;
        private Point mouseLocation;
        private PathGeometry pathGeometry;
        private PathFigure figure;

        static DrawUILayer()
        {
            IsDrawModeEnabled = true;
            UpdateCursor();
        }

        public DrawUILayer()
        {
            this.canvas = new Canvas();
            this.pathGeometry = new PathGeometry();
            this.canvas.MouseLeftButtonDown += this.Canvas_MouseLeftButtonDown;
            this.canvas.MouseLeftButtonUp += this.Canvas_MouseLeftButtonUp;
        }

        public Canvas UIElement
        {
            get
            {
                return this.canvas;
            }
        }

        public string Name
        {
            get
            {
                return LayerName;
            }
        }

        public static bool IsDrawModeEnabled
        {
            get; set;
        }

        public void Initialize(UILayerInitializeContext context)
        {
            this.context = context;

            double width = PageLayoutHelper.GetActualWidth(context.Page);
            double height = PageLayoutHelper.GetActualHeight(context.Page);

            this.canvas.Width = width;
            this.canvas.Height = height;
            this.canvas.Background = System.Windows.Media.Brushes.Transparent;
        }

        public void Update(UILayerUpdateContext context)
        {
        }

        public void Clear()
        {
            this.pathGeometry = new PathGeometry();
            this.figure = new PathFigure();
            this.mouseLocation = new Point();
        }

        internal static void UpdateCursor()
        {
            if (IsDrawModeEnabled)
            {
                Mouse.OverrideCursor = Cursors.Pen;
            }
            else
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.canvas.MouseMove += this.Canvas_MouseMove;
            if (IsDrawModeEnabled)
            {
                e.Handled = true;

                this.mouseLocation = e.GetPosition(this.canvas);

                this.figure = this.pathGeometry.Figures.AddPathFigure();
                this.figure.StartPoint = this.mouseLocation;
                this.figure.IsClosed = false;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentLocation = e.GetPosition(this.canvas);
            this.figure.Segments.AddLineSegment(currentLocation);
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.canvas.MouseMove -= this.Canvas_MouseMove;

            e.Handled = true;

            RadFixedPage page = this.context.Page;

            FixedContentEditor editor = new FixedContentEditor(page);
            editor.GraphicProperties.IsFilled = false;
            editor.GraphicProperties.StrokeColor = new RgbColor(255, 0, 0);
            editor.DrawPath(this.pathGeometry);

            RadPdfViewer viewer = this.context.Presenter.Owner as RadPdfViewer;
            if (viewer != null)
            {
                viewer.InvalidatePageUI(page);
            }
        }
    }
}