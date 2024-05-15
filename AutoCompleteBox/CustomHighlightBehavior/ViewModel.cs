using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace CustomHighlightBehavior
{
    public class ViewModel: ViewModelBase
    {
        public ObservableCollection<Item> Items { get; set; }

        public ViewModel()
        {
            this.Items = new ObservableCollection<Item>(this.GetItems(100));
        }

        private ObservableCollection<Item> GetItems(int size)
        {
            var result = new ObservableCollection<Item>();

            for (int i = 0; i < size; i++)
            {
                result.Add(new Item() { Name = string.Format("Item {0}", i), Id = i });
            }

            return result;
        }
    }
}
