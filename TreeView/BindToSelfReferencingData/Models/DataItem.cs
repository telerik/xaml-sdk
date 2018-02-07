using Telerik.Windows.Controls;

namespace BindToSelfReferencingData.Models
{
    public class DataItem : ViewModelBase
    {
        public int Id
        {
            get;
            set;
        }
        public int ParentId
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }

        public DataItemCollection OwnerCollection
        {
            get;
            protected set;
        }

        internal void SetOwnerCollection(DataItemCollection collection)
        {
            this.OwnerCollection = collection;
        }
    }
}
