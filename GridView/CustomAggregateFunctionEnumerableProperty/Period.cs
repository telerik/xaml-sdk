using System;
using System.ComponentModel;
using System.Linq;

namespace CustomAggregateFunctionEnumerableProperty
{
    public class Period : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private HalfSeason halfSeason ;

        public HalfSeason HalfSeason
        {
            get { return this.halfSeason; }
            set 
            { 
                this.halfSeason = value;
                this.OnPropertyChanged("HalfSeason");
            }
        }
        
        private double numbersOfGoals;

        public double NumberOfGoals
        {
            get { return this.numbersOfGoals; }
            set 
            { 
                this.numbersOfGoals = value;
                this.OnPropertyChanged("NumberOfGoals");
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum HalfSeason
    {
        First,
        Second
    }
}
