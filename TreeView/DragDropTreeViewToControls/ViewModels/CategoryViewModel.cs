using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace DragDropTreeViewToControls.ViewModels
{
    public class CategoryViewModel
    {
        private string _title;
        private IList _items;

        public static IList Generate()
        {
            CategoryViewModel latest = new CategoryViewModel();
            latest.Name = "Latest Additions";
            foreach (ProductViewModel item in ProductViewModel.Generate(4))
            {
                latest.Items.Add(item);
            }

            CategoryViewModel highestRated = new CategoryViewModel();
            highestRated.Name = "Highest Rated";
            foreach (ProductViewModel item in ProductViewModel.Generate(5))
            {
                highestRated.Items.Add(item);
            }

            CategoryViewModel onSale = new CategoryViewModel();
            onSale.Name = "On Sale";
            foreach (ProductViewModel item in ProductViewModel.Generate(6))
            {
                onSale.Items.Add(item);
            }

            CategoryViewModel value = new CategoryViewModel();
            value.Name = "Instant Deal";
            value.Items.Add(onSale);
            foreach (ProductViewModel item in ProductViewModel.Generate(3))
            {
                value.Items.Add(item);
            }

            ObservableCollection<object> result = new ObservableCollection<object>();

            result.Add(latest);
            result.Add(highestRated);
            result.Add(value);

            return result;
        }

        public CategoryViewModel()
        {
            Items = new ObservableCollection<object>();
        }

        public string Name
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }
        public IList Items
        {
            get
            {
                return _items;
            }
            set
            {
                this._items = value;
            }
        }
    }

}
