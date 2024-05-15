using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BindingToDynamicData
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = this.GetMenuItems();
        }

        public ObservableCollection<MenuItem> GetMenuItems()
        {
            ObservableCollection<MenuItem> items = new ObservableCollection<MenuItem>();

            MenuItem copyItem = new MenuItem()
            {
                IconUrl = new Uri("Images/copy.png", UriKind.Relative),
                Text = "Copy",
                Command = new CopyCommand()
            };
            items.Add(copyItem);

            MenuItem pasteItem = new MenuItem()
            {
                IconUrl = new Uri("Images/paste.png", UriKind.Relative),
                Text = "Paste",
                Command = new PasteCommand()
            };
            items.Add(pasteItem);

            MenuItem cutItem = new MenuItem()
            {
                IconUrl = new Uri("Images/cut.png", UriKind.Relative),
                Text = "Cut",
                Command = new CutCommand()
            };
            items.Add(cutItem);

            return items;
        }
    }
}
