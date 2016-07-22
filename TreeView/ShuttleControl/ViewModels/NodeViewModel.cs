using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace ShuttleControl
{
    public class NodeViewModel : ViewModelBase
    {
        private NodeViewModel parent;
        private string header;
        private bool isSelected;
        private bool isSelectable;
        private bool isExpanded;
        private string imagePath;
        private ObservableCollection<NodeViewModel> children;

        public NodeViewModel(string header, NodeViewModel parent, bool isSelected, bool isExpanded, string imagePath, bool isSelectable = true)
        {
            this.Header = header;
            this.Parent = parent;
            this.IsSelected = isSelected;
            this.IsExpanded = isExpanded;
            this.ImagePath = imagePath;
            this.Children = new ObservableCollection<NodeViewModel>();
            this.IsSelectable = isSelectable;
        }

        public NodeViewModel Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                if (this.parent != value)
                {
                    this.parent = value;
                }
            }
        }

        public string Header
        {
            get
            {
                return this.header;
            }
            set
            {
                if (this.header != value)
                {
                    this.header = value;
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (this.isSelected != value)
                {
                    if (!this.IsSelectable && value)
                    {
                        return;
                    }
                    this.isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public bool IsSelectable
        {
            get
            {
                return this.isSelectable;
            }
            set
            {
                if (this.isSelectable != value)
                {
                    this.isSelectable = value;
                    OnPropertyChanged("IsSelectable");
                }
            }
        }

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
                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }
            set
            {
                if (this.imagePath != value)
                {
                    this.imagePath = value;
                }
            }
        }

        public ObservableCollection<NodeViewModel> Children
        {
            get
            {
                return this.children;
            }
            set
            {
                if (this.children != value)
                {
                    this.children = value;
                    OnPropertyChanged("Children");
                }
            }
        }
    }
}
