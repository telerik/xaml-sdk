using System;
using System.Windows;
using Telerik.Windows.Controls;
using System.Collections.Specialized;
using System.Collections;
using System.Windows.Data;

namespace BindingColumnsFromViewModel
{
    public class MyColumnsBindingBehavior : ViewModelBase
    {
        private readonly RadGridView grid = null;
        private readonly INotifyCollectionChanged columns = null;

        public static readonly DependencyProperty ColumnsProperty
            = DependencyProperty.RegisterAttached("Columns", typeof(INotifyCollectionChanged), typeof(MyColumnsBindingBehavior),
                new PropertyMetadata(new PropertyChangedCallback(OnColumnsPropertyChanged)));

        public static void SetColumns(DependencyObject dependencyObject, INotifyCollectionChanged columns)
        {
            dependencyObject.SetValue(ColumnsProperty, columns);
        }

        public static INotifyCollectionChanged GetColumns(DependencyObject dependencyObject)
        {
            return (INotifyCollectionChanged)dependencyObject.GetValue(ColumnsProperty);
        }

        private static void OnColumnsPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            RadGridView grid = dependencyObject as RadGridView;
            INotifyCollectionChanged columns = e.NewValue as INotifyCollectionChanged;

            if (grid != null && columns != null)
            {
                MyColumnsBindingBehavior behavior = new MyColumnsBindingBehavior(grid, columns);
                behavior.Attach();
            }
        }

        private void Attach()
        {
            if (grid != null && columns != null)
            {
                Transfer(GetColumns(grid) as IList, grid.Columns);

                columns.CollectionChanged -= ContextColumns_CollectionChanged;
                columns.CollectionChanged += ContextColumns_CollectionChanged;
            }
        }

        public MyColumnsBindingBehavior(RadGridView grid, INotifyCollectionChanged columns)
        {
            this.grid = grid;
            this.columns = columns;
        }

        void ContextColumns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();

            Transfer(GetColumns(grid) as IList, grid.Columns);

            SubscribeToEvents();
        }

        void GridColumns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();

            Transfer(grid.Columns, GetColumns(grid) as IList);

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            grid.Columns.CollectionChanged += GridColumns_CollectionChanged;

            if (GetColumns(grid) != null)
            {
                GetColumns(grid).CollectionChanged += ContextColumns_CollectionChanged;
            }
        }

        private void UnsubscribeFromEvents()
        {
            grid.Columns.CollectionChanged -= GridColumns_CollectionChanged;

            if (GetColumns(grid) != null)
            {
                GetColumns(grid).CollectionChanged -= ContextColumns_CollectionChanged;
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
