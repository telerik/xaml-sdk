using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace ExpandItemsIntoView
{
    public static class RadTreeListViewExtensions
    {
        public static void ExpandItemIntoView(this RadTreeListView treeListView, object item)
        {
            if (treeListView.Items.IndexOf(item) == -1)
            {
                var filterDescriptor = new FilterDescriptor<object>() { FilteringExpression = i => i == item };
                treeListView.FilterDescriptors.Add(filterDescriptor);

                var itemsToExpand = new List<object>(treeListView.Items.OfType<object>());

                treeListView.FilterDescriptors.Remove(filterDescriptor);

                ExpandAndScrollRecursive(treeListView, itemsToExpand.FirstOrDefault(), itemsToExpand);
            }
            else
            {
                treeListView.ScrollIntoViewAsync(item, (f) =>
                {
                    treeListView.ExpandHierarchyItem(item);
                });
            }
        }

        public static void ExpandItemIntoView(this RadTreeListView treeListView, IEnumerable itemsToExpand)
        {
            var items = itemsToExpand.OfType<object>();
            ExpandAndScrollRecursive(treeListView, items.FirstOrDefault(), new List<object>(items));
        }

        private static void ExpandAndScrollRecursive(RadTreeListView treeListView, object item, IList<object> itemsToExpand)
        {
            treeListView.ScrollIntoViewAsync(item, (f) =>
            {
                treeListView.ExpandHierarchyItem(item);

                itemsToExpand.Remove(item);

                ExpandAndScrollRecursive(treeListView, itemsToExpand.FirstOrDefault(), itemsToExpand);
            });
        }
    }
}
