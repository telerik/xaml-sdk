using System.ComponentModel;
using System.Windows.Media;
using Telerik.Windows.Controls.GanttView;

namespace HighlightedAndSelectableDependencies
{
    public class CustomDependency : Dependency, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Brush color;

        public Brush Color
        {
            get { return color; }
            set
            {
                color = value;
                this.NotifyPropertyChanged("Color");
            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                this.NotifyPropertyChanged("IsSelected");
            }
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
