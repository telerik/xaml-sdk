using Prism.Regions;
using System.Windows.Controls;

namespace NavigationViewWithPrism.Views
{
    public partial class ViewA : UserControl, INavigationAware
    {
        public ViewA()
        {
            InitializeComponent();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var item = navigationContext.Parameters["Item"];
            this.DataContext = item;
        }
    }
}
