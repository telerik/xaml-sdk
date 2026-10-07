using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Documents.Fixed.Model.ColorSpaces;
using Telerik.Documents.Fixed.Model.Editing;
using Telerik.Documents.Fixed.Model;
using Telerik.Documents.Fixed.Model.Data;
using Telerik.Windows.Documents.Fixed.UI.Layers;
using Telerik.Documents.Fixed.Utilities.Rendering;
using System.Windows.Media; 
using System.Windows;
using System.Drawing; 

namespace PdfElementsEditor
{
    public class HighlightElementLayer : IUILayer
    {
        private static readonly string LayerName = "HighlightElementLayer";
        private readonly Canvas canvas;

        private UILayerInitializeContext context;

     
        public HighlightElementLayer(int count)
        {
            this.canvas = new Canvas();
            this.canvas.Name = "MyLayer" + count;
          
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
            if (this.canvas.Children.Count == 0)
            {
                return;
            }

            this.canvas.Children.Clear();
        }

        /// <summary>
        /// The fixed page this layer instance is bound to. Used to highlight only the
        /// page that owns the selected element.
        /// </summary>
        public RadFixedPage Page
        {
            get { return this.context?.Page; }
        }

        /// <summary>
        /// Highlights the supplied bounds. <paramref name="pageBounds"/> must be expressed
        /// in the fixed page's own coordinate space (top-left origin, DIP units) - i.e. the
        /// value returned by a content element's bounds. The bounds are mapped to the
        /// layer's canvas (which is drawn unzoomed; the viewer applies the zoom transform).
        /// </summary>
        public void UpdateHighlight(System.Windows.Rect pageBounds)
        {
            this.canvas.Children.Clear();

            if (this.context == null || pageBounds.IsEmpty)
            {
                return;
            }

            System.Windows.Rect rect = this.TransformToCanvas(pageBounds);

            var myRect = new System.Windows.Shapes.Rectangle();
            myRect.StrokeThickness = 1;
            myRect.Stroke = System.Windows.Media.Brushes.DeepPink;
            myRect.Fill = new System.Windows.Media.SolidColorBrush(
                System.Windows.Media.Color.FromArgb(48, 255, 20, 147));
            myRect.Width = rect.Width;
            myRect.Height = rect.Height;

            Canvas.SetLeft(myRect, rect.X);
            Canvas.SetTop(myRect, rect.Y);

            this.canvas.Children.Add(myRect);
        }

        /// <summary>
        /// Maps a rectangle from fixed page content coordinates to this layer canvas coordinates.
        /// The viewer does not render the whole page - it renders the page's visible content box
        /// (the CropBox intersected with the MediaBox) at the canvas origin and then applies the
        /// page's own /Rotate entry. The canvas size is <see cref="PageLayoutHelper.GetActualWidth"/> /
        /// <see cref="PageLayoutHelper.GetActualHeight"/>, which are the rotated visible content box
        /// dimensions. Content element bounds, on the other hand, are expressed in the unrotated page
        /// content space, so both the visible content box offset and the page rotation have to be
        /// applied here. The zoom is applied by the viewer on the whole presenter, so it is not
        /// applied here.
        /// </summary>
        private System.Windows.Rect TransformToCanvas(System.Windows.Rect pageBounds)
        {
            RadFixedPage page = this.context.Page;
            System.Windows.Rect visibleContentBox = PageLayoutHelper.GetVisibleContentBox(page);

            Matrix matrix = Matrix.Identity;
            matrix.Translate(-visibleContentBox.X, -visibleContentBox.Y);

            switch (page.Rotation)
            {
                case Rotation.Rotate90:
                    matrix.Rotate(90);
                    matrix.Translate(visibleContentBox.Height, 0);
                    break;
                case Rotation.Rotate180:
                    matrix.Rotate(180);
                    matrix.Translate(visibleContentBox.Width, visibleContentBox.Height);
                    break;
                case Rotation.Rotate270:
                    matrix.Rotate(270);
                    matrix.Translate(0, visibleContentBox.Width);
                    break;
            }

            return System.Windows.Rect.Transform(pageBounds, matrix);
        }
    }
}

