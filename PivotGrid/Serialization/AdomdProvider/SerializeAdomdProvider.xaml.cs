using Serialization.DataProviderSerializers;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Serialization
{
    /// <summary>
    /// Interaction logic for SerializeAdomdProvider.xaml
    /// </summary>
    public partial class SerializeAdomdProvider : UserControl
    {
        private string lastSerializedProvider;
        public SerializeAdomdProvider()
        {
            InitializeComponent();
            this.EnsureLoadState();
        }

        private void OnSave(object sender, System.Windows.RoutedEventArgs e)
        {
            AdomdProviderSerializer provider = new AdomdProviderSerializer();
            this.lastSerializedProvider = provider.Serialize(this.pivot.DataProvider);
            this.EnsureLoadState();
        }

        private void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            AdomdProviderSerializer provider = new AdomdProviderSerializer();
            provider.Deserialize(this.pivot.DataProvider, this.lastSerializedProvider);
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
