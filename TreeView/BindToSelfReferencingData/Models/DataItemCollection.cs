using System.Collections.ObjectModel;

namespace BindToSelfReferencingData.Models
{
    public class DataItemCollection : ObservableCollection<DataItem>
    {
        protected override void InsertItem(int index, DataItem item)
        {   
            this.AdoptItem(item);
            base.InsertItem(index, item);
        }
        protected override void RemoveItem(int index)
        {
            this.DiscardItem(this[index]);
            base.RemoveItem(index);
        }
        protected override void SetItem(int index, DataItem item)
        {
            this.AdoptItem(item);
            base.SetItem(index, item);
        }
        protected override void ClearItems()
        {
            foreach (DataItem item in this)
            {
                this.DiscardItem(item);
            }
            base.ClearItems();
        }
        private void AdoptItem(DataItem item)
        {
            item.SetOwner(this);
        }
        private void DiscardItem(DataItem item)
        {
            item.SetOwner(null);
        }
    }
}
