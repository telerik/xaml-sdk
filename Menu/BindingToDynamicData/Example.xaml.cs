using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BindingToDynamicData
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
            this.DataContext = this.GetMenuItems();
        }

        public ObservableCollection<MenuItem> GetMenuItems()
        {
            ObservableCollection<MenuItem> items = new ObservableCollection<MenuItem>();
            ObservableCollection<MenuItem> fileSubItems = new ObservableCollection<MenuItem>();

            MenuItem newItem = new MenuItem()
            {
                IconUrl = new Uri("Images/newFile.png", UriKind.Relative),
                Text = "New File"
            };
            fileSubItems.Add(newItem);

            MenuItem openItem = new MenuItem()
            {
                IconUrl = new Uri("Images/open.png", UriKind.Relative),
                Text = "Open File"
            };
            fileSubItems.Add(openItem);

            MenuItem saveItem = new MenuItem()
            {
                IconUrl = new Uri("Images/save.png", UriKind.Relative),
                Text = "Save File"
            };
            fileSubItems.Add(saveItem);

            MenuItem fileItem = new MenuItem()
            {
                SubItems = fileSubItems,
                Text = "File"
            };
            items.Add(fileItem);

            MenuItem editItem = new MenuItem()
            {
                Text = "Edit"
            };
            items.Add(editItem);

            MenuItem viewItem = new MenuItem()
            {
                Text = "View"
            };
            items.Add(viewItem);

            return items;
        }
    }
}
