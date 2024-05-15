using Prism.Events;
using Telerik.Windows.Controls;

namespace ShellPrism8.Menu
{
    /// <summary>
    /// Interaction logic for MenuItemLoad.xaml
    /// </summary>  
    public partial class MenuItemLoad : RadMenuItem
    {
        public MenuItemLoad(IEventAggregator eventAggregator)
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
            this.Aggregator.GetEvent<LoadLayoutEvent>().Publish(null);
        }
    }
}