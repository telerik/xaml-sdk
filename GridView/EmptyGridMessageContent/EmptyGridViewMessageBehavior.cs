using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace EmptyGridMessageContent
{
    public class EmptyGridViewMessageBehavior
    {
        private static Dictionary<RadGridView, EmptyGridViewMessageBehavior> gridViewToBehaviorDict = new Dictionary<RadGridView, EmptyGridViewMessageBehavior>();
        
        public static readonly DependencyProperty EmptyMessageContentProperty =
            DependencyProperty.RegisterAttached(
                "EmptyMessageContent",
                typeof(object), 
                typeof(EmptyGridViewMessageBehavior), 
                new PropertyMetadata(null, OnEmptyMessageContentChanged));

        public static object GetEmptyMessageContent(DependencyObject obj)
        {
            return (object)obj.GetValue(EmptyMessageContentProperty);
        }

        public static void SetEmptyMessageContent(DependencyObject obj, object value)
        {
            obj.SetValue(EmptyMessageContentProperty, value);
        }

        private static void OnEmptyMessageContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gridView = (RadGridView)d;
            EmptyGridViewMessageBehavior behavior;
            if (!gridViewToBehaviorDict.TryGetValue(gridView, out behavior))
            {
                behavior = new EmptyGridViewMessageBehavior();
                behavior.Attach(gridView);
            }

            if (e.NewValue != null)
            {
                behavior.UpdateContent(e.NewValue);
            }
            else
            {
                behavior.Dettach();
                gridViewToBehaviorDict.Remove(gridView);
            }
        }

        private RadGridView gridView;
        private ContentControl additionalContentControl;
        private object messageContent;

        public void Attach(RadGridView gridView)
        {
            if (gridView == null)
            {
                return;
            }

            this.gridView = gridView;
            this.gridView.Items.CollectionChanged += OnGridViewItemsCollectionChanged;
            this.PrepareAdditionalContentControl();

            if (VisualTreeHelper.GetChildrenCount(gridView) > 0)
            {
                this.InsertAdditionalContentControl();
            }
            else
            {
                this.gridView.Loaded += OnGridViewLoaded;
            }
        }

        public void Dettach()
        {
            this.messageContent = null;
            this.gridView.Items.CollectionChanged -= OnGridViewItemsCollectionChanged;
            this.RemoveAdditionalContentControl();
            this.additionalContentControl.Content = null;
            this.additionalContentControl = null;
            this.gridView = null;
        }

        public void UpdateContent(object content)
        {
            this.messageContent = content;
            if (this.additionalContentControl != null)
            {
                this.additionalContentControl.Content = content;
            }
        }
        
        private void OnGridViewLoaded(object sender, RoutedEventArgs e)
        {
            this.InsertAdditionalContentControl();
            this.gridView.Loaded -= OnGridViewLoaded;
        }

        private void OnGridViewItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.additionalContentControl.Visibility = this.gridView.Items.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void InsertAdditionalContentControl()
        {
            if (this.additionalContentControl.Content != this.messageContent)
            {
                this.additionalContentControl.Content = this.messageContent;
            }
            
            var rootGrid = this.gridView.FindChildByType<Grid>();
            rootGrid.Children.Add(this.additionalContentControl);
        }

        private void RemoveAdditionalContentControl()
        {
            var rootGrid = this.gridView.FindChildByType<Grid>();
            rootGrid.Children.Remove(this.additionalContentControl);
        }

        private void PrepareAdditionalContentControl()
        {
            this.additionalContentControl = new ContentControl();
            this.additionalContentControl.Name = "EmptyMessageContent";
            this.additionalContentControl.IsHitTestVisible = false;
            this.additionalContentControl.Margin = new Thickness(0, 40, 0, 0);
            this.additionalContentControl.SetValue(Grid.RowProperty, 2);
            this.additionalContentControl.SetValue(Grid.RowSpanProperty, 2);
            this.additionalContentControl.SetValue(Grid.ColumnSpanProperty, 2);
        }
    }
}
