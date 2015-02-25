using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace CustomFiltering
{
    public class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            this.DataItems = new ObservableCollection<DataItem>
                {
                    new DataItem { Title = "Globex Corporation"},
                    new DataItem { Title = "Atlantic Northern"},
                    new DataItem { Title = "Roboto Industries"},
                    new DataItem { Title = "Galaxy Corp"},
                    new DataItem { Title = "Wayne Enterprises"},
                    new DataItem { Title = "Acme, inc."},
                    new DataItem { Title = "Consolidated Holdings"},
                };
        }

        public ObservableCollection<DataItem> DataItems { get; private set; }
    }
}