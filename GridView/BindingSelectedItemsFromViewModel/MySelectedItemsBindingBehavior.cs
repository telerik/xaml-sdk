using System;
using System.Windows;
using Telerik.Windows.Controls;
using System.Collections.Specialized;
using System.Collections;
using System.Windows.Data;

namespace BindingSelectedItemsFromViewModel
{
    public class MySelectedItemsBindingBehavior : ViewModelBase
    {
        private readonly RadGridView grid = null;
        private readonly INotifyCollectionChanged selectedItems = null;
        private Boolean isSubscribedToEvents = false;
        private static Boolean isAttached = false;


        public static readonly DependencyProperty SelectedItemsProperty
            = DependencyProperty.RegisterAttached("SelectedItems", typeof(INotifyCollectionChanged), typeof(MySelectedItemsBindingBehavior),
                new PropertyMetadata(new PropertyChangedCallback(OnSelectedItemsPropertyChanged)));

        public static void SetSelectedItems(DependencyObject dependencyObject, INotifyCollectionChanged selectedItems)
        {
            dependencyObject.SetValue(SelectedItemsProperty, selectedItems);
        }

        public static INotifyCollectionChanged GetSelectedItems(DependencyObject dependencyObject)
        {
            return (INotifyCollectionChanged)dependencyObject.GetValue(SelectedItemsProperty);
        }

        private static void OnSelectedItemsPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            RadGridView grid = dependencyObject as RadGridView;
            INotifyCollectionChanged selectedItems = e.NewValue as INotifyCollectionChanged;

            if (grid != null && selectedItems != null && !isAttached)
            {
                MySelectedItemsBindingBehavior behavior = new MySelectedItemsBindingBehavior(grid, selectedItems);
                behavior.Attach();
                isAttached = true;
            }
        }

        private void Attach()
        {
            if (grid != null && selectedItems != null)
            {
                Transfer(GetSelectedItems(grid) as IList, grid.SelectedItems);
                SubscribeToEvents();
            }
        }

        public MySelectedItemsBindingBehavior(RadGridView grid, INotifyCollectionChanged selectedItems)
        {
            this.grid = grid;
            this.selectedItems = selectedItems;
        }

        void ContextSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();

            Transfer(GetSelectedItems(grid) as IList, grid.SelectedItems);

            SubscribeToEvents();
        }

        void GridSelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();

            Transfer(grid.SelectedItems, GetSelectedItems(grid) as IList);

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (!isSubscribedToEvents)
            {
                grid.SelectedItems.CollectionChanged += GridSelectedItems_CollectionChanged;

                if (GetSelectedItems(grid) != null)
                {
                    GetSelectedItems(grid).CollectionChanged += ContextSelectedItems_CollectionChanged;
                }
                isSubscribedToEvents = true;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (isSubscribedToEvents)
            {
                grid.SelectedItems.CollectionChanged -= GridSelectedItems_CollectionChanged;

                if (GetSelectedItems(grid) != null)
                {
                    GetSelectedItems(grid).CollectionChanged -= ContextSelectedItems_CollectionChanged;
                }
                isSubscribedToEvents = false;
            }
        }

        public static void Transfer(IList source, IList target)
        {
            if (source == null || target == null)
                return;

            target.Clear();

            foreach (object o in source)
            {
                target.Add(o);
            }
        }
    }
}
