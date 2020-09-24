using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace SelectedItemsBinding
{
	public class SelectedItemsBehavior : Behavior<RadComboBox>
    {
        private RadComboBox ComboBox
        {
            get
            {
                return this.AssociatedObject as RadComboBox;
            }
        }

        public INotifyCollectionChanged SelectedItems
        {
            get { return (INotifyCollectionChanged)this.GetValue(SelectedItemsProperty); }
            set { this.SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemsProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(INotifyCollectionChanged), typeof(SelectedItemsBehavior), new PropertyMetadata(OnSelectedItemsPropertyChanged));
        
        private static void OnSelectedItemsPropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            var collection = args.NewValue as INotifyCollectionChanged;
            if (collection != null)
            {
                ((SelectedItemsBehavior)target).UpdateTransfer(args.NewValue);
                collection.CollectionChanged += ((SelectedItemsBehavior)target).ContextSelectedItems_CollectionChanged;
            }
        }

        private void UpdateTransfer(object items)
        {
            Transfer(items as IList, this.ComboBox.SelectedItems);
            this.ComboBox.SelectionChanged += this.ComboSelectionChanged;
        }

        private void ContextSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UnsubscribeFromEvents();
            Transfer(SelectedItems as IList, this.ComboBox.SelectedItems);
            this.SubscribeToEvents();
        }

        private void ComboSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.ComboBox.ItemsSource != null)
            {
                this.UnsubscribeFromEvents();
                Transfer(this.ComboBox.SelectedItems, SelectedItems as IList);
                this.SubscribeToEvents();
            }
        }

        private void SubscribeToEvents()
        {
            this.ComboBox.SelectionChanged += this.ComboSelectionChanged;
            if (this.SelectedItems != null)
            {
                this.SelectedItems.CollectionChanged += this.ContextSelectedItems_CollectionChanged;
            }
        }

        private void UnsubscribeFromEvents()
        {
            this.ComboBox.SelectionChanged -= this.ComboSelectionChanged;
            if (this.SelectedItems != null)
            {
                this.SelectedItems.CollectionChanged -= this.ContextSelectedItems_CollectionChanged;
            }
        }

        public static void Transfer(IList source, IList target)
        {
            if (source == null || target == null)
                return;

            target.Clear();
            foreach (var o in source)
            {
                target.Add(o);
            }
        }
    }
}
