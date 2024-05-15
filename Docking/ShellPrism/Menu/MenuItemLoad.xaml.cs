using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;
using Telerik.Windows.Controls;

namespace ShellPrism.Menu
{
    /// <summary>
    /// Interaction logic for MenuItemLoad.xaml
    /// </summary>
    [Export]
    public partial class MenuItemLoad : RadMenuItem
    {
        public MenuItemLoad()
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
            this.Aggregator.GetEvent<LoadLayoutEvent>().Publish(null);
        }
    }
}