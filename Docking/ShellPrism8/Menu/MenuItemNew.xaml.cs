using Prism.Events;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace ShellPrism8.Menu
{
    /// <summary>
    /// Interaction logic for MenuItemNew.xaml
    /// </summary>
   
    public partial class MenuItemNew : RadMenuItem
    {
        public MenuItemNew(IEventAggregator eventAggregator)
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
            this.Aggregator.GetEvent<CreateDocumentEvent>().Publish("New Document");
        }
    }
}