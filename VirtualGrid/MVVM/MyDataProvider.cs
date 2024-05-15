using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.VirtualGrid;

namespace MVVM_VirtualGrid
{
    public class MyDataProvider : DataProvider
    {
        private List<Club> selectedItems;
        private int currentCellColumnIndex;

        public MyDataProvider(IEnumerable source) : base(source)
        {
            this.selectedItems = new List<Club>();
        }

        //At this state the SelectedIndexes are not cleared yet and can be persisted.
        protected override void SortDescriptorPreparing(SortingEventArgs e)
        {
            var selectedIndexes = this.ParentGrid.SelectedIndexes.ToList();

            if (selectedIndexes.Count > 0)
            {
                this.StoreSelectedItems(selectedIndexes);
            }
            base.SortDescriptorPreparing(e);
        }

        //Update the selection after the sorting is processed.
        protected override void OnSortingCompleted()
        {
            base.OnSortingCompleted();
            this.UpdateSelection();
        }

        //Hide the primary key property
        public override IList<ItemPropertyInfo> ItemProperties
        {
            get
            {
                return base.ItemProperties.Skip(1).ToList();
            }
        }

        protected override void OnHeaderValueNeeded(HeaderValueEventArgs e)
        {
            if (e.HeaderOrientation == VirtualGridOrientation.Horizontal)
            {
                int propertyIndex = this.GetPropertyIndex("Name");

                if (e.Index == propertyIndex)
                {
                    e.Value = "Club Name";
                }
                else
                {
                    base.OnHeaderValueNeeded(e);
                }
            }
        }

        protected override void OnEditorNeeded(EditorNeededEventArgs args)
        {
            int propertyIndex = this.GetPropertyIndex("Established");
            if (args.ColumnIndex == propertyIndex)
            {
                var editor = new RadDateTimePicker();
                var item = (this.Source.GetItemAt(args.RowIndex) as Club);

                args.Editor = editor;
                editor.SelectedValue = item.Established;
                args.EditorProperty = RadDateTimePicker.SelectedValueProperty;
            }
            else
            {
                base.OnEditorNeeded(args);
            }

        }

        protected override bool IsColumnReadOnly(int columnIndex)
        {
            int propertyIndex = this.GetPropertyIndex("Name");
            if (columnIndex == propertyIndex)
            {
                return true;
            }

            return base.IsColumnReadOnly(columnIndex);
        }

        /// <summary>
        /// Gets the index of a given property
        /// </summary>
        /// <param name="propertyName">The property name</param>
        /// <returns>The property index</returns>
        private int GetPropertyIndex(string propertyName)
        {
            ItemPropertyInfo itemProperty = this.ItemProperties.Where(ip => ip.Name == propertyName).FirstOrDefault();
            int index = this.ItemProperties.IndexOf(itemProperty);
            
            return index;
        }

        /// <summary>
        /// Adds the previously persisted indexes
        /// </summary>
        /// <param name="selectedIndexes"></param>
        private void UpdateSelection()
        {
            int index;
            foreach (Club item in this.selectedItems)
            {
                index = this.Source.IndexOf(item);
                this.ParentGrid.ToggleIndexSelection(index);
            }
        }

        private void StoreSelectedItems(IEnumerable<int> selectedIndexes)
        {
            this.selectedItems.Clear();
            foreach (var index in selectedIndexes)
            {
                Club item = this.Source.GetItemAt(index) as Club;
                this.selectedItems.Add(item);
            }   
        }
    }
}
