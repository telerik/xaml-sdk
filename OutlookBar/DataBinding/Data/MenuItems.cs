using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.Data
{
    public class MenuItemBase
    {
        public string Header { get; set; }
        public string Content { get; set; }
        public string IconSource { get; set; }
        public string IconSourceSmall { get; set; }

        public override string ToString()
        {
            return String.Empty;
        }
    }

    public class MailMenuItem : MenuItemBase
    {
        public ObservableCollection<MailDirectoryItem> MailDirectories { get; set; }
        public MailMenuItem()
        {
            this.MailDirectories = new ObservableCollection<MailDirectoryItem>();
        }
    }

    public class MailDirectoryItem
    {
        public string Header { get; set; }
        public string IconSource { get; set; }
        public ObservableCollection<MailDirectoryItem> Children { get; set; }

        public MailDirectoryItem()
        {
            this.Children = new ObservableCollection<MailDirectoryItem>();
        }
    }

    public class CalendarMenuItem : MenuItemBase
    {
    }

    public class ContactsMenuItem : MenuItemBase
    {
        public ObservableCollection<Person> ContactsList { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public string IconSource { get; set; }
    }

}
