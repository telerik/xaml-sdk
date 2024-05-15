using BindToSelfReferencingData.Models;
using System.Windows;

namespace BindToSelfReferencingData
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var source =  new DataItemCollection()
            {                
                 new DataItem () { Text = "Item 1", Id = 1, ParentId = 0 },
                 new DataItem () { Text = "Item 2", Id = 2, ParentId = 0 },
                 new DataItem () { Text = "Item 3", Id = 3, ParentId = 0 },
                 new DataItem () { Text = "Item 1.1", Id = 5, ParentId = 1 },
                 new DataItem () { Text = "Item 1.2", Id = 6, ParentId = 1 },
                 new DataItem () { Text = "Item 1.3", Id = 7, ParentId = 1 },
                 new DataItem () { Text = "Item 2.1", Id = 8, ParentId = 2 },
                 new DataItem () { Text = "Item 2.2", Id = 9, ParentId = 2 },
                 new DataItem () { Text = "Item 2.3", Id = 10, ParentId = 2 },
                 new DataItem () { Text = "Item 3.1", Id = 11, ParentId = 3 },
                 new DataItem () { Text = "Item 3.2", Id = 12, ParentId = 3 },
                 new DataItem () { Text = "Item 3.3", Id = 13, ParentId = 3, }                 
            };

            foreach (var item in source)
            {
                item.SetOwnerCollection(source);
            }

            this.DataContext = source;
        }
    }
}
