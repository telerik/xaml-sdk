using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;

namespace DragDropWithLines
{
    public class RowReorderBehavior
    {
        private const string DropPositionFeedbackElementName = "DragBetweenItemsFeedback";
        private ContentPresenter dropPositionFeedbackPresenter;
        private Grid dropPositionFeedbackPresenterHost;

        private RadGridView _associatedObject;

        /// <summary>
        /// AssociatedObject Property
        /// </summary>
        public RadGridView AssociatedObject
        {
            get
            {
                return _associatedObject;
            }
            set
            {
                _associatedObject = value;
            }
        }

        private readonly static Dictionary<RadGridView, RowReorderBehavior> instances;

        static RowReorderBehavior()
        {
            instances = new Dictionary<RadGridView, RowReorderBehavior>();
        }

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            RowReorderBehavior behavior = GetAttachedBehavior(obj as RadGridView);

            behavior.AssociatedObject = obj as RadGridView;

            if (value)
            {
                behavior.Initialize();
            }
            else
            {
                behavior.CleanUp();
            }
            obj.SetValue(IsEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(RowReorderBehavior),
                new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));

        public static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SetIsEnabled(dependencyObject, (bool)e.NewValue);
        }

        private static RowReorderBehavior GetAttachedBehavior(RadGridView gridview)
        {
            if (!instances.ContainsKey(gridview))
            {
                instances[gridview] = new RowReorderBehavior();
                instances[gridview].AssociatedObject = gridview;
            }

            return instances[gridview];
        }

        public RowReorderBehavior()
        {

        }

        protected virtual void Initialize()
        {
            this.AssociatedObject.RowLoaded -= this.AssociatedObject_RowLoaded;
            this.AssociatedObject.RowLoaded += this.AssociatedObject_RowLoaded;
            this.UnsubscribeFromDragDropEvents();
            this.SubscribeToDragDropEvents();

            this.AssociatedObject.Dispatcher.BeginInvoke((Action)(() =>
            {
                this.dropPositionFeedbackPresenter = new ContentPresenter();
                this.dropPositionFeedbackPresenter.Name = DropPositionFeedbackElementName;
                this.dropPositionFeedbackPresenter.HorizontalAlignment = HorizontalAlignment.Left;
                this.dropPositionFeedbackPresenter.VerticalAlignment = VerticalAlignment.Top;
                this.dropPositionFeedbackPresenter.RenderTransformOrigin = new Point(0.5, 0.5);

                this.AttachDropPositionFeedback();
            }));
        }

        protected virtual void CleanUp()
        {
            this.AssociatedObject.RowLoaded -= this.AssociatedObject_RowLoaded;
            this.UnsubscribeFromDragDropEvents();
            
            this.DetachDropPositionFeedback();
        }

        void AssociatedObject_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            if (e.Row is GridViewHeaderRow || e.Row is GridViewNewRow || e.Row is GridViewFooterRow)
                return;

            GridViewRow row = e.Row as GridViewRow;
            this.InitializeRowDragAndDrop(row);
        }

        private void InitializeRowDragAndDrop(GridViewRow row)
        {
            if (row == null)
                return;

            DragDropManager.RemoveDragOverHandler(row, OnRowDragOver);
            DragDropManager.AddDragOverHandler(row, OnRowDragOver);
        }

        private void SubscribeToDragDropEvents()
        {
            DragDropManager.AddDragInitializeHandler(this.AssociatedObject, OnDragInitialize);
            DragDropManager.AddGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback);
            DragDropManager.AddDropHandler(this.AssociatedObject, OnDrop);
            DragDropManager.AddDragDropCompletedHandler(this.AssociatedObject, OnDragDropCompleted);
        }

        private void UnsubscribeFromDragDropEvents()
        {
            DragDropManager.RemoveDragInitializeHandler(this.AssociatedObject, OnDragInitialize);
            DragDropManager.RemoveGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback);
            DragDropManager.RemoveDropHandler(this.AssociatedObject, OnDrop);
            DragDropManager.RemoveDragDropCompletedHandler(this.AssociatedObject, OnDragDropCompleted);
        }

        private void OnDragDropCompleted(object sender, DragDropCompletedEventArgs e)
        {
            this.HideDropPositionFeedbackPresenter();
        }

        private void OnDragInitialize(object sender, DragInitializeEventArgs e)
        {
            var sourceRow = e.OriginalSource as GridViewRow ?? (e.OriginalSource as FrameworkElement).ParentOfType<GridViewRow>();
            if (sourceRow != null && sourceRow.Name != "PART_RowResizer")
            {
                DropIndicationDetails details = new DropIndicationDetails();
                var item = sourceRow.Item;
                details.CurrentDraggedItem = item;

                IDragPayload dragPayload = DragDropPayloadManager.GeneratePayload(null);

                dragPayload.SetData("DraggedItem", item);
                dragPayload.SetData("DropDetails", details);

                e.Data = dragPayload;

                e.DragVisual = new DragVisual()
                {
                    Content = details,
                    ContentTemplate = this.AssociatedObject.Resources["DraggedItemTemplate"] as DataTemplate
                };
                e.DragVisualOffset = e.RelativeStartPoint;
                e.AllowedEffects = DragDropEffects.All;
            }
        }

        private void OnGiveFeedback(object sender, Telerik.Windows.DragDrop.GiveFeedbackEventArgs e)
        {
            e.SetCursor(Cursors.Arrow);
            e.Handled = true;
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedItem");
            var details = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if (details == null || draggedItem == null)
            {
                return;
            }

            if (e.Effects == DragDropEffects.Move || e.Effects == DragDropEffects.All)
            {
                ((sender as RadGridView).ItemsSource as IList).Remove(draggedItem);
            }

            if (e.Effects != DragDropEffects.None)
            {
                var collection = (sender as RadGridView).ItemsSource as IList;
                int index = details.DropIndex < 0 ? 0 : details.DropIndex;
                index = details.DropIndex > collection.Count - 1 ? collection.Count : index;

                collection.Insert(index, draggedItem);
            }

            HideDropPositionFeedbackPresenter();
        }

        private void OnRowDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var row = sender as GridViewRow;
            var details = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if (details == null || row == null)
            {
                return;
            }

            details.CurrentDraggedOverItem = row.DataContext;

            if (details.CurrentDraggedItem == details.CurrentDraggedOverItem)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }

            details.CurrentDropPosition = GetDropPositionFromPoint(e.GetPosition(row), row);
            int dropIndex = (this.AssociatedObject.Items as IList).IndexOf(row.DataContext);
            int draggedItemIdex = (this.AssociatedObject.Items as IList).IndexOf(DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedItem"));

            if (dropIndex >= row.GridViewDataControl.Items.Count - 1 && details.CurrentDropPosition == DropPosition.After)
            {
                details.DropIndex = dropIndex;
                this.ShowDropPositionFeedbackPresenter(this.AssociatedObject, row, details.CurrentDropPosition);
                return;
            }

            dropIndex = draggedItemIdex > dropIndex ? dropIndex : dropIndex - 1;
            details.DropIndex = details.CurrentDropPosition == DropPosition.Before ? dropIndex : dropIndex + 1;

            this.ShowDropPositionFeedbackPresenter(this.AssociatedObject, row, details.CurrentDropPosition);
        }

        public virtual DropPosition GetDropPositionFromPoint(Point absoluteMousePosition, GridViewRow row)
        {
            if (row != null)
            {
                return absoluteMousePosition.Y < row.ActualHeight / 2 ? DropPosition.Before : DropPosition.After;
            }

            return DropPosition.Inside;
        }

        private bool IsDropPositionFeedbackAvailable()
        {
            return
                  this.dropPositionFeedbackPresenterHost != null &&
                  this.dropPositionFeedbackPresenter != null;
        }

        private void ShowDropPositionFeedbackPresenter(GridViewDataControl gridView, GridViewRow row, DropPosition lastRowDropPosition)
        {
            if (!this.IsDropPositionFeedbackAvailable())
                return;
            var yOffset = this.GetDropPositionFeedbackOffset(row, lastRowDropPosition);
            this.dropPositionFeedbackPresenter.Visibility = Visibility.Visible;
            this.dropPositionFeedbackPresenter.Width = row.ActualWidth;
            this.dropPositionFeedbackPresenter.RenderTransform = new TranslateTransform()
            {
                Y = yOffset
            };
        }

        private void HideDropPositionFeedbackPresenter()
        {
            this.dropPositionFeedbackPresenter.RenderTransform = new TranslateTransform()
            {
                X = 0,
                Y = 0
            };
            this.dropPositionFeedbackPresenter.Visibility = Visibility.Collapsed;
        }

        private double GetDropPositionFeedbackOffset(GridViewRow row, DropPosition dropPosition)
        {
            var yOffset = row.TransformToVisual(this.dropPositionFeedbackPresenterHost).Transform(new Point(0, 0)).Y;
            if (dropPosition == DropPosition.After)
                yOffset += row.ActualHeight;
            yOffset -= (this.dropPositionFeedbackPresenter.ActualHeight / 2.0);
            return yOffset;
        }

        private void DetachDropPositionFeedback()
        {
            if (this.IsDropPositionFeedbackAvailable())
            {
                this.dropPositionFeedbackPresenterHost.Children.Remove(this.dropPositionFeedbackPresenter);
                this.dropPositionFeedbackPresenter = null;
            }
        }

        private void AttachDropPositionFeedback()
        {
            this.dropPositionFeedbackPresenterHost = this.AssociatedObject.ParentOfType<Grid>();

            if (this.dropPositionFeedbackPresenterHost != null)
            {
                this.dropPositionFeedbackPresenter.Content = CreateDefaultDropPositionFeedback();
                if (dropPositionFeedbackPresenterHost != null && dropPositionFeedbackPresenterHost.FindName(this.dropPositionFeedbackPresenter.Name) == null)
                {
                    this.dropPositionFeedbackPresenterHost.Children.Add(this.dropPositionFeedbackPresenter);
                }
            }
            this.HideDropPositionFeedbackPresenter();
        }

        private static UIElement CreateDefaultDropPositionFeedback()
        {
            Grid grid = new Grid()
            {
                Height = 8,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                IsHitTestVisible = false,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            grid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(8)
            });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            Ellipse ellipse = new Ellipse()
            {
                Stroke = new SolidColorBrush(Colors.Orange),
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.Orange),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Width = 8,
                Height = 8
            };
            Rectangle rectangle = new Rectangle()
            {
                Fill = new SolidColorBrush(Colors.Orange),
                RadiusX = 2,
                RadiusY = 2,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = 2
            };
            Grid.SetColumn(ellipse, 0);
            Grid.SetColumn(rectangle, 1);
            grid.Children.Add(ellipse);
            grid.Children.Add(rectangle);

            Canvas.SetZIndex(grid, 10000);

            return grid;
        }
    }
}