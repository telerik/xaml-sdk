using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace BindingItemsSource
{
    public class ViewModel : ViewModelBase
    {
        private bool isOpen;
        private ObservableCollection<CustomMenuItem> menuItems;
        private Random randomNumberGenerator;

        public ViewModel()
        {
            this.randomNumberGenerator = new Random();
            this.MenuItems = this.GetMenuItems();
            this.AddItemCommand = new DelegateCommand(OnAddItemCommandExecuted);
            this.RemoveItemCommand = new DelegateCommand(OnRemoveItemCommandExecuted);
            this.ResetAllItemsCommand = new DelegateCommand(OnResetAllItemsCommandExecuted);
            this.IsOpen = true;
            this.Description = "Use the buttons above to manipulate the top level RadialMenuItems through the ViewModel. \n\rClick on the RadialMenuItems in the RadialMenu in order to Add/Remove from the their child items collection and to randomly change the background of third item.";
        }

        public string Description { get; set; }
        public ICommand AddItemCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
        public ICommand ResetAllItemsCommand { get; set; }

        public ObservableCollection<CustomMenuItem> MenuItems
        {
            get
            {
                return this.menuItems;
            }

            set
            {
                if (this.menuItems != value)
                {
                    this.menuItems = value;
                    this.OnPropertyChanged(() => this.MenuItems);
                }
            }
        }

        public bool IsOpen
        {
            get
            {
                return this.isOpen;
            }

            set
            {
                if (this.isOpen != value)
                {
                    this.isOpen = value;
                    this.OnPropertyChanged(() => this.IsOpen);
                }
            }
        }

        private ObservableCollection<CustomMenuItem> GetMenuItems()
        {
            var collection = new ObservableCollection<CustomMenuItem>();

            collection.Add(new CustomMenuItem
            {
                Header = "Add Child",
                GroupName = "TopLevel",
                Command = new DelegateCommand(OnAddItemCommandExecuted),
                CommandParameter = "Add Child"
            });

            collection.Add(new CustomMenuItem
            {
                Header = "Remove Child",
                GroupName = "TopLevel",
                Command = new DelegateCommand(OnRemoveItemCommandExecuted),
                CommandParameter = "Remove Child",
                ItemsSource = new ObservableCollection<CustomMenuItem> 
                {
                    new CustomMenuItem { Header = "Child Item" },
                    new CustomMenuItem { Header = "Child Item" },
                    new CustomMenuItem { Header = "Child Item" }
                }
            });

            collection.Add(new CustomMenuItem
            {
                Header = "Color Me",
                GroupName = "TopLevel",
                Command = new DelegateCommand(OnChangeBackgroundCommandExecuted),
                CommandParameter = "Color Me"
            });

            return collection;
        }

        private void OnAddItemCommandExecuted(object obj)
        {
            if (obj != null)
            {
                var menuItem = this.MenuItems.FirstOrDefault(i => (string)i.Header == (string)obj);

                if (menuItem.ItemsSource == null)
                {
                    menuItem.ItemsSource = new ObservableCollection<CustomMenuItem>();
                }

                (menuItem.ItemsSource as ObservableCollection<CustomMenuItem>).Add(new CustomMenuItem { Header = "Child Item" });
                menuItem.ToolTipContent = "Child Items Count: " + menuItem.ItemsSource.Count().ToString();
            }
            else
            {
                this.MenuItems.Insert(1, new CustomMenuItem { Header = "New Item", ContentSectorBackground = new SolidColorBrush(Colors.Red), GroupName = "TopLevel" });
            }

            this.IsOpen = true;
        }

        private void OnRemoveItemCommandExecuted(object obj)
        {
            if (obj != null)
            {
                var menuItem = this.MenuItems.FirstOrDefault(i => (string)i.Header == (string)obj);
                var items = menuItem.ItemsSource as ObservableCollection<CustomMenuItem>;
                if (items.Count > 0)
                {
                    items.RemoveAt(items.Count - 1);
                }

                menuItem.ToolTipContent = "Child Items Count: " + menuItem.ItemsSource.Count().ToString();
            }
            else
            {
                var selectedItem = this.MenuItems.FirstOrDefault(i => i.IsSelected);

                if (selectedItem != null)
                {
                    this.MenuItems.Remove(selectedItem);
                }
            }

            this.IsOpen = true;
        }

        private void OnResetAllItemsCommandExecuted(object obj)
        {
            this.MenuItems = this.GetMenuItems();
            this.IsOpen = true;
        }

        private void OnChangeBackgroundCommandExecuted(object obj)
        {
            if (obj != null)
            {
                var item = this.MenuItems.FirstOrDefault(i => (string)i.Header == (string)obj);
                item.ContentSectorBackground = new SolidColorBrush(this.GenerateRandomColor());
            }
            else
            {
                foreach (var item in this.MenuItems)
                {
                    item.ContentSectorBackground = new SolidColorBrush(this.GenerateRandomColor());
                }
            }
        }

        private Color GenerateRandomColor()
        {
            Color randColor = new Color();
            randColor.R = (byte)this.randomNumberGenerator.Next(0, 255);
            randColor.G = (byte)this.randomNumberGenerator.Next(0, 255);
            randColor.B = (byte)this.randomNumberGenerator.Next(0, 255);
            randColor.A = (byte)this.randomNumberGenerator.Next(0, 255);
            return randColor;
        }
    }
}
