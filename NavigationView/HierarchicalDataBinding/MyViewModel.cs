using HierarchicalDataBinding.Models;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace HierarchicalDataBinding
{
    public class MyViewModel : ViewModelBase
    {
        private ObservableCollection<NavigationItemModel> items;

        public ObservableCollection<NavigationItemModel> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = this.GenerateItems();
                }

                return this.items;
            }
        }

        public ObservableCollection<NavigationItemModel> GenerateItems()
        {
            var source = new ObservableCollection<NavigationItemModel>()
            {
                new NavigationItemModel()
                {
                    Title = "Editors",
                    IconGlyph = "&#xe10b;",
                    Children = new ObservableCollection<NavigationItemModel>()
                    {
                        new EditorItemModel() { Title = "RadComboBox", DocumentationLink = "https://docs.telerik.com/devtools/wpf/controls/radcombobox/getting-started" },
                        new EditorItemModel() { Title = "RadAutoCompleteBox", DocumentationLink = "https://docs.telerik.com/devtools/wpf/controls/radautocompletebox/getting-started" },
                    }
                },
                new NavigationItemModel()
                {
                    Title = "Layout controls",
                    IconGlyph = "&#xe024;",
                    Children = new ObservableCollection<NavigationItemModel>()
                    {
                        new LayoutControlItemModel() { Title = "RadTileView", DocumentationLink = "https://docs.telerik.com/devtools/wpf/controls/radtileview/getting-started/getting-started" },
                        new LayoutControlItemModel() { Title = "RadTileList", DocumentationLink = "https://docs.telerik.com/devtools/wpf/controls/radtilelist/getting-started/getting-started" },
                    }
                }
            };

            return source;
        }
    }
}
