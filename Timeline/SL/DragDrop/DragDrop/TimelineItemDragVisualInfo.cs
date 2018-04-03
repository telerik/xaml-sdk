using System.Windows.Media;
using Telerik.Windows.Controls;

namespace DragDrop
{
    /// <summary>
    /// This class is the model of the drag visual element
    /// </summary>
    public class TimelineItemDragVisualInfo : ViewModelBase
    {
        private object groupKey;
        private int rowIndex;        
        
        public ImageSource ItemImageSource { get; set; }
        public double ItemImageWidth { get; set; }
        public double ItemImageHeight { get; set; }
        
        public object GroupKey
        {
            get { return groupKey; }
            set
            {
                if (this.groupKey != value)
                {
                    this.groupKey = value;
                    OnPropertyChanged("GroupKey");
                }                
            }
        }       

        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
            set
            {
                if (this.rowIndex != value)
                {
                    this.rowIndex = value;
                    OnPropertyChanged("RowIndex");
                }
            }
        }        
    }
}
