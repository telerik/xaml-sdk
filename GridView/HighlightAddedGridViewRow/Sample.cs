using Telerik.Windows.Controls;

namespace HighlightAddedGridViewRow
{
    public class Sample : ViewModelBase
    {
        private int id;
        private string data;
        private bool isNew;

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                this.OnPropertyChanged("ID");
            }
        }

        public string Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
                this.OnPropertyChanged("Data");
            }
        }

        public bool IsNew
        {
            get
            {
                return this.isNew;
            }
            set
            {
                this.isNew = value;
                this.OnPropertyChanged("IsNew");
            }
        }
    }
}
