using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace BindingSelectedItemsToViewModel
{
    public static class ChartUtilities
    {
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.RegisterAttached(
            "SelectedItems",
            typeof(INotifyCollectionChanged),
            typeof(ChartUtilities),
            new PropertyMetadata(null, OnSelectedItemsChanged));

        private static bool isSyncingSelection;
        private static List<Tuple<WeakReference, List<ChartSelectionBehavior>>> collectionToSelectionBehaviors = new List<Tuple<WeakReference, List<ChartSelectionBehavior>>>();

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
            var behavior = (ChartSelectionBehavior)target;

            var oldCollection = args.OldValue as INotifyCollectionChanged;
            if (oldCollection != null)
            {
                behavior.SelectionChanged -= Behavior_SelectionChanged;
                oldCollection.CollectionChanged -= SelectedItems_CollectionChanged;
                RemoveAssociation(oldCollection, behavior);
            }

            var newCollection = args.NewValue as INotifyCollectionChanged;
            if (newCollection != null)
            {
                behavior.SelectionChanged += Behavior_SelectionChanged;
                newCollection.CollectionChanged += SelectedItems_CollectionChanged;
                AddAssociation(newCollection, behavior);

                behavior.Dispatcher.BeginInvoke((Action)(() =>
                {
                    OnSelectedItemsChanged(newCollection, null, (IList)newCollection);
                }));
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

            var behaviors = GetOrCreateSelectionBehaviors(collection);
            foreach (var behavior in behaviors)
            {
                SyncSelection(behavior, oldItems, newItems);
            }

            isSyncingSelection = false;
        }

        private static void Behavior_SelectionChanged(object sender, ChartSelectionChangedEventArgs args)
        {
            if (isSyncingSelection)
            {
                return;
            }

            var collection = (IList)GetSelectedItems((ChartSelectionBehavior)sender);
            foreach (DataPoint point in args.RemovedPoints)
            {
                var dataItem = point.DataItem;
                collection.Remove(dataItem);
            }
            foreach (DataPoint point in args.AddedPoints)
            {
                var dataItem = point.DataItem;
                collection.Add(dataItem);
            }
        }

        private static void SyncSelection(ChartSelectionBehavior behavior, IList oldItems, IList newItems)
        {
            if (oldItems != null)
            {
                SetItemsSelection(behavior, oldItems, false);
            }

            if (newItems != null)
            {
                SetItemsSelection(behavior, newItems, true);
            }
        }

        private static void SetItemsSelection(ChartSelectionBehavior behavior, IList items, bool shouldSelect)
        {
            foreach (var item in items)
            {
                var dataPoints = GetDataPointsForItem(item, behavior);
                foreach (var dataPoint in dataPoints)
                {
                    dataPoint.IsSelected = shouldSelect;
                }
            }
        }

        private static IEnumerable<DataPoint> GetDataPointsForItem(object item, ChartSelectionBehavior behavior)
        {
            IEnumerable seriesCollection = GetSeries(behavior.Chart);
            foreach (ChartSeries series in seriesCollection)
            {
                var dataPoints = GetDataPoints(series);
                foreach (DataPoint dataPoint in dataPoints)
                {
                    if (dataPoint.DataItem == item)
                    {
                        yield return dataPoint;
                    }
                }
            }
        }

        private static IEnumerable GetSeries(RadChartBase chartView)
        {
            var cartesianChart = chartView as RadCartesianChart;
            if (cartesianChart != null)
            {
                return cartesianChart.Series;
            }

            var pieChart = chartView as RadPieChart;
            if (pieChart != null)
            {
                return pieChart.Series;
            }

            var polarChart = (RadPolarChart)chartView;
            return polarChart.Series;
        }

        private static IEnumerable GetDataPoints(ChartSeries series)
        {
            var dataPoints = (IEnumerable)series.GetType().GetProperty("DataPoints").GetValue(series);
            return dataPoints;
        }

        private static void AddAssociation(INotifyCollectionChanged collection, ChartSelectionBehavior behavior)
        {
            List<ChartSelectionBehavior> behaviors = GetOrCreateSelectionBehaviors(collection);
            behaviors.Add(behavior);
        }

        private static void RemoveAssociation(INotifyCollectionChanged collection, ChartSelectionBehavior behavior)
        {
            List<ChartSelectionBehavior> behaviors = GetOrCreateSelectionBehaviors(collection);
            behaviors.Remove(behavior);

            if (behaviors.Count == 0)
            {
                CleanUp();
            }
        }

        private static List<ChartSelectionBehavior> GetOrCreateSelectionBehaviors(INotifyCollectionChanged collection)
        {
            for (int i = 0; i < collectionToSelectionBehaviors.Count; i++)
            {
                var wr = collectionToSelectionBehaviors[i].Item1;
                if (wr.Target == collection)
                {
                    return collectionToSelectionBehaviors[i].Item2;
                }
            }

            collectionToSelectionBehaviors.Add(new Tuple<WeakReference, List<ChartSelectionBehavior>>(new WeakReference(collection), new List<ChartSelectionBehavior>()));
            return collectionToSelectionBehaviors[collectionToSelectionBehaviors.Count - 1].Item2;
        }

        private static void CleanUp()
        {
            for (int i = collectionToSelectionBehaviors.Count - 1; i >= 0; i--)
            {
                bool isAlive = collectionToSelectionBehaviors[i].Item1.IsAlive;
                var behaviors = collectionToSelectionBehaviors[i].Item2;
                if (behaviors.Count == 0 || !isAlive)
                {
                    collectionToSelectionBehaviors.RemoveAt(i);
                }
            }
        }
    }
}
