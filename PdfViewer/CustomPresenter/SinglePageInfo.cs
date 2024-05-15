using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Documents.Fixed.Layout;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.UI.Layers;
using Telerik.Windows.Documents.UI;

namespace CustomPresenter
{
    public class SinglePageInfo : FixedPageLayoutInfo
    {
        private readonly double topOffsetInPresenter;
        private readonly double bottomPositionInView;
        private readonly bool intersectsWithViewportVertically;
        private readonly bool intersectsWithViewportHorizontally;
         
        public SinglePageInfo(double topOffsetInPresenter, double bottomPositionInView, bool intersectsHorizontally, bool intersectsVertically, RadFixedPage page, Rect positionInView, GeneralTransform transformations)
            : base(page, positionInView, transformations)
        {
            this.topOffsetInPresenter = topOffsetInPresenter;
            this.bottomPositionInView = bottomPositionInView;
            this.intersectsWithViewportVertically = intersectsVertically;
            this.intersectsWithViewportHorizontally = intersectsHorizontally;
        }
         
        public bool IntersectsWithViewportVertically
        {
            get
            {
                return this.intersectsWithViewportVertically;
            }
        }
         
        public bool IntersectsWithViewportHorizontally
        {
            get
            {
                return this.intersectsWithViewportHorizontally;
            }
        }
         
        public bool IntersectsWithViewport
        {
            get
            {
                return this.IntersectsWithViewportHorizontally || this.IntersectsWithViewportVertically;
            }
        }
         
        public double VerticalOffset
        {
            get
            {
                return this.topOffsetInPresenter;
            }
        }
         
        public double BottomPositionInView
        {
            get
            {
                return this.bottomPositionInView;
            }
        }
         
        public double TopPositionInView
        {
            get
            {
                return this.PositionInView.Y;
            }
        }
         
        public double LeftPositionInView
        {
            get
            {
                return this.PositionInView.X;
            }
        }
    }
}
