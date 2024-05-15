using Prism.Regions;
using System.Collections.ObjectModel;

namespace NavigationViewWithPrism.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly string[] glyphStrings = new string[] { "&#xe501;", "&#xe13d;" };
        private readonly string[] viewNames = new string[] { "ViewA", "ViewB" };

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.Items = new ObservableCollection<NavigationItemModel>();
            for (int i = 1; i <= 2; i++)
            {
                var glyphString = this.glyphStrings[i - 1];
                this.Items.Add(new NavigationItemModel() { Title = "Item " + i, IconGlyph = glyphString, ViewName = viewNames[i - 1] });
            }

            this.SelectionChangedCommand = new Telerik.Windows.Controls.DelegateCommand(OnSelectionChanged);
            this.RegionManager = regionManager;
        }

        public ObservableCollection<NavigationItemModel> Items { get; set; }
        public Telerik.Windows.Controls.DelegateCommand SelectionChangedCommand { get; }
        public IRegionManager RegionManager { get; }

        private void OnSelectionChanged(object obj)
        {
            var item = obj as NavigationItemModel;
            var parameters = new NavigationParameters();
            parameters.Add("Item", item);
            this.RegionManager.RequestNavigate("ContentRegion", item.ViewName, parameters);
        }
    }
}
