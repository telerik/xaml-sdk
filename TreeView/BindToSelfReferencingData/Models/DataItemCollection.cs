using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BindToSelfReferencingData.Models
{
    public class DataItemCollection : ObservableCollection<DataItem>
    {
        public DataItemCollection()
            : base()
        {
        }

        public DataItemCollection(IEnumerable<DataItem> collection)
            : base(collection)
        {
        }

        public DataItem AssociatedItem
        {
            get;
            protected set;
        }

        public void SetAssociatedItem(DataItem item)
        {
            this.AssociatedItem = item;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (DataItem item in e.NewItems)
                {
                    if (this.AssociatedItem != null && item.ParentId != this.AssociatedItem.Id)
                    {
                        item.ParentId = this.AssociatedItem.Id;
                    }                    
                }
            }
        }
    }
}
