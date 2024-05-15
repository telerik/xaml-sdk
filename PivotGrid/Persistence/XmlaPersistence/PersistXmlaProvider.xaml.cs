using Persistence.ValueProviders;
using System;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Telerik.Pivot.Xmla;
using Telerik.Windows.Persistence;
using Telerik.Windows.Persistence.Services;

namespace Persistence
{
    /// <summary>
    /// Interaction logic for PersistXmlaProvider.xaml
    /// </summary>
    public partial class PersistXmlaProvider : UserControl
    {
       private Stream stream = new MemoryStream();
       public PersistXmlaProvider()
        {
            InitializeComponent();
            ServiceProvider.RegisterPersistenceProvider<IValueProvider>(typeof(XmlaDataProvider), new XmlaValueProvider());
            this.EnsureLoadState();
        }

        private void OnSave(object sender, System.Windows.RoutedEventArgs e)
        {
            PersistenceManager manager = new PersistenceManager();
            this.stream = manager.Save(this.pivot.DataProvider);
            this.EnsureLoadState();
        }

        private void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            this.stream.Position = 0;
            PersistenceManager manager = new PersistenceManager();
            manager.Load(this.pivot.DataProvider, this.stream);
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
