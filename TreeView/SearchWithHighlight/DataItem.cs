using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace SearchWithHighlight_WPF
{
    public class DataItem : ViewModelBase
    {
        private List<InlineModel> headerParts;

        private string header;

        public string Header
        {
            get { return header; }
            set
            {
                this.header = value;
                this.UpdateInlines(string.Empty);
            }
        }

        public ObservableCollection<DataItem> Children { get; set; }

        public List<InlineModel> HeaderParts
        {
            get
            {
                return headerParts;
            }
            private set
            {
                this.headerParts = value;
                this.OnPropertyChanged("HeaderParts");
            }
        }

        public DataItem()
        {
            this.Children = new ObservableCollection<DataItem>();
        }

        public void UpdateInlines(string searchText)
        {
            int searchStartIndex = this.Header.ToLower().IndexOf(searchText.ToLower());
            if (searchStartIndex == -1 | string.IsNullOrEmpty(searchText))
            {
                this.HeaderParts = new List<InlineModel>() { new InlineModel() { Text = this.Header } };
                return;
            }

            int searchEndIndex = searchStartIndex + searchText.Length;
            int searchRange = searchEndIndex - searchStartIndex;

            var inline1 = new InlineModel();
            if (searchStartIndex - 1 != -1)
            {
                inline1.Text = this.Header.Substring(0, searchStartIndex);
            }

            var inline2 = new InlineModel() { Text = this.Header.Substring(searchStartIndex, searchRange), Background = Brushes.Bisque };
            var inline3 = new InlineModel() { Text = this.Header.Substring(searchEndIndex) };

            this.HeaderParts = new List<InlineModel>()
            {
                inline1,
                inline2,
                inline3,
            };
        }
    }
}
