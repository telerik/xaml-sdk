using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Documents.Fixed.Layout;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Text;
using Telerik.Windows.Documents.Fixed.UI.Layers;
using Telerik.Windows.Documents.UI;

namespace CustomPresenter
{
    public class CustomSinglePagePresenter : FixedDocumentPresenterBase
    {
        private readonly SinglePageLayoutManager pagesLayoutManager; 

        private SinglePageInfo visiblePage; 
        private SinglePageInfo currentPageLayoutInfo; 
        private double lastViewportHeight = 0;
        private double lastVerticalScrollOffset = 0; 
        private bool scaleFactorChanged = false; 
          
        public CustomSinglePagePresenter()
        {  
            this.pagesLayoutManager = new SinglePageLayoutManager(this);
        }
          
        protected override int CurrentPageNo
        {
            get
            {
                if (this.CurrentSinglePageInfo != null)
                {
                    return this.CurrentSinglePageInfo.FixedPage.PageNo;
                }

                return 0;
            }
        }
         
        protected override PagesLayoutManagerBase PagesLayoutManager
        {
            get { return this.pagesLayoutManager; }
        }

        private SinglePageInfo CurrentSinglePageInfo
        {
            get
            {
                return this.currentPageLayoutInfo;
            }
            set
            {
                if (this.currentPageLayoutInfo != value)
                {
                    this.currentPageLayoutInfo = value;
                    if (this.currentPageLayoutInfo != null && this.CurrentPage != this.currentPageLayoutInfo.FixedPage)
                    {
                        this.CurrentPage = this.currentPageLayoutInfo.FixedPage;
                        this.OnCurrentPageChanged(this.CurrentPage);
                    }
                }
            }
        } 
           
        public override bool GetLocationFromViewPoint(Point positionInView, out RadFixedPage page, out Point location)
        {
            return this.pagesLayoutManager.GetLocationFromViewPoint(positionInView, this.ViewportSize, out page, out location);
        }  

        protected override void UpdateScrollOffsetFromPosition(TextPosition position)
        {
        }
           
        protected override void DoOnScaleFactorChangedOverride(double oldScaleFactor, double newScaleFactor)
        {
            base.DoOnScaleFactorChangedOverride(oldScaleFactor, newScaleFactor);

            this.scaleFactorChanged = true; 
        }
         
        protected override Size MeasureOverride(Size constraint)
        {
            this.pagesLayoutManager.UpdateLayout(constraint);

            double minWidth = Math.Min(constraint.Width, this.pagesLayoutManager.ContentSize.Width);
            double minHeight = Math.Min(constraint.Height, this.pagesLayoutManager.ContentSize.Height);

            Size size = new Size(minWidth, minHeight);

            base.MeasureOverride(constraint);

            return size;
        }
         
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.UpdateScrollBars(arrangeBounds);

            this.ViewportSize = arrangeBounds;

            double verticalScrollDirection = this.Owner.VerticalScrollOffset - lastVerticalScrollOffset; 
            bool viewportResized = (this.ViewportSize.Height - lastViewportHeight) != 0;
            bool useCurrentPageInfo = this.scaleFactorChanged || viewportResized;

            Point scrollOffset = new Point(this.Owner.HorizontalScrollOffset, this.Owner.VerticalScrollOffset);
            this.visiblePage = this.pagesLayoutManager.GetPageInView(
                new Rect(scrollOffset, arrangeBounds),
                verticalScrollDirection,
                this.visiblePage,
                useCurrentPageInfo);


            if (useCurrentPageInfo)
            {
                this.scaleFactorChanged = false;
                if (this.visiblePage != null)
                { 
                    if (this.visiblePage.IntersectsWithViewport)
                    {
                        if (this.visiblePage.BottomPositionInView + this.visiblePage.VerticalOffset < this.Owner.VerticalScrollOffset + ViewportSize.Height) 
                        {
                            this.Owner.ScrollToVerticalOffset(this.visiblePage.BottomPositionInView + this.visiblePage.VerticalOffset - this.ViewportSize.Height);
                        }
                        else if (this.visiblePage.TopPositionInView - this.visiblePage.VerticalOffset > this.Owner.VerticalScrollOffset) 
                        {
                            this.Owner.ScrollToVerticalOffset(this.visiblePage.TopPositionInView - this.visiblePage.VerticalOffset);
                        }
                    }
                    else
                    {
                        this.Owner.ScrollToVerticalOffset(this.visiblePage.TopPositionInView - this.visiblePage.VerticalOffset);
                    }
                }
            } 

            this.lastViewportHeight = ViewportSize.Height;
            this.lastVerticalScrollOffset = this.Owner.VerticalScrollOffset;

            this.VisiblePages = this.visiblePage == null ? Enumerable.Empty<FixedPageLayoutInfo>() : new FixedPageLayoutInfo[] { this.visiblePage };
            this.UpdateVisiblePage(this.visiblePage);

            return base.ArrangeOverride(arrangeBounds);
        }
         
        protected override void ReleaseDocumentResourcesOverride()
        {
            this.CurrentSinglePageInfo = null;
            this.visiblePage = null;
            this.lastVerticalScrollOffset = 0;
            this.lastViewportHeight = 0;
            this.scaleFactorChanged = false;
        }
              
        private void UpdateVisiblePage(SinglePageInfo visiblePage)
        {
            if (visiblePage == null)
            {
                return;
            }

            this.CurrentSinglePageInfo = visiblePage;
            Point scrollOffset = new Point(this.Owner.HorizontalScrollOffset, this.Owner.VerticalScrollOffset);;

            List<int> remove = new List<int>();
            foreach (int pageNo in this.VisiblePresenters.Keys)
            {
                if (visiblePage.FixedPage.PageNo != pageNo)
                {
                    remove.Add(pageNo);
                }
            }

            FixedPagePresenter presenter;
            foreach (int pageNo in remove)
            {
                this.ReleasePagePresenter(pageNo);
            }

            SinglePageInfo pageInfo = visiblePage;

            presenter = this.GetPagePresenter(pageInfo);

            if (pageInfo.Transformations != null)
            {
                presenter.RenderTransform = (Transform)pageInfo.Transformations;
            }

            Point topLeft;

            if (visiblePage.IntersectsWithViewport)
            {
                double y = Math.Min(scrollOffset.Y, visiblePage.BottomPositionInView - ViewportSize.Height);
                y = Math.Max(y, visiblePage.TopPositionInView); 

                topLeft = new Point(scrollOffset.X, y);
            }
            else
            {
                topLeft = new Point(visiblePage.PositionInView.Left, visiblePage.PositionInView.Top);
            }

            Rect viewport = new Rect(topLeft, this.ViewportSize);
            Rect viewportIntersectionRect = pageInfo.GetViewportIntersectionRect(viewport);
            presenter.UpdateLayers(new UILayerUpdateContext(viewportIntersectionRect)
            {
                ShouldShowSelectionMarkers = this.ShouldShowSelectionMarkers
            });

            this.UpdateCanvas(presenter, pageInfo, scrollOffset, viewport);
        }

        private void UpdateCanvas(FixedPagePresenter presenter, SinglePageInfo pageInfo, Point scrollOffset, Rect viewport)
        { 
            Canvas.SetLeft(presenter, pageInfo.PositionInView.X - scrollOffset.X);

            double offset = pageInfo.PositionInView.Y - scrollOffset.Y;
            if (offset > pageInfo.VerticalOffset || (offset > 0 && offset < pageInfo.VerticalOffset))
            {
                Canvas.SetTop(presenter, pageInfo.VerticalOffset);
                return;
            }
            else if (offset < 0)
            {
                double totalheight = pageInfo.BottomPositionInView - pageInfo.TopPositionInView;
                double heightOutside = -offset;
                double heightInside = totalheight - heightOutside;
                if (heightInside < viewport.Height)
                {
                    Canvas.SetTop(presenter, viewport.Height - totalheight);
                    return;
                }
            }

            Canvas.SetTop(presenter, offset);
        } 
    }
}
