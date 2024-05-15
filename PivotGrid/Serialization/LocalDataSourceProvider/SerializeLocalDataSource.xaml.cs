using Serialization.DataProviderSerializers;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Serialization
{
    /// <summary>
    /// Interaction logic for SerializeLocalDataSource.xaml
    /// </summary>
    public partial class SerializeLocalDataSource : UserControl
    {
        private string lastSerializadProvider;
        public SerializeLocalDataSource()
        {
            InitializeComponent();
            this.EnsureLoadState();
        }

        private void OnSave(object sender, System.Windows.RoutedEventArgs e)
        {
            LocalDataSourceSerializer provider = new LocalDataSourceSerializer();
            this.lastSerializadProvider = provider.Serialize(this.pivotGrid.DataProvider);
            this.EnsureLoadState();
        }

        private void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            LocalDataSourceSerializer provider = new LocalDataSourceSerializer();
            provider.Deserialize(this.pivotGrid.DataProvider, this.lastSerializadProvider);
            this.EnsureLoadState();
        }
        private bool CanLoad()
        {
            return !String.IsNullOrEmpty(this.lastSerializadProvider);
        }

        private void EnsureLoadState()
        {
            this.buttonLoad.IsEnabled = this.CanLoad();
        }
    }
}
