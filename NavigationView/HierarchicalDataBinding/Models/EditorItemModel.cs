using System.Collections.ObjectModel;

namespace HierarchicalDataBinding.Models
{
    public class EditorItemModel : ControlItemModel
    {
        private ObservableCollection<string> editorItemsSource;

        public ObservableCollection<string> EditorItemsSource
        {
            get
            {
                if(this.editorItemsSource == null)
                {
                    this.editorItemsSource = new ObservableCollection<string>() { "Germany", "Italy", "France", "Greece", "United States", "United Kingdom", "Bulgaria" };
                }

                return this.editorItemsSource;
            }
            
        }

    }

}
