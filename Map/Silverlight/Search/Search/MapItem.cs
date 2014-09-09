using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace Search
{
    public class MapItem : ViewModelBase
    {
        private Location location = Location.Empty;
        private string title = null;
        private string description = null;

        public Location Location
        {
            get
            {
                return this.location;
            }

            set
            {
                this.location = value;
                this.OnPropertyChanged("Location");
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.OnPropertyChanged("Title");
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
                this.OnPropertyChanged("Description");
            }
        }
    }
}
