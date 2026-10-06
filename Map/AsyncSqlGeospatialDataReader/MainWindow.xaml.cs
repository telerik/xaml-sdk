using System.Windows;

namespace AsyncSqlGeospatialDataReader
{  
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.asyncSqlGeospatialDataReader.Source = WktDataStorage.GetData();
        }
    }
}
