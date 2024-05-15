using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;
using Telerik.Windows.Controls;

namespace ShellPrism.Menu
{
    /// <summary>
    /// Interaction logic for MenuItemSave.xaml
    /// </summary>
    [Export]
    public partial class MenuItemSave : RadMenuItem
    {
        public MenuItemSave()
        {
            InitializeComponent();
        }

        [Import]
        public IEventAggregator Aggregator
        {
            get;
            set;
        }

        private void OnClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            this.Aggregator.GetEvent<SaveLayoutEvent>().Publish(null);
        }
    }
}