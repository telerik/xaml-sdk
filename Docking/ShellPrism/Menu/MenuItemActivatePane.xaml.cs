using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace ShellPrism.Menu
{
    /// <summary>
    /// Interaction logic for MenuItemActivatePane.xaml
    /// </summary>
    [Export]
    public partial class MenuItemActivatePane : RadMenuItem
    {
        public MenuItemActivatePane()
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
            this.Aggregator.GetEvent<ActivateViewEvent>().Publish("Output");
        }
    }
}