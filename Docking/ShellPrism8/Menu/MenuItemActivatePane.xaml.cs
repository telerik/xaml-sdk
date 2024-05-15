using Prism.Events;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace ShellPrism8.Menu
{
    /// <summary>
    /// Interaction logic for MenuItemActivatePane.xaml
    /// </summary>
    
    public partial class MenuItemActivatePane : RadMenuItem
    {
        public MenuItemActivatePane(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.Aggregator = eventAggregator;
        }

       
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