using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace AddHeaderIcon
{
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();

            var source = new ObservableCollection<CategoryModel>();
            for (int i = 1; i <= 3; i++)
            {
                source.Add(new CategoryModel()
                {
                    Header = "Category " + i,
                    ImageSourcePath = "../../Images/" + i + ".png",
                    Items = new ObservableCollection<ItemModel>()
                    {
                        new ItemModel() { Content = "Item 1", },
                        new ItemModel() { Content = "Item 2", },
                        new ItemModel() { Content = "Item 3", },
                    },
                });                
            }

            this.DataContext = source;
        }
    }

    public class CategoryModel
    {
        public string Header { get; set; }
        public string ImageSourcePath { get; set; }
        public ObservableCollection<ItemModel> Items { get; set; }
    }

    public class ItemModel
    {
        public string Content { get; set; }
    }
}
