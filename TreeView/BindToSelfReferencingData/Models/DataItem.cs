namespace BindToSelfReferencingData.Models
{
    public class DataItem
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
        public DataItemCollection Owner
        {
            get;
            protected set;
        }
        internal void SetOwner(DataItemCollection collection)
        {
            this.Owner = collection;
        }
    }
}
