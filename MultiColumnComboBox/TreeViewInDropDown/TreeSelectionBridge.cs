using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.MultiColumnComboBox;

namespace TreeViewInDropDown
{
    public class TreeSelectionBridge : SelectionBridge
    {
        private TreeViewComboBox owner;

        /// <summary>
        /// Gets the source element that is associated with the selection bridge.
        /// </summary>
        protected internal RadTreeView Source { get; set; }

        public TreeSelectionBridge(RadTreeView treeView, TreeViewComboBox owner) : base(owner)
        {
            this.owner = owner;
            this.Source = treeView;
            this.SubscribeToSourceEvents();

            foreach (var item in this.Owner.SelectedItems)
            {
                this.Source.SelectedItems.Add(item);
            }
        }

        public override void ClearSourceSelection()
        {
            this.Source.SelectedItems.Clear();
        }

        public override void ItemsDeselectedInOwner(IEnumerable<object> removedItems)
        {
            base.ItemsDeselectedInOwner(removedItems);

            foreach (var item in removedItems)
            {
                this.Source.SelectedItems.Remove(item);
            }
        }

        public override void ItemsSelectedInOwner(IEnumerable<object> addedItems)
        {
            base.ItemsSelectedInOwner(addedItems);

            foreach (var item in addedItems)
            {
                this.Source.SelectedItems.Add(item);
            }
        }

        public override void SynchronizeSelectedItemsWithSource()
        {
            this.Source.SelectedItems.Clear();
            this.IsSelectionChangeInPlace = false;

            var coercedSelectedItems = this.owner.SelectedItems.Distinct().ToList();

            foreach (var item in coercedSelectedItems)
            {
                this.Source.SelectedItems.Add(item);
            }
        }

        public override void UnsubscribeFromSourceEvents()
        {
            base.UnsubscribeFromSourceEvents();
            this.Source.SelectionChanged -= this.OnSourceSelectionChanged;
        }

        public void SubscribeToSourceEvents()
        {
            this.Source.SelectionChanged += this.OnSourceSelectionChanged;
        }

        private void OnSourceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsSelectionChangeInPlace)
            {
                return;
            }

            this.IsSelectionChangeInPlace = true;

            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                this.ItemsSelectedInSource(e.AddedItems.OfType<object>());
            }

            if (e.RemovedItems != null && e.RemovedItems.Count > 0)
            {
                this.ItemsDeselectedInSource(e.RemovedItems.OfType<object>());
            }

            this.IsSelectionChangeInPlace = false;
        }

        public override bool SynchronizeCurrentItemWithSelection()
        {
            return false;
        }

        /// <summary>
        /// Invoked when SelectedItems.Reset occurs in RadMultiColumnComboBox.
        /// </summary>
        public override void SelectedItemsResetInOwner()
        {
            this.Source.SelectedItems.Clear();

            foreach (var item in this.Owner.SelectedItems)
            {
                this.Source.SelectedItems.Add(item);
            }
            
            this.owner.InvokeSelectionChanged(this.Source.SelectedItems.ToList(), null);
        }

      
        /// <summary>
        /// Clean event subscriptions and other used resources.
        /// </summary>
        public override void CleanUp()
        {
            base.CleanUp();
            this.Source.SelectionChanged -= this.OnSourceSelectionChanged;
        }
    }
}
