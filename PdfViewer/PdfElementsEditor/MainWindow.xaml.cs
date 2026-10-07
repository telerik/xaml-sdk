using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Documents.Fixed.Model;
using Telerik.Documents.Fixed.Model.Common;
using Telerik.Documents.Fixed.Model.Graphics;
using Telerik.Documents.Fixed.Model.Text;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;
using Telerik.Windows.Documents.Fixed.UI.Layers;

namespace PdfElementsEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<PageModel> pageModel = new ObservableCollection<PageModel>();
        CustomUILayersBuilder uILayersBuilder = new CustomUILayersBuilder();

        public MainWindow()
        {
            InitializeComponent();
            ExtensibilityManager.RegisterLayersBuilder(uILayersBuilder);

            pdfViewer.DocumentChanged += PdfViewer_DocumentChanged;
            this.pdfViewer.Document = new PdfFormatProvider().Import(System.IO.File.ReadAllBytes(@"..\..\..\samplepdf.pdf"), null);
            this.radTreeView.SelectionChanged += RadTreeView_SelectionChanged;

        }

        private static readonly System.Reflection.MethodInfo GetBoundsMethod =
            typeof(ContentElementBase).GetMethod(
                "GetBounds",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        private void RadTreeView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = this.radTreeView.SelectedItem as ElementModel;
            if (item == null)
            {
                // A page node (or nothing) is selected - remove any existing highlight.
                foreach (var layer in this.uILayersBuilder.MyLayers)
                {
                    layer.Clear();
                }

                return;
            }

            var document = this.pdfViewer.Document;
            if (document == null || item.pageIndex < 1 || item.pageIndex > document.Pages.Count)
            {
                return;
            }

            RadFixedPage targetPage = document.Pages[item.pageIndex - 1];

            System.Windows.Rect bounds = GetElementBounds(item.Element);
            if (bounds.IsEmpty)
            {
                return;
            }

            // Draw the highlight only on the layer that belongs to the element's page and
            // clear the highlight on every other page.
            foreach (var layer in this.uILayersBuilder.MyLayers)
            {
                if (layer.Page == targetPage)
                {
                    layer.UpdateHighlight(bounds);
                }
                else
                {
                    layer.Clear();
                }
            }

            this.EnsureElementIsVisible(targetPage, bounds, item.pageIndex);
        }

        /// <summary>
        /// Scrolls the viewer so that the given page bounds become visible.
        /// If the element is already inside the viewport, no scrolling is performed.
        /// </summary>
        private void EnsureElementIsVisible(RadFixedPage page, System.Windows.Rect pageBounds, int pageIndex, bool allowRetry = true)
        {
            // Make sure the presenter is measured/arranged so that the viewport size, the scroll
            // offsets and the page layout infos used below are up to date.
            this.pdfViewer.UpdateLayout();

            Telerik.Windows.Documents.UI.FixedDocumentPresenterBase presenter =
                this.pdfViewer.FixedDocumentPresenter as Telerik.Windows.Documents.UI.FixedDocumentPresenterBase;
            if (presenter == null)
            {
                this.pdfViewer.GoToPage(pageIndex);

                return;
            }

            System.Windows.Point topLeftInView;
            System.Windows.Point bottomRightInView;
            if (!presenter.GetViewPointFromLocation(page, pageBounds.TopLeft, out topLeftInView) ||
                !presenter.GetViewPointFromLocation(page, pageBounds.BottomRight, out bottomRightInView))
            {
                // The page is not part of the current layout yet - bring it into view and
                // retry once after the layout is updated.
                this.pdfViewer.GoToPage(pageIndex);

                if (allowRetry)
                {
                    this.Dispatcher.BeginInvoke(
                        new Action(() => this.EnsureElementIsVisible(page, pageBounds, pageIndex, false)),
                        System.Windows.Threading.DispatcherPriority.Loaded);
                }

                return;
            }

            // The real viewport is the presenter's arranged size - the viewer's ActualHeight/Width
            // also includes the scroll bars and the control border, which makes elements that are
            // slightly outside the viewport look visible and suppresses the scrolling.
            FrameworkElement presenterElement = presenter as FrameworkElement;
            double viewportWidth = presenterElement != null && presenterElement.ActualWidth > 0
                ? presenterElement.ActualWidth
                : this.pdfViewer.ActualWidth;
            double viewportHeight = presenterElement != null && presenterElement.ActualHeight > 0
                ? presenterElement.ActualHeight
                : this.pdfViewer.ActualHeight;

            // Leave a small margin so the element is not glued to the viewport edge.
            const double Margin = 20;

            if (bottomRightInView.Y > viewportHeight || topLeftInView.Y < 0)
            {
                double delta = topLeftInView.Y < 0
                    ? topLeftInView.Y - Margin
                    : Math.Min(topLeftInView.Y - Margin, bottomRightInView.Y - viewportHeight + Margin);

                this.pdfViewer.ScrollToVerticalOffset(Math.Max(this.pdfViewer.VerticalScrollOffset + delta, 0));
            }

            if (bottomRightInView.X > viewportWidth || topLeftInView.X < 0)
            {
                double delta = topLeftInView.X < 0
                    ? topLeftInView.X - Margin
                    : Math.Min(topLeftInView.X - Margin, bottomRightInView.X - viewportWidth + Margin);

                this.pdfViewer.ScrollToHorizontalOffset(Math.Max(this.pdfViewer.HorizontalScrollOffset + delta, 0));
            }
        }

        /// <summary>
        /// Calculates the bounds of a content element in the page content coordinate system,
        /// exactly the way the viewer renders the element:
        /// - every element is drawn under its <see cref="PositionContentElement.Position"/> matrix,
        ///   so the element geometry has to be expressed in the element's own (local) space and
        ///   then transformed with that matrix;
        /// - a path is drawn from its geometry, which is already in local space;
        /// - an image is drawn in the (0, 0, Width, Height) local rectangle;
        /// - a text fragment is drawn on its baseline, i.e. the glyphs are placed above the
        ///   position point - the local rectangle starts at -FontSize and the measured line height
        ///   covers the descenders.
        /// The internal GetBounds() cannot be used directly here - for paths it returns the
        /// untransformed geometry bounds and for text fragments it returns a rectangle that starts
        /// at the baseline (which places the highlight right below the glyphs).
        /// </summary>
        private static System.Windows.Rect GetElementBounds(ContentElementBase element)
        {
            Telerik.Documents.Fixed.Model.Graphics.Path path = element as Telerik.Documents.Fixed.Model.Graphics.Path;
            if (path != null)
            {
                if (path.Geometry == null)
                {
                    return System.Windows.Rect.Empty;
                }

                return TransformLocalBounds(path.Geometry.Bounds, path.Position.Matrix);
            }

            Telerik.Documents.Fixed.Model.Objects.Image image = element as Telerik.Documents.Fixed.Model.Objects.Image;
            if (image != null)
            {
                return TransformLocalBounds(new System.Windows.Rect(0, 0, image.Width, image.Height), image.Position.Matrix);
            }

            TextFragment fragment = element as TextFragment;
            if (fragment != null)
            {
                System.Windows.Rect measured = (System.Windows.Rect)GetBoundsMethod.Invoke(fragment, null);
                double height = Math.Max(measured.Height, fragment.FontSize);

                return TransformLocalBounds(
                    new System.Windows.Rect(0, -fragment.FontSize, measured.Width, height),
                    fragment.Position.Matrix);
            }

            return (System.Windows.Rect)GetBoundsMethod.Invoke(element, null);
        }

        private static System.Windows.Rect TransformLocalBounds(System.Windows.Rect localBounds, Matrix positionMatrix)
        {
            if (localBounds.IsEmpty)
            {
                return System.Windows.Rect.Empty;
            }

            return System.Windows.Rect.Transform(localBounds, positionMatrix);
        }

        private async void PdfViewer_DocumentChanged(object sender, Telerik.Windows.Documents.Fixed.DocumentChangedEventArgs e)
        {
            pdfViewer.ScaleFactor = 1;

            // Show the busy indicator until the tree view model has been built.
            this.treeBusyIndicator.IsBusy = true;
            this.radTreeView.ItemsSource = null;
            this.pageModel.Clear();

            try
            {
                // When a document is opened through the viewer (toolbar), DocumentChanged
                // fires before the pages' Content has been populated, so building the model
                // immediately finds empty pages (only page nodes, no child elements). Yielding
                // at Background priority lets the viewer finish loading the page content and
                // also gives the busy indicator a chance to render.
                await System.Windows.Threading.Dispatcher.Yield(System.Windows.Threading.DispatcherPriority.Background);

                RadFixedDocument document = this.pdfViewer.Document;
                if (document == null)
                {
                    return;
                }

                // The model is a plain POCO graph - building it off the UI thread keeps the
                // window (and the busy indicator animation) responsive for large documents.
                List<PageModel> pages = await Task.Run(() => this.BuildModel(document));

                foreach (PageModel page in pages)
                {
                    this.pageModel.Add(page);
                }

                this.radTreeView.ItemsSource = this.pageModel;

                // Let the tree view generate and render its containers before the
                // indicator is hidden. The items are expanded through the item container
                // style - calling ExpandAll() here would realize every container at once
                // and block the UI thread (freezing the indicator animation).
                await System.Windows.Threading.Dispatcher.Yield(System.Windows.Threading.DispatcherPriority.ContextIdle);
            }
            finally
            {
                this.treeBusyIndicator.IsBusy = false;
            }
        }

        private List<PageModel> BuildModel(RadFixedDocument document)
        {
            List<PageModel> pages = new List<PageModel>();
            int ID = 0;

            for (int i = 0; i < document.Pages.Count; i++)
            {
                int pageIndex = i + 1;
                PageModel model = new PageModel(pageIndex);

                this.AddElements(document.Pages[i].Content, model, pageIndex, ref ID);

                pages.Add(model);
            }

            return pages;
        }

        private void AddElements(IEnumerable<ContentElementBase> content, PageModel model, int pageIndex, ref int ID)
        {
            foreach (var item in content)
            {
                if (item is Telerik.Documents.Fixed.Model.Objects.Form form)
                {
                    if (form.FormSource != null)
                    {
                        this.AddElements(form.FormSource.Content, model, pageIndex, ref ID);
                    }

                    continue;
                }

                if (this.TryGetNestedContent(item, out IEnumerable<ContentElementBase> nestedContent))
                {
                    this.AddElements(nestedContent, model, pageIndex, ref ID);
                    continue;
                }

                string text = this.GetDisplayText(item);
                model.Elements.Add(new ElementModel(ID++, text, item, pageIndex));
            }
        }

        private string GetDisplayText(ContentElementBase item)
        {
            if (item is TextFragment textFragment)
            {
                string text = textFragment.Text;
                if (!string.IsNullOrWhiteSpace(text))
                {
                    return text;
                }
            }

            return item.GetType().Name;
        }

        private bool TryGetNestedContent(ContentElementBase item, out IEnumerable<ContentElementBase> nestedContent)
        {
            nestedContent = null;

            var contentProperty = item.GetType().GetProperty("Content", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (contentProperty == null)
            {
                return false;
            }

            if (contentProperty.GetValue(item) is IEnumerable<ContentElementBase> childContent)
            {
                nestedContent = childContent;
                return true;
            }

            return false;
        }

        private void RadContextMenu_Loaded(object sender, RoutedEventArgs e)
        {
            var menu = sender as RadContextMenu;
            foreach (RadMenuItem item in menu.Items)
            {
                item.CommandParameter = pdfViewer;
            }

            // The menu is a shared resource declared in Window.Resources, so it does not inherit
            // the DataContext of the item it is opened for. Without this the Command bindings
            // resolve to null and the menu items do nothing.
            menu.Opened -= this.RadContextMenu_Opened;
            menu.Opened += this.RadContextMenu_Opened;
        }

        private void RadContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var menu = (RadContextMenu)sender;
            var clickedElement = menu.GetClickedElement<FrameworkElement>();
            menu.DataContext = clickedElement != null ? clickedElement.DataContext : null;
        }
    }
}
