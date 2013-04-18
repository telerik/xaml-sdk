using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ExpandItemsIntoView
{
    public class MyObject
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [Display(AutoGenerateField = false)]
        public ObservableCollection<MyObject> Items { get; set; }
    }
}
