using System.Collections.ObjectModel;
using System.Windows;

namespace EmptyGridMessageContent
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        } 
        
        public ObservableCollection<RowInfo> GetData()
        {
            var result = new ObservableCollection<RowInfo>();
            for (int i = 0; i < 100; i++)
            {
                result.Add(new RowInfo() { Id = i, Name = "Item " + i });
            }
            return result;
        }

        private void OnReloadItems(object sender, RoutedEventArgs e)
        {
            this.gridView.ItemsSource = GetData();
        }

        private void OnClearItems(object sender, RoutedEventArgs e)
        {
            this.gridView.ItemsSource = null;
        }
    }   
}
