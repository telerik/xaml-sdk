using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System.Collections;

namespace SelectedItemsBinding
{
    public static class ComboBoxSelectionUtilities
    {
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.RegisterAttached(
            "SelectedItems",
            typeof(INotifyCollectionChanged),
            typeof(ComboBoxSelectionUtilities),
            new PropertyMetadata(null, OnSelectedItemsChanged));

        private static bool isSyncingSelection;
        private static List<Tuple<WeakReference, List<RadComboBox>>> collectionToComboBoxes = new List<Tuple<WeakReference, List<RadComboBox>>>();

        public static INotifyCollectionChanged GetSelectedItems(DependencyObject obj)
        {
            return (INotifyCollectionChanged)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, INotifyCollectionChanged value)
        {
            obj.SetValue(SelectedItemsProperty, value);
        }

        private static void OnSelectedItemsChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            var comboBox = (RadComboBox)target;

            var oldCollection = args.OldValue as INotifyCollectionChanged;
            if (oldCollection != null)
            {
                comboBox.SelectionChanged -= ComboBox_SelectionChanged;
                oldCollection.CollectionChanged -= SelectedItems_CollectionChanged;
                RemoveAssociation(oldCollection, comboBox);
            }

            var newCollection = args.NewValue as INotifyCollectionChanged;
            if (newCollection != null)
            {
                comboBox.SelectionChanged += ComboBox_SelectionChanged;
                newCollection.CollectionChanged += SelectedItems_CollectionChanged;
                AddAssociation(newCollection, comboBox);
                OnSelectedItemsChanged(newCollection, null, (IList)newCollection);
            }
        }

        private static void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            INotifyCollectionChanged collection = (INotifyCollectionChanged)sender;
            OnSelectedItemsChanged(collection, args.OldItems, args.NewItems);
        }

        private static void OnSelectedItemsChanged(INotifyCollectionChanged collection, IList oldItems, IList newItems)
        {
            isSyncingSelection = true;

            var comboBoxes = GetOrCreateComboBox(collection);
            foreach (var comboBox in comboBoxes)
            {
                SyncSelection(comboBox, oldItems, newItems);
            }

            isSyncingSelection = false;
        }

        private static void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (isSyncingSelection)
            {
                return;
            }

            var collection = (IList)GetSelectedItems((RadComboBox)sender);
            foreach (object item in args.RemovedItems)
            {
                collection.Remove(item);
            }
            foreach (object item in args.AddedItems)
            {
                collection.Add(item);
            }
        }

        private static void SyncSelection(RadComboBox comboBox, IList oldItems, IList newItems)
        {
            if (oldItems != null)
            {
                SetItemsSelection(comboBox, oldItems, false);
            }

            if (newItems != null)
            {
                SetItemsSelection(comboBox, newItems, true);
            }
        }

        private static void SetItemsSelection(RadComboBox comboBox, IList items, bool shouldSelect)
        {
            foreach (var item in items)
            {
                bool contains = comboBox.SelectedItems.Contains(item);
                if (shouldSelect && !contains)
                {
                    comboBox.SelectedItems.Add(item);
                }
                else if (contains && !shouldSelect)
                {
                    comboBox.SelectedItems.Remove(item);
                }
            }
        }

        private static void AddAssociation(INotifyCollectionChanged collection, RadComboBox comboBox)
        {
            List<RadComboBox> comboBoxes = GetOrCreateComboBox(collection);
            comboBoxes.Add(comboBox);
        }

        private static void RemoveAssociation(INotifyCollectionChanged collection, RadComboBox comboBox)
        {
            List<RadComboBox> comboBoxes = GetOrCreateComboBox(collection);
            comboBoxes.Remove(comboBox);

            if (comboBoxes.Count == 0)
            {
                CleanUp();
            }
        }

        private static List<RadComboBox> GetOrCreateComboBox(INotifyCollectionChanged collection)
        {
            for (int i = 0; i < collectionToComboBoxes.Count; i++)
            {
                var wr = collectionToComboBoxes[i].Item1;
                if (wr.Target == collection)
                {
                    return collectionToComboBoxes[i].Item2;
                }
            }

            collectionToComboBoxes.Add(new Tuple<WeakReference, List<RadComboBox>>(new WeakReference(collection), new List<RadComboBox>()));
            return collectionToComboBoxes[collectionToComboBoxes.Count - 1].Item2;
        }

        private static void CleanUp()
        {
            for (int i = collectionToComboBoxes.Count - 1; i >= 0; i--)
            {
                bool isAlive = collectionToComboBoxes[i].Item1.IsAlive;
                var behaviors = collectionToComboBoxes[i].Item2;
                if (behaviors.Count == 0 || !isAlive)
                {
                    collectionToComboBoxes.RemoveAt(i);
                }
            }
        }
    }
}
