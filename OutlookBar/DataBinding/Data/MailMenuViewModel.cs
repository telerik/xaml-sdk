using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace DataBinding.Data
{
    public class MailMenuViewModel : ViewModelBase
    {
        public ObservableCollection<MenuItemBase> MenuItems { get; set; }

        private MenuItemBase selectedItem;
        public MenuItemBase SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged("SelectedItem");
                }
            }
        }

        public MailMenuViewModel()
        {
            this.MenuItems = new ObservableCollection<MenuItemBase>();
            
            PopulateMenuItems();

            this.selectedItem = this.MenuItems[0];
        }

        private void PopulateMenuItems()
        {
            var mailMenuItem = new MailMenuItem() { Content = "Mails content", Header = "Mail", IconSource = "images/mailBig.png", IconSourceSmall = "images/mailSmall.png" };
            mailMenuItem.MailDirectories.Add(new MailDirectoryItem()
            {
                Header = "Personal Folders",
                IconSource = "images/1PersonalFolders.png",
                Children = new ObservableCollection<MailDirectoryItem>()
                {
                    new MailDirectoryItem { Header = "Deleted Items", IconSource = "images/2DeletedItems.png", },
                    new MailDirectoryItem { Header = "Drafts", IconSource = "images/3Drafts.png", },
                    new MailDirectoryItem()
                    { 
                        Header = "Inbox", IconSource = "images/4Inbox.png", 
                        Children = new ObservableCollection<MailDirectoryItem>()
                        {
                            new MailDirectoryItem() { Header = "Nancy Davolio", IconSource = "images/letter.png" },
                            new MailDirectoryItem() { Header = "Janer Leverling", IconSource = "images/letter.png" },
                            new MailDirectoryItem() { Header = "Robert King", IconSource = "images/letter.png" },
                        }
                    },
                    new MailDirectoryItem { Header = "Junk Emails", IconSource = "images/junk.png", },
                    new MailDirectoryItem { Header = "Outbox", IconSource = "images/outbox.png", },
                    new MailDirectoryItem { Header = "Sent Items", IconSource = "images/sent.png", }
                }
            });

            this.MenuItems.Add(mailMenuItem);

            var calendarMenuItem = new CalendarMenuItem() { Content = "Calendar content", Header = "Calendar", IconSource = "images/calendarBig.png", IconSourceSmall = "images/calendarSmall.png" };
            this.MenuItems.Add(calendarMenuItem);

            var contactsMenuItem = new ContactsMenuItem() { Content = "Contacts content", Header = "Contacts", IconSource = "images/contactsBig.png", IconSourceSmall = "images/contactsSmall.png" };
            contactsMenuItem.ContactsList = new ObservableCollection<Person>()
            {
                new Person() { Name = "John Smith", IconSource = "images/contact.png"},
                new Person() { Name = "James Bond", IconSource = "images/contact.png"},
                new Person() { Name = "Haris Pilton", IconSource = "images/contact.png"},
                new Person() { Name = "Kim LeBlank", IconSource = "images/contact.png"},
                new Person() { Name = "Rock Lee", IconSource = "images/contact.png"},
                new Person() { Name = "Jim Brown", IconSource = "images/contact.png"},
            };

            this.MenuItems.Add(contactsMenuItem);
        }
    }   
}
