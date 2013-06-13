using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;
using Telerik.Windows.Controls;

namespace RadContextMenuAndRadGridViewMVVM
{
    [ContentProperty("ContextMenu")]
    public class RadContextMenuXamlHolder : INotifyPropertyChanged
    {
        private RadContextMenu contextMenu;
        public event PropertyChangedEventHandler PropertyChanged;

        public RadContextMenu ContextMenu
        {
            get
            {
                return this.contextMenu;
            }
            set
            {
                if (this.contextMenu != value)
                {
                    this.contextMenu = value;
                    this.RaisePropertyChanged("ContextMenu");
                }
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
