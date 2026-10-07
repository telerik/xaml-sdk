using System;
using System.Windows;
using System.Windows.Data;
using Telerik.Windows;

namespace _2._5D_Chart
{
    public class DependencyPropertyWatcher<T> : DependencyObject, IDisposable
    {
        public static readonly DependencyProperty WatcherPropertyProperty = DependencyProperty.Register(
            "WatcherProperty",
            typeof(T),
            typeof(DependencyPropertyWatcher<T>),
            new PropertyMetadata(WatcherPropertyChanged));

        private RadRoutedEventHandler callback;
        private object watchedElement;

        public DependencyPropertyWatcher(object watchedElement, string propertyName, RadRoutedEventHandler propertyChangedCallback)
        {
            this.watchedElement = watchedElement;
            this.callback = propertyChangedCallback;
            BindingOperations.SetBinding(this, DependencyPropertyWatcher<T>.WatcherPropertyProperty, new Binding(propertyName) { Source = watchedElement });
        }

        public T WatcherProperty
        {
            get { return (T)GetValue(WatcherPropertyProperty); }
            set { SetValue(WatcherPropertyProperty, value); }
        }

        private static void WatcherPropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            var watcher = (DependencyPropertyWatcher<T>)target;
            watcher.RaiseCallback();
        }

        private void RaiseCallback()
        {
            if (this.watchedElement != null && this.callback != null)
            {
                this.callback.Invoke(this.watchedElement, new RadRoutedEventArgs());
            }
        }

        public void Dispose()
        {
            this.watchedElement = null;
            this.callback = null;
        }
    }
}
