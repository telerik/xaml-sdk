using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Internal;
using Telerik.Windows.Rendering;

namespace HighlightedAndSelectableDependencies
{
    public class CustomRelationContainer : Control, IDataContainer, ISupportsStates
    {
        public static readonly DependencyProperty ArrowThicknessProperty =
            DependencyProperty.Register("ArrowThickness", typeof(double), typeof(CustomRelationContainer), new PropertyMetadata(1d));

        public static readonly DependencyProperty SummaryContainerMarginProperty =
            DependencyProperty.Register("SummaryContainerMargin", typeof(Thickness), typeof(CustomRelationContainer), null);

        public static readonly DependencyProperty MilestoneContainerMarginProperty =
            DependencyProperty.Register("MilestoneContainerMargin", typeof(Thickness), typeof(CustomRelationContainer), null);

        public static readonly DependencyProperty MinEdgeLengthProperty =
            DependencyProperty.Register("MinEdgeLength", typeof(double), typeof(CustomRelationContainer), new PropertyMetadata(10d, OnMinEdgeLengthPropertyChanged));

        public static readonly DependencyProperty HighlightColorProperty =
            DependencyProperty.Register("HighlightColor", typeof(Brush), typeof(CustomRelationContainer));

        private object dataItem;
        private CustomRelationInfo relationInfo;
        private FrameworkElement endArrow;
        private string currentCustomState = "NotHighlighted";
        private bool areTooClose = false;
        private ArrowProxy proxy;

        public CustomRelationContainer()
        {
            this.DefaultStyleKey = typeof(CustomRelationContainer);
            this.DataContext = this.proxy = new ArrowProxy();
        }

        public double ArrowThickness
        {
            get { return (double)this.GetValue(ArrowThicknessProperty); }
            set { this.SetValue(ArrowThicknessProperty, value); }
        }

        public Thickness SummaryContainerMargin
        {
            get { return (Thickness)this.GetValue(SummaryContainerMarginProperty); }
            set { this.SetValue(SummaryContainerMarginProperty, value); }
        }

        public Thickness MilestoneContainerMargin
        {
            get { return (Thickness)this.GetValue(MilestoneContainerMarginProperty); }
            set { this.SetValue(MilestoneContainerMarginProperty, value); }
        }

        public double MinEdgeLength
        {
            get { return (double)this.GetValue(MinEdgeLengthProperty); }
            set { this.SetValue(MinEdgeLengthProperty, value); }
        }

        public Brush HighlightColor
        {
            get { return (Brush)this.GetValue(HighlightColorProperty); }
            set { this.SetValue(HighlightColorProperty, value); }
        }


        public object DataItem
        {
            get
            {
                return this.dataItem;
            }
            set
            {
                if (this.dataItem != value)
                {
                    this.UnhookEvents();
                    this.dataItem = value;
                    this.relationInfo = value as CustomRelationInfo;
                    if (this.relationInfo != null)
                    {
                        this.HookEvents();
                        this.UpdateMargin();
                        this.UpdateEndArrow();
                        this.HighlightColor = relationInfo.Foreground;
                        this.RefreshSelection();
                    }
                }
            }
        }

        private void RefreshSelection()
        {
            this.ArrowThickness = (this.relationInfo.Dependency as CustomDependency).IsSelected ? 3 : 1;
        }

        public bool AreTooClose
        {
            get
            {
                return this.areTooClose;
            }
            set
            {
                if (this.areTooClose != value)
                {
                    this.areTooClose = value;
                    this.UpdateMargin();
                    this.UpdateEndArrow();
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.endArrow = this.GetTemplateChild("EndArrowHost") as FrameworkElement;

            this.ChangeVisualState(false);

            this.UpdateEndArrow();
        }

        void ISupportsStates.GoToState(string stateName)
        {
            this.currentCustomState = stateName;
            this.ChangeVisualState(true);
        }

        protected virtual void ChangeVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, this.IsMouseOver ? "MouseOver" : "Normal", useTransitions);
            VisualStateManager.GoToState(this, this.currentCustomState, useTransitions);
        }


        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
            this.ChangeVisualState(true);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            this.ChangeVisualState(true);
            base.OnMouseLeave(e);
        }


        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.relationInfo != null)
            {
                var arrowThickness = this.ArrowThickness;
                var minLength = this.MinEdgeLength;

                this.AreTooClose = availableSize.Width < minLength * 6;

                this.proxy.PathData = relationInfo.IsSameSide ?
                    GetSameSidePathData(relationInfo.IsTimeReversed == relationInfo.AreGroupsReversed, relationInfo.IsArrowReversed, availableSize, minLength, arrowThickness) :
                    GetDifferentSidesPathData(relationInfo.IsTimeReversed, this.areTooClose, relationInfo.AreGroupsReversed, availableSize, minLength, arrowThickness);
            }

            return base.MeasureOverride(availableSize);
        }

        protected virtual Thickness GetVisualsMargin(IDateRange from, IDateRange to, bool isTimeReversed, bool isArrowReversed, bool isSameSide)
        {
            var milestonMargin = this.MilestoneContainerMargin;
            var summaryMargin = this.SummaryContainerMargin;

            var isToMilestone = IsMilestone(to);
            var isToSummary = !isToMilestone && IsSummary(to);

            var isFromMilestone = IsMilestone(from);
            var isFromSummary = !isToMilestone && IsSummary(from);

            if (isArrowReversed)
            {
                // TODO: Make this calculation consistent with the EventDecorationContainer.
                var correctionFrom = isFromMilestone ? milestonMargin.Left : isFromSummary ? summaryMargin.Left : 0d;
                var correctionTo = isToMilestone ? milestonMargin.Right : isToSummary ? summaryMargin.Right : 0d;
                if (isSameSide)
                {
                    correctionFrom = -correctionFrom;
                }

                if (isTimeReversed != isSameSide)
                {
                    return new Thickness(-correctionFrom, 0d, -correctionTo, 0d);
                }
                else
                {
                    return new Thickness(correctionTo, 0d, correctionFrom, 0d);
                }
            }
            else
            {
                var correctionFrom = isFromMilestone ? milestonMargin.Right + 2 : isFromSummary ? summaryMargin.Right : 0d;
                var correctionTo = isToMilestone ? milestonMargin.Left + 2 : isToSummary ? summaryMargin.Left : 0d;
                if (isSameSide)
                {
                    correctionFrom = -correctionFrom;
                }

                if (isTimeReversed)
                {
                    return new Thickness(-correctionTo, 0d, -correctionFrom, 0d);
                }
                else
                {
                    return new Thickness(correctionFrom, 0d, correctionTo, 0d);
                }
            }
        }

        private static bool IsMilestone(object originalEvent)
        {
            var milestone = originalEvent as IMilestone;
            return milestone != null && milestone.IsMilestone;
        }

        private static bool IsSummary(object originalEvent)
        {
            var milestone = originalEvent as ISummary;
            return milestone != null && milestone.IsSummary;
        }

        private static string GetSameSidePathData(bool isUpperShort, bool isReversed, Size areaSize, double minHorizontalLine, double arrowThickness)
        {
            var startX = isReversed ? areaSize.Width - (minHorizontalLine * 2) - (arrowThickness / 2) : (minHorizontalLine * 2) + (arrowThickness / 2);
            var startY = isUpperShort != isReversed ? arrowThickness / 2 : areaSize.Height - (arrowThickness / 2);

            var horizontalDirection = isReversed ? 1 : -1;
            var verticalDirection = isUpperShort != isReversed ? 1 : -1;

            return GetPathData(startX, startY, horizontalDirection * minHorizontalLine, verticalDirection * (areaSize.Height - arrowThickness), 0, 0, horizontalDirection * (arrowThickness + (minHorizontalLine * 3) - areaSize.Width));
        }

        private static string GetDifferentSidesPathData(bool isHorizontallyReversed, bool areTooClose, bool isVerticallyRevesed, Size areaSize, double minHorizontalLine, double arrowThickness)
        {
            if (isHorizontallyReversed)
            {
                var middleY = Math.Ceiling(areaSize.Height / 2);
                var startX = isVerticallyRevesed ? (minHorizontalLine * 2) + (arrowThickness / 2) : areaSize.Width - (minHorizontalLine * 2) - (arrowThickness / 2);
                var horizontalDirection = isVerticallyRevesed ? -1 : 1;

                return GetPathData(startX, arrowThickness / 2, horizontalDirection * minHorizontalLine, middleY - arrowThickness, horizontalDirection * (arrowThickness - areaSize.Width + (2 * minHorizontalLine)), areaSize.Height - middleY, horizontalDirection * minHorizontalLine);
            }
            else if (areTooClose)
            {
                var middleY = Math.Ceiling(areaSize.Height / 2);
                var startX = isVerticallyRevesed ? areaSize.Width - (minHorizontalLine * 2) - (arrowThickness / 2) : (minHorizontalLine * 2) + (arrowThickness / 2);
                var horizontalDirection = isVerticallyRevesed ? -1 : 1;

                return GetPathData(startX, arrowThickness / 2, horizontalDirection * (minHorizontalLine - arrowThickness), middleY - arrowThickness, horizontalDirection * (areaSize.Width - (6 * minHorizontalLine) + arrowThickness), areaSize.Height - middleY, horizontalDirection * (minHorizontalLine - arrowThickness));
            }
            else
            {
                var middleX = Math.Round((areaSize.Width / 2) - (minHorizontalLine * 2), 0);
                var startY = isVerticallyRevesed ? areaSize.Height - (arrowThickness / 2) : (arrowThickness / 2);
                var verticalDirection = isVerticallyRevesed ? -1 : 1;

                return GetPathData((minHorizontalLine * 2) + (arrowThickness / 2), startY, middleX, 0, 0, verticalDirection * (areaSize.Height - arrowThickness), middleX);
            }
        }

        private static string GetPathData(double startX, double startY, double middle1OffsetX, double middle2OffsetY, double middle3OffsetX, double endOffsetY, double endOffsetX)
        {
            return string.Format(CultureInfo.InvariantCulture, "F0 M{0},{1} h{2} v{3} h{4} v{5} h{6}", startX, startY, middle1OffsetX, middle2OffsetY, middle3OffsetX, endOffsetY, endOffsetX);
        }

        private static void OnMinEdgeLengthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as CustomRelationContainer;
            if (target != null)
            {
                target.UpdateMargin();
            }
        }

        private void HookEvents()
        {
            if (this.relationInfo != null)
            {
                var propertyChanged = this.relationInfo.TaskFrom as INotifyPropertyChanged;
                if (propertyChanged != null)
                {
                    propertyChanged.PropertyChanged += this.ConnectedEventsPropertyChanged;
                }

                propertyChanged = this.relationInfo.TaskTo as INotifyPropertyChanged;

                if (propertyChanged != null)
                {
                    propertyChanged.PropertyChanged += this.ConnectedEventsPropertyChanged;
                }

                var dependency = this.relationInfo.Dependency as INotifyPropertyChanged;
                if (dependency != null)
                {
                    dependency.PropertyChanged += this.ConnectedEventsPropertyChanged;
                }
            }
        }

        private void UnhookEvents()
        {
            if (this.relationInfo != null)
            {
                var propertyChanged = this.relationInfo.TaskFrom as INotifyPropertyChanged;
                if (propertyChanged != null)
                {
                    propertyChanged.PropertyChanged -= this.ConnectedEventsPropertyChanged;
                }

                propertyChanged = this.relationInfo.TaskTo as INotifyPropertyChanged;

                if (propertyChanged != null)
                {
                    propertyChanged.PropertyChanged -= this.ConnectedEventsPropertyChanged;
                }

                var dependency = this.relationInfo.Dependency as INotifyPropertyChanged;
                if (dependency != null)
                {
                    dependency.PropertyChanged -= this.ConnectedEventsPropertyChanged;
                }
            }
        }

        private void ConnectedEventsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ("IsMilestone".Equals(e.PropertyName) || "IsSummary".Equals(e.PropertyName))
            {
                this.UpdateMargin();
            }
            else if ("Start".Equals(e.PropertyName) || "End".Equals(e.PropertyName))
            {
                this.UpdateMargin();
                this.UpdateEndArrow();
            }
            else if ("IsSelected".Equals(e.PropertyName))
            {
                this.RefreshSelection();
            }
        }

        private void UpdateMargin()
        {
            if (this.relationInfo != null)
            {
                var minEdgeLength = this.MinEdgeLength;
                var margin = this.Margin;

                var marginBottom = margin.Top == margin.Bottom ? margin.Bottom - 1 : margin.Bottom;

                var correction = this.GetVisualsMargin(this.relationInfo.TaskFrom, this.relationInfo.TaskTo, this.relationInfo.IsTimeReversed, this.relationInfo.IsArrowReversed, this.relationInfo.IsSameSide);
                this.Margin = new Thickness((-2 * minEdgeLength) - correction.Left, margin.Top, (-2 * minEdgeLength) - correction.Right, marginBottom);
            }
        }

        private void UpdateEndArrow()
        {
            if (this.endArrow != null && this.relationInfo != null)
            {
                this.endArrow.RenderTransform = relationInfo.IsArrowReversed ? new RotateTransform { Angle = 180 } : new RotateTransform { Angle = 0 };
                
                if (relationInfo.Dependency.Type == DependencyType.StartFinish)
                {
                    this.endArrow.VerticalAlignment = !relationInfo.AreGroupsReversed ? VerticalAlignment.Top : VerticalAlignment.Bottom;
                    this.endArrow.HorizontalAlignment = !relationInfo.IsTimeReversed ? HorizontalAlignment.Left : HorizontalAlignment.Right;
                }
                else
                {
                    this.endArrow.VerticalAlignment = relationInfo.AreGroupsReversed ? VerticalAlignment.Top : VerticalAlignment.Bottom;
                    this.endArrow.HorizontalAlignment = relationInfo.IsTimeReversed ? HorizontalAlignment.Left : HorizontalAlignment.Right;
                }

                var oldMargin = this.endArrow.Margin;
                if (this.endArrow.HorizontalAlignment == HorizontalAlignment.Left)
                {
                    this.endArrow.Margin = new Thickness((2 * this.MinEdgeLength) - this.endArrow.Width, oldMargin.Top, 0, oldMargin.Bottom);
                }
                else
                {
                    this.endArrow.Margin = new Thickness(0, oldMargin.Top, 2 * this.MinEdgeLength, oldMargin.Bottom);
                }
            }
        }

        public class ArrowProxy : Telerik.Windows.Core.PropertyChangedBase
        {
            private string pathData = string.Empty;

            /// <summary>
            /// Gets or sets PathData and notifies for changes.
            /// </summary>
            public string PathData
            {
                get
                {
                    return this.pathData;
                }
                set
                {
                    if (this.pathData != value)
                    {
                        this.pathData = value;
                        this.OnPropertyChanged("PathData");
                    }
                }
            }
        }
    }
}
