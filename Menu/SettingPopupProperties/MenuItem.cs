using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SettingPopupProperties
{
    public class MenuItem
    {
        public string Header { get; set; }
        public string IconURL { get; set; }

        public ObservableCollection<MenuItem> SubItems { get; set; }
    }
}
