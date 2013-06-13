using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BindingToDynamicData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
                Command = ApplicationCommands.Copy
            };
            items.Add(copyItem);
            MenuItem pasteItem = new MenuItem()
            {
                IconUrl = new Uri("Images/paste.png", UriKind.Relative),
                Text = "Paste",
                Command = ApplicationCommands.Paste
            };
            items.Add(pasteItem);
            MenuItem cutItem = new MenuItem()
            {
                IconUrl = new Uri("Images/cut.png", UriKind.Relative),
                Text = "Cut",
                Command = ApplicationCommands.Cut
            };
            items.Add(cutItem);

            return items;
        }
    }
}
