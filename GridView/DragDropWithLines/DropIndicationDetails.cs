using System;
using System.Linq;
using Telerik.Windows.Controls;

namespace DragDropWithLines
{
    public class DropIndicationDetails : ViewModelBase
    {
        private object currentDraggedItem;
        private DropPosition currentDropPosition;
        private object currentDraggedOverItem;

        public object CurrentDraggedOverItem
        {
            get
            {
                return currentDraggedOverItem;
            }
            set
            {
                if (this.currentDraggedOverItem != value)
                {
                    currentDraggedOverItem = value;
                    OnPropertyChanged("CurrentDraggedOverItem");
                }
            }
        }

        public int DropIndex { get; set; }

        public DropPosition CurrentDropPosition
        {
            get
            {
                return this.currentDropPosition;
            }
            set
            {
                if (this.currentDropPosition != value)
                {
                    this.currentDropPosition = value;
                    OnPropertyChanged("CurrentDropPosition");
                }
            }
        }

        public object CurrentDraggedItem
        {
            get
            {
                return this.currentDraggedItem;
            }
            set
            {
                if (this.currentDraggedItem != value)
                {
                    this.currentDraggedItem = value;
                    OnPropertyChanged("CurrentDraggedItem");
                }
            }
        }
    }
}
