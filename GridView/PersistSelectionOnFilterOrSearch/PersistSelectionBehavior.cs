using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace BoundSelectColumn
{
    public class PersistSelectionBehavior : Behavior<RadGridView>
    {
        private bool persistSelection = false;
        private HashSet<object> selectedItems = new HashSet<object>();

        RadGridView RadGridView { get { return AssociatedObject; } }

        protected override void OnAttached()
        {
            this.RadGridView.ShowSearchPanel = true; // required to initialize the search panel and correctly attach to its children's events
            this.RadGridView.Loaded += RadGridView_Loaded;
            this.RadGridView.DataLoaded += RadGridView_DataLoaded;
            this.RadGridView.SelectionChanged += RadGridView_SelectionChanged;
            this.RadGridView.Items.CollectionChanged += Items_CollectionChanged;
            this.RadGridView.Filtered += Filter_Changed;
        }

        private void RadGridView_Loaded(object sender, RoutedEventArgs e)
        {
            var searchPanel = this.RadGridView.ChildrenOfType<GridViewSearchPanel>().First();
            var textBox = searchPanel.ChildrenOfType<TextBox>().First();
            textBox.TextChanged += Filter_Changed;
            //this.RadGridView.ShowSearchPanel = false; uncomment this line if you wish to hide the search panel by default
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            foreach (var item in this.RadGridView.SelectedItems)
            {
                this.selectedItems.Add(item);
            }
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                this.persistSelection = true;
            }
        }

        private void RadGridView_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            if (this.persistSelection)
            {
                foreach (var item in e.RemovedItems)
                {
                    this.selectedItems.Remove(item);
                }

                foreach (var item in e.AddedItems)
                {
                    this.selectedItems.Add(item);
                }
            }
        }

        private void RadGridView_DataLoaded(object sender, EventArgs e)
        {
            this.persistSelection = false;

            foreach (var item in this.selectedItems)
            {
                this.RadGridView.SelectedItems.Add(item);
            }
        }
    }
}
