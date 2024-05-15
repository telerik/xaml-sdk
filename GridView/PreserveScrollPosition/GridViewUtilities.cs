using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace PreserveSelectedItemScrollPosition
{
    public static class GridViewUtilities
    {
        public static bool GetPreserveSelectedItemScrollPosition(DependencyObject obj)
        {
            return (bool)obj.GetValue(PreserveSelectedItemScrollPositionProperty);
        }

        public static void SetPreserveSelectedItemScrollPosition(DependencyObject obj, bool value)
        {
            obj.SetValue(PreserveSelectedItemScrollPositionProperty, value);
        }

        public static readonly DependencyProperty PreserveSelectedItemScrollPositionProperty =
            DependencyProperty.RegisterAttached("PreserveSelectedItemScrollPosition", typeof(bool), typeof(GridViewUtilities), new PropertyMetadata(OnPreserveSelectedItemScrollPosition));

        private static void OnPreserveSelectedItemScrollPosition(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gridView = d as RadGridView;

            gridView.Items.CollectionChanged += (s, a) =>
            {
                var selectedItem = gridView.SelectedItem;
                var selectedItemIndex = gridView.Items.IndexOf(selectedItem);

                double offset = 0;

                if (a.NewItems != null)
                {
                    foreach (var item in a.NewItems)
                    {
                        if (gridView.Items.IndexOf(item) < selectedItemIndex)
                        {
                            offset += gridView.RowHeight;
                        }
                    }
                }

                if (a.OldItems != null)
                {
                    foreach (var item in a.OldItems)
                    {
                        if (a.OldStartingIndex < selectedItemIndex)
                        {
                            offset -= gridView.RowHeight;
                        }
                    }
                }

                var scrollViewer = gridView.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + offset);
                scrollViewer.UpdateLayout(); // required so that the value of the VerticalOffset property is updated
            };
        }
    }
}
