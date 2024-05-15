using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace ShellPrism.Menu
{
    /// <summary>
    /// Interaction logic for MenuItemNew.xaml
    /// </summary>
    [Export]
    public partial class MenuItemNew : RadMenuItem
    {
        public MenuItemNew()
        {
            InitializeComponent();
        }

        [Import]
        public IEventAggregator Aggregator
        {
            get;
            set;
        }

        private void OnClick(object sender, RadRoutedEventArgs e)
        {
            this.Aggregator.GetEvent<CreateDocumentEvent>().Publish("New Document");
        }
    }
}