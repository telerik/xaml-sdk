using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using Telerik.Windows.Controls;

namespace ChangeThemeRuntime.ViewModels
{
    public class TreeListViewModel : ViewModelBase
    {
        private string name;
        private bool isEmpty;
        private DateTime createdOn;
        private ObservableCollection<TreeListViewModel> items;
        private readonly XElement folderElement;

        public TreeListViewModel(XElement element)
        {
            this.folderElement = element;
        }

        public TreeListViewModel(string name, bool isEmpty, DateTime createdOn, XElement element)
        {
            this.Name = name;
            this.IsEmpty = isEmpty;
            this.folderElement = element;
            this.CreatedOn = createdOn;
            this.items = new ObservableCollection<TreeListViewModel>(from f in this.folderElement.Elements("folder")
                                                                     select new TreeListViewModel(
                                                                         f.Attribute("Name").Value,
                                                                         bool.Parse(f.Attribute("IsEmpty").Value),
                                                                         DateTime.Parse(f.Attribute("CreationTime").Value, System.Globalization.CultureInfo.InvariantCulture),
                                                                         f
                                                                     ));
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public DateTime CreatedOn
        {
            get
            {
                return this.createdOn;
            }
            set
            {
                this.createdOn = value;
            }
        }

        [Display(AutoGenerateField = false)]
        public bool IsEmpty
        {
            get
            {
                return this.isEmpty;
            }
            set
            {
                this.isEmpty = value;
            }
        }

        [Display(AutoGenerateField = false)]
        public ObservableCollection<TreeListViewModel> Items
        {
            get
            {
                return this.items;
            }
        }

        private bool isExpanded;

        [Display(AutoGenerateField = false)]
        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }
            set
            {
                if (this.isExpanded != value)
                {
                    this.isExpanded = value;

                    this.LoadChildren();

                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        public void LoadChildren()
        {
            if (this.items == null)
            {
                this.items = new ObservableCollection<TreeListViewModel>(from f in this.folderElement.Elements("folder")
                                                                         select new TreeListViewModel(f)
                                                                         {
                                                                             Name = f.Attribute("Name").Value,
                                                                             IsEmpty = bool.Parse(f.Attribute("IsEmpty").Value),
                                                                             CreatedOn = DateTime.Parse(f.Attribute("CreationTime").Value, System.Globalization.CultureInfo.InvariantCulture),
                                                                         });
                this.OnPropertyChanged("Items");
            }
        }
    }
}
