using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Timeline;
using Telerik.Windows.DragDrop;
using Telerik.Windows.Media.Imaging;

namespace DragDrop
{
    public class TimelineDragDropBehavior
    {
        public static readonly DependencyProperty DragDropBehaviorProperty =
            DependencyProperty.RegisterAttached(
                "DragDropBehavior", 
                typeof(TimelineDragDropBehavior), 
                typeof(TimelineDragDropBehavior), 
                new PropertyMetadata(null, OnDragDropBehaviorChanged));

        public static TimelineDragDropBehavior GetDragDropBehavior(DependencyObject obj)
        {
            return (TimelineDragDropBehavior)obj.GetValue(DragDropBehaviorProperty);
        }

        public static void SetDragDropBehavior(DependencyObject obj, TimelineDragDropBehavior value)
        {
            obj.SetValue(DragDropBehaviorProperty, value);
        }

        private static void OnDragDropBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                ((TimelineDragDropBehavior)e.NewValue).DetachTimeline((RadTimeline)d);
            }
                    
            if (e.NewValue != null)
            {
                ((TimelineDragDropBehavior)e.NewValue).AttachTimeline((RadTimeline)d);
            }
        }

        private RadTimeline timeline;
        private List<GroupInfo> groupControlInfos = new List<GroupInfo>();
        private ContentControl dragVisual = new ContentControl();
        private DataTemplate dragVisualTemplate;

        public DataTemplate DragVisualTemplate
        {
            get { return dragVisualTemplate; }
            set
            {
                dragVisualTemplate = value;
                if (this.dragVisual != null)
                {
                    this.dragVisual.ContentTemplate = this.dragVisualTemplate;
                }
            }
        }        

        private void AttachTimeline(RadTimeline timeline)
        {
            this.timeline = timeline;
            this.timeline.ItemRowIndexGenerator = new CustomRowIndexGenerator();
            SubscribeToDragDropEvents();
        }

        private void DetachTimeline(RadTimeline timeline)
        {
            this.timeline = null;
            this.timeline.ItemRowIndexGenerator = null;
            UnsubscribeFromDragDropEvents();
        }

        private void SubscribeToDragDropEvents()
        {
            DragDropManager.AddDragInitializeHandler(this.timeline, OnTimelineDragInitialize);
            DragDropManager.AddDragOverHandler(this.timeline, OnTimelineDragOver);
            DragDropManager.AddDropHandler(this.timeline, OnTimelineDrop);            
        }

        private void UnsubscribeFromDragDropEvents()
        {
            DragDropManager.RemoveDragInitializeHandler(this.timeline, OnTimelineDragInitialize);
            DragDropManager.RemoveDragOverHandler(this.timeline, OnTimelineDragOver);
            DragDropManager.RemoveDropHandler(this.timeline, OnTimelineDrop);
        }

        private void OnTimelineDragInitialize(object sender, DragInitializeEventArgs e)
        {
            var itemControl = e.OriginalSource as TimelineItemControlBase;            
            if (itemControl != null)
            {
                var data = (TimelineDataItem)itemControl.DataContext;
                var dataItem = (ITimelineItem)data.DataItem;
                var payload = DragDropPayloadManager.GeneratePayload(null);
                payload.SetData("DraggedItem", dataItem);

                e.AllowedEffects = DragDropEffects.Move;
                e.Data = payload;

                this.dragVisual.Content = new TimelineItemDragVisualInfo()
                {
                    ItemImageSource = new RadBitmap(itemControl).Bitmap,
                    ItemImageWidth = itemControl.ActualWidth,
                    ItemImageHeight = itemControl.ActualHeight,
                };
                e.DragVisual = this.dragVisual;

                this.groupControlInfos = GetGroupInfos(timeline);
            }
        }

        private void OnTimelineDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {            
            var position = e.GetPosition(this.timeline);
            DateTime dateTimeUnderMouse = this.timeline.ConvertPointToDateTime(position);
            var dataItem = (ITimelineItem)DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedItem");            
            var dragVisual = (TimelineItemDragVisualInfo)this.dragVisual.Content;

            var groupUnderMouse = GetGroupUnderMouse(position);
            if (groupUnderMouse != null)
            {
                var rowIndex = GetRowUnderMouse(position, groupUnderMouse);
                dragVisual.GroupKey = groupUnderMouse.Key;
                dragVisual.RowIndex = rowIndex;
            }
            else
            {
                dragVisual.GroupKey = null;
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }            
        }

        private void OnTimelineDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {            
            var position = e.GetPosition(this.timeline);
            var dataItem = (ITimelineItem)DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedItem");
            var source = (IList)this.timeline.ItemsSource;
            var itemIndex = source.IndexOf(dataItem);
            var groupUnderMouse = GetGroupUnderMouse(position);
            var rowIndex = GetRowUnderMouse(position, groupUnderMouse);

            source.Remove(dataItem);
           
            dataItem.GroupKey = groupUnderMouse.Key;
            dataItem.RowIndex = rowIndex;
            dataItem.StartDate = this.timeline.ConvertPointToDateTime(position);

            source.Insert(itemIndex, dataItem);            
        }

        private GroupInfo GetGroupUnderMouse(Point position)
        {
            return this.groupControlInfos.FirstOrDefault(x => x.Bounds.Contains(position));
        }

        private int GetRowUnderMouse(Point mousePos, GroupInfo group)
        {
            double rowHeight = group.Bounds.Height / group.RowsCount;
            var normalizedY = mousePos.Y - group.Bounds.Top;  
            return (int)(normalizedY / rowHeight);
        }

        public List<GroupInfo> GetGroupInfos(RadTimeline timeline)
        {
            var infos = new List<GroupInfo>();
            var itemGroupControls = timeline.ChildrenOfType<TimelineItemGroupControl>();
            GroupInfo previousItem = null;
            double previousSeparatorY = 0;

            foreach (var groupControl in itemGroupControls)
            {
                var groupInfo = new GroupInfo();
                groupInfo.RowsCount = groupControl.DataGroups.Max(x => x.RowsCount);
                groupInfo.Key = groupControl.Header;
                var separatorBounds = GetGroupSeparatorBounds(groupControl);

                if (previousItem != null)
                {
                    previousItem.Bounds = new Rect(0, previousSeparatorY, this.timeline.ActualWidth, separatorBounds.Y - previousSeparatorY);
                }
                if (itemGroupControls.Last() == groupControl)
                {
                    var groupBounds = BoundsRelativeTo(groupControl, Application.Current.MainWindow);
                    groupInfo.Bounds = new Rect(0, separatorBounds.Top, this.timeline.ActualWidth, groupBounds.Height);
                }

                infos.Add(groupInfo);
                previousItem = groupInfo;
                previousSeparatorY = separatorBounds.Y;
            }
            return infos;
        }

        private Rect GetGroupSeparatorBounds(TimelineItemGroupControl groupControl)
        {
            var header = groupControl.ChildrenOfType<Grid>().FirstOrDefault(x => x.Name.Equals("Header"));
            var pathSeparator = header.Children.OfType<Path>().FirstOrDefault();
            var pathSeparatorBounds = BoundsRelativeTo(pathSeparator, Application.Current.MainWindow);
            return pathSeparatorBounds;
        }

        private Rect BoundsRelativeTo(FrameworkElement element, Visual relativeTo)
        {
            GeneralTransform gt = element.TransformToAncestor(relativeTo);
            return gt.TransformBounds(new Rect(0, 0, element.ActualWidth, element.ActualHeight));
        }
    }
}
