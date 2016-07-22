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
                    Items = new ObservableCollection<string>()
                    {
                        "Item 1", "Item 2", "Item 3", 
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
        public ObservableCollection<string> Items { get; set; }
    }
}
