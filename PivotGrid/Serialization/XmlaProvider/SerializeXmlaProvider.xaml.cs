using Serialization.DataProviderSerializers;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Serialization
{
    /// <summary>
    /// Interaction logic for SerializeXmlaProvider.xaml
    /// </summary>
    public partial class SerializeXmlaProvider : UserControl
    {
        private string lastSerializedProvider;
        public SerializeXmlaProvider()
        {
            InitializeComponent();
            this.EnsureLoadState();
        }

        private void OnSave(object sender, System.Windows.RoutedEventArgs e)
        {
            XmlaProviderSerializer provider = new XmlaProviderSerializer();
            this.lastSerializedProvider = provider.Serialize(this.pivot.DataProvider);
            this.EnsureLoadState();
        }

        private void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            XmlaProviderSerializer provider = new XmlaProviderSerializer();
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
