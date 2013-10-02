using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomToolboxDragDrop_WPF.ViewModels
{
    public class MyGallery
    {
        public string Header { get; set; }
        public ObservableCollection<MyShape> Shapes { get; set; }
        public MyGallery()
        {
            this.Shapes = new ObservableCollection<MyShape>();
        }
    }

}
