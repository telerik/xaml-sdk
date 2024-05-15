using Prism.Events;
using Telerik.Windows.Controls;

namespace ShellPrism8.Menu
{
    /// <summary>
    /// Interaction logic for MenuItemSave.xaml
    /// </summary>
    public partial class MenuItemSave : RadMenuItem
    {
        public MenuItemSave(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.Aggregator = eventAggregator;
        }

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