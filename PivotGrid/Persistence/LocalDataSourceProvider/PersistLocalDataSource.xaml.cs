using Persistence.ValueProviders;
using System;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Telerik.Pivot.Core;
using Telerik.Windows.Persistence;
using Telerik.Windows.Persistence.Services;

namespace Persistence
{
    /// <summary>
    /// Interaction logic for PersistLocalDataSource.xaml
    /// </summary>
    public partial class PersistLocalDataSource : UserControl
    {
        Stream stream = new MemoryStream();
        public PersistLocalDataSource()
        {
            InitializeComponent();

            ServiceProvider.RegisterPersistenceProvider<IValueProvider>(typeof(LocalDataSourceProvider), new LocalDataSourceValueProvider());
            this.EnsureLoadState();
        }

         private void OnSave(object sender, System.Windows.RoutedEventArgs e)
         {
             PersistenceManager manager = new PersistenceManager();
             this.stream = manager.Save(this.pivotGrid.DataProvider);
             this.EnsureLoadState();
         }

         private void OnLoad(object sender, System.Windows.RoutedEventArgs e)
         {
             this.stream.Position = 0;
             PersistenceManager manager = new PersistenceManager();
             manager.Load(this.pivotGrid.DataProvider, this.stream);
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
