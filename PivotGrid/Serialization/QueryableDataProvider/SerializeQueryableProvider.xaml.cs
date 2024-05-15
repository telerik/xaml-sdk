using QueryableDataProviderSerialization.DataProviderSerializers;
using System;
using System.Windows.Controls;

namespace QueryableDataProviderSerialization
{
    /// <summary>
    /// Interaction logic for PersistQueryableProvider.xaml
    /// </summary>
    public partial class SerializeQueryableProvider : UserControl
    {
        private string lastSerializedProvider;

        public SerializeQueryableProvider()
        {
            InitializeComponent();

            try
            {
                (this.Resources["QueryableProvider"] as Telerik.Pivot.Queryable.QueryableDataProvider).Source = new NorthwindDBEntities().Orders;
            }
            catch
            {

            }

            this.EnsureLoadState();
        }

        private void OnSave(object sender, System.Windows.RoutedEventArgs e)
        {
            QueryableProviderSerializer serializer = new QueryableProviderSerializer();
            this.lastSerializedProvider = serializer.Serialize(this.PivotGrid.DataProvider);
            this.EnsureLoadState();
        }

        private void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            QueryableProviderSerializer serializer = new QueryableProviderSerializer();
            serializer.Deserialize(this.PivotGrid.DataProvider, this.lastSerializedProvider);
            this.EnsureLoadState();
        }

        private bool CanLoad()
        {
            return !String.IsNullOrEmpty(this.lastSerializedProvider);
        }

        private void EnsureLoadState()
        {
            this.buttonLoad.IsEnabled = this.CanLoad();
        }
    }
}
