using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace TreeViewInDropDown
{
    public class MyViewModel : ViewModelBase
    {
        private ObservableCollection<Item> items;

        public ObservableCollection<Item> Items
        {
            get
            {
                if(this.items == null)
                {
                    this.items = Item.CreateItems();
                }

                return this.items;
            }
            
        }
    }

    public class Item : ViewModelBase
    {
        public string Name { get; set; }

        public static ObservableCollection<Item> CreateItems()
        {
            var items = new ObservableCollection<Item>();

            for (int i = 0; i < 5; i++)
            {
                var item = new Item() { Name = "Item " + i };
                for (int j = 0; j < 5; j++)
                {
                    item.Items.Add(new Item() { Name = $"Item {i}.{j}" });
                }

                items.Add(item);
            }


            return items;
        }

        private ObservableCollection<Item> items = new ObservableCollection<Item>();

        public ObservableCollection<Item> Items
        {
            get { return items; }
            set
            {
                items = value;
                this.OnPropertyChanged("Items");
            }
        } 
    }
}
