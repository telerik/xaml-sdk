using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace SearchWithHighlight_WPF
{
    public class MainViewModel : ViewModelBase
    {
        private readonly List<DataItem> searchResults = new List<DataItem>();
        public DelegateCommand SearchCommand { get; set; }
        public ObservableCollection<DataItem> Items { get; set; }

        public MainViewModel()
        {
            this.Items = new ObservableCollection<DataItem>();
            this.SearchCommand = new DelegateCommand(OnSearchExecute);

            DataItem blausUSA = new DataItem() { Header = "BLAUS USA", Children = new ObservableCollection<DataItem>() } ;
            blausUSA.Children.Add(new DataItem() { Header="Nancy Davolio" });
            blausUSA.Children.Add(new DataItem() { Header="Andrew Fuller" });
            blausUSA.Children.Add(new DataItem() { Header="Janet Leverling" });
            blausUSA.Children.Add(new DataItem() { Header="Margaret Peacock" });
            blausUSA.Children.Add(new DataItem() { Header="Steven Buchanan" });

            DataItem dumonCorporation = new DataItem() { Header = "DUMON CORPORATION", Children = new ObservableCollection<DataItem>() };
            dumonCorporation.Children.Add(new DataItem() { Header = "Michael Suyama" });
            dumonCorporation.Children.Add(new DataItem() { Header = "Robert King" });

            DataItem fissGroup = new DataItem() { Header = "FISS GROUP", Children = new ObservableCollection<DataItem>() };
            fissGroup.Children.Add(new DataItem() { Header = "Laura Callahan" });
            fissGroup.Children.Add(new DataItem() { Header = "Anne Dodsworth" });

            this.Items.Add(blausUSA);
            this.Items.Add(dumonCorporation);
            this.Items.Add(fissGroup);           
        }

        private void OnSearchExecute(object obj)
        {
            string searchText = (string)obj;

            foreach (var item in this.searchResults)
            {
                item.UpdateInlines(string.Empty);
            }
            this.searchResults.Clear();

            var stack = new Stack<DataItem>();
            foreach (DataItem item in this.Items)
            {
                stack.Push(item);

                while (stack.Count > 0)
                {
                    DataItem currentItem = stack.Pop();
                    foreach (var child in currentItem.Children)
                    {
                        stack.Push(child);
                    }

                    if (currentItem.Header.ToLower().Contains(searchText.ToLower()))
                    {
                        this.searchResults.Add(currentItem);
                        currentItem.UpdateInlines(searchText);
                    }
                }
            }
        }
    }
}
