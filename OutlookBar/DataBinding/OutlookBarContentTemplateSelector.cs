using DataBinding.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace DataBinding
{
    public class OutlookBarContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MailTemplate { get; set; }
        public DataTemplate CalendarTemplate { get; set; }
        public DataTemplate ContactsTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is MailMenuItem)
            {
                return this.MailTemplate;
            }
            else if (item is CalendarMenuItem)
            {
                return this.CalendarTemplate;
            }
            else if (item is ContactsMenuItem)
            {
                return this.ContactsTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
