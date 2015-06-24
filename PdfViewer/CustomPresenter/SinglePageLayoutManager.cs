using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Documents.Fixed.Layout;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.UI;

namespace CustomPresenter
{
    public class SinglePageLayoutManager : PagesLayoutManagerBase
    {
        private SinglePageInfo visiblePage;
        private readonly List<SinglePageInfo> pageLayoutInfos;

        public SinglePageLayoutManager(IFixedDocumentPresenter presenter)
            : base(presenter)
        {
            this.pageLayoutInfos = new List<SinglePageInfo>();
        }

        public override void UpdateLayout(Size viewportSize)
        {
            RotationAngle rotationAngle = this.Presenter.Owner.RotationAngle;

            this.pageLayoutInfos.Clear();
            if (this.Document == null)
            {
                this.ContentSize = new Size(0, 0);
                return;
            }

            double height = 0;
            double width = 0;
            foreach (RadFixedPage page in this.Document.Pages)
            {
                Size pageSize = this.GetScaledPageSize(page);
                Size rotatedPageSize = this.RotateSize(pageSize, rotationAngle);
                Size pageSizeWithMargins = new Size(rotatedPageSize.Width + (2 * this.PageMargins.Width), rotatedPageSize.Height + (2 * this.PageMargins.Height));

                if (pageSizeWithMargins.Width > width)
                {
                    width = pageSizeWithMargins.Width;
                }

                double horizontalOffset = ((viewportSize.Width - rotatedPageSize.Width) / 2) - this.PageMargins.Width;
                double verticalOffset = ((viewportSize.Height - rotatedPageSize.Height) / 2) - this.PageMargins.Height;

                horizontalOffset = Math.Max(0, horizontalOffset);
                verticalOffset = Math.Max(0, verticalOffset);

                double newHeight = height + pageSizeWithMargins.Height + 2 * verticalOffset;

                Rect positionInView = new Rect(this.PageMargins.Width + horizontalOffset, height + this.PageMargins.Height + verticalOffset, rotatedPageSize.Width, rotatedPageSize.Height);

                TransformGroup transformations = this.CreateTransformations(pageSize, rotationAngle);

                FixedPageLayoutInfo layoutInfo = new FixedPageLayoutInfo(page, positionInView, transformations);

                SinglePageInfo singlePageInfo = new SinglePageInfo(verticalOffset + this.PageMargins.Height, newHeight,
                    rotatedPageSize.Width > viewportSize.Width,
                    rotatedPageSize.Height > viewportSize.Height,
                    layoutInfo.FixedPage, layoutInfo.PositionInView,
                    layoutInfo.Transformations);

                this.pageLayoutInfos.Add(singlePageInfo);

                height = newHeight;
            }

            this.ContentSize = new Size(width, height);
        }

        public SinglePageInfo GetPageInView(int pageNo)
        {
            return this.pageLayoutInfos.Where(info => info.FixedPage.PageNo == pageNo).FirstOrDefault();
        }

        public SinglePageInfo GetPageInView(Rect viewport, double verticalScrollDirection, SinglePageInfo currentPageInfo, bool retrieveCurrentPageInfo)
        {
            if (retrieveCurrentPageInfo && currentPageInfo != null)
            {
                this.visiblePage = this.pageLayoutInfos.Where(info => info.FixedPage.PageNo == currentPageInfo.FixedPage.PageNo).FirstOrDefault();

                return this.visiblePage;
            }

            return this.GetPageInView(viewport, verticalScrollDirection, currentPageInfo);
        }

        public bool GetLocationFromViewPoint(Point viewPoint, Size viewport, out RadFixedPage page, out Point location)
        {
            location = new Point();
            page = null;
            if (this.ContentSize.Width == 0 || this.ContentSize.Height == 0 || this.pageLayoutInfos == null || this.pageLayoutInfos.Count == 0)
            {
                return false;
            }

            SinglePageInfo pageInfo = this.visiblePage;
            if (pageInfo == null || !pageInfo.FixedPage.HasContent)
            {
                return false;
            }

            page = pageInfo.FixedPage;

            double verticalScrollOffset = this.Presenter.Owner.VerticalScrollOffset;

            if (pageInfo.IntersectsWithViewportVertically)
            {
                if (verticalScrollOffset < pageInfo.PositionInView.Y)
                {
                    viewPoint.Y += pageInfo.PositionInView.Y - verticalScrollOffset;
                }
                else if (verticalScrollOffset + viewport.Height > pageInfo.BottomPositionInView)
                {
                    viewPoint.Y -= verticalScrollOffset + viewport.Height - pageInfo.BottomPositionInView;
                }
            }
            else
            {
                if (verticalScrollOffset + pageInfo.VerticalOffset > pageInfo.PositionInView.Y)
                {
                    viewPoint.Y -= verticalScrollOffset + pageInfo.VerticalOffset - pageInfo.PositionInView.Y;
                }
                else if (verticalScrollOffset + pageInfo.VerticalOffset < pageInfo.PositionInView.Y)
                {
                    viewPoint.Y += pageInfo.PositionInView.Y - (verticalScrollOffset + pageInfo.VerticalOffset);
                }
            }

            location = new Point(viewPoint.X - pageInfo.PositionInView.X, viewPoint.Y - pageInfo.PositionInView.Y);
            if (pageInfo.InverseTransformations != null)
            {
                location = pageInfo.InverseTransformations.Transform(location);
            }

            return true;
        }

        public override void Release()
        {
            this.visiblePage = null;
        }

        protected override List<FixedPageLayoutInfo> GetPagesLayoutInfos()
        {
            List<FixedPageLayoutInfo> result = new List<FixedPageLayoutInfo>();
            foreach (var info in this.pageLayoutInfos)
            {
                result.Add(info);
            }

            return result;
        }

        private SinglePageInfo GetPageInView(Rect viewport, double verticalScrollDirection)
        {
            List<SinglePageInfo> infos = new List<SinglePageInfo>(this.pageLayoutInfos);

            if (verticalScrollDirection < 0)
            {
                infos.Reverse();
            }

            foreach (var info in infos)
            {
                if (Intersects(viewport, info.PositionInView))
                {
                    this.visiblePage = info;
                    return this.visiblePage;
                }
            }

            return this.visiblePage;
        }

        private SinglePageInfo GetPageInView(Rect viewport, double verticalScrollDirection, SinglePageInfo currentPageInfo)
        {
            //In case the scroll direction changes between two pages.
            if (currentPageInfo != null)
            {
                foreach (var info in this.pageLayoutInfos)
                {
                    if (Intersects(viewport, info.PositionInView) && info.FixedPage.PageNo == currentPageInfo.FixedPage.PageNo)
                    {
                        this.visiblePage = info;
                        return this.visiblePage;
                    }
                }
            }

            return this.GetPageInView(viewport, verticalScrollDirection);
        }

        private static bool Intersects(Rect a, Rect b)
        {
            if (a.IsEmpty || b.IsEmpty)
            {
                return false;
            }

            return b.Y < (a.Y + a.Height) &&
                    a.Y < (b.Y + b.Height) &&
                    b.X < (a.X + a.Width) &&
                    a.X < (b.X + b.Width);
        }
    }
}
