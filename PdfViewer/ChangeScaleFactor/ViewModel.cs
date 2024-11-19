using System.ComponentModel;

namespace ChangeScaleFactor
{
    public class ViewModel : INotifyPropertyChanged
    {
        private double initialScaleFactor;

        public ViewModel(double initialScaleFactor)
        {
            this.InitialScaleFactor = initialScaleFactor;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double InitialScaleFactor
        {
            get
            {
                return this.initialScaleFactor;
            }
            set
            {
                if (this.initialScaleFactor != value)
                {
                    this.initialScaleFactor = value;
                    this.OnPropertyChanged("InitialScaleFactor");
                }
            }
        }
        
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
