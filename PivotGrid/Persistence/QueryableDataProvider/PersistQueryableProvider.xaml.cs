using QueryableDataProviderPersistence.ValueProviders;
using System.IO;
using System.Windows.Controls;
using Telerik.Pivot.Queryable;
using Telerik.Windows.Persistence;
using Telerik.Windows.Persistence.Services;

namespace QueryableDataProviderPersistence
{
    /// <summary>
    /// Interaction logic for PersistQueryableProvider.xaml
    /// </summary>
    public partial class PersistQueryableProvider : UserControl
    {
        Stream stream = new MemoryStream();

        public PersistQueryableProvider()
        {
            InitializeComponent();

            try
            {
                (this.Resources["QueryableProvider"] as Telerik.Pivot.Queryable.QueryableDataProvider).Source = new NorthwindDBEntities().Orders;
            }
            catch
            {

            }

            ServiceProvider.RegisterPersistenceProvider<IValueProvider>(typeof(QueryableDataProvider), new QueryableDataSourceValueProvider());
            this.EnsureLoadState();
        }

        private void OnSave(object sender, System.Windows.RoutedEventArgs e)
        {
            PersistenceManager manager = new PersistenceManager();
            this.stream = manager.Save(this.PivotGrid.DataProvider);
            this.EnsureLoadState();
        }

        private void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            this.stream.Position = 0;
            PersistenceManager manager = new PersistenceManager();
            manager.Load(this.PivotGrid.DataProvider, this.stream);
            this.EnsureLoadState();
        }

        private bool CanLoad()
        {
            return this.stream != null && this.stream.Length > 0;
        }

        private void EnsureLoadState()
        {
            this.buttonLoad.IsEnabled = this.CanLoad();
        }
    }
}
