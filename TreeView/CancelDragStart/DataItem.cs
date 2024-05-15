using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace CancelDragStart
{
    public class DataItem : ViewModelBase
    {
        private static Brush forbidDragBrush = new SolidColorBrush(GetColorFromHex("#46FF6347"));
        private static Brush normalBrush = new SolidColorBrush(GetColorFromHex("#4690EE90"));

        private bool canDrag;

        public string Header { get; set; }

        public ObservableCollection<DataItem> Children { get; set; }

        public bool CanDrag
        {
            get { return canDrag; }
            set
            {
                if (canDrag != value)
                {
                    canDrag = value;
                    this.OnPropertyChanged("CanDrag");
                    this.OnPropertyChanged("Brush");
                }
            }
        }

        public Brush Brush
        {
            get
            {
                return canDrag ? normalBrush : forbidDragBrush;
            }
        }

        public static Color GetColorFromHex(string myColor)
        {
           return Color.FromArgb(
                    Convert.ToByte(myColor.Substring(1, 2), 16),
                    Convert.ToByte(myColor.Substring(3, 2), 16),
                    Convert.ToByte(myColor.Substring(5, 2), 16),
                    Convert.ToByte(myColor.Substring(7, 2), 16));
        }
    }
}
