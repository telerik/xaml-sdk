using System;
using Telerik.Windows.Controls;

namespace EditableTabHeaders
{
    public class TabItemModel : ViewModelBase
    {
        private String name;
        private String content;
        public String Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public String Content
        {
            get
            {
                return this.content;
            }
            set
            {
                if (this.content != value)
                {
                    this.content = value;
                    OnPropertyChanged("Content");
                }
            }
        }
    }
}
