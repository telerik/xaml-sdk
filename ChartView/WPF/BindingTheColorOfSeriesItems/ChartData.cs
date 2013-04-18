using System.ComponentModel;
using System.Windows.Media;

namespace BindingTheColorOfSeriesItems
{
	public class ChartData : INotifyPropertyChanged
	{
		private int _category;
		public int Category
		{
			get { return this._category; }
			set { this._category = value; this.OnPropertyChanged("Category"); }
		}

		private double _value;
		public double Value
		{
			get { return this._value; }
			set { this._value = value; this.OnPropertyChanged("Value"); }
		}

		private Brush _color;
		public Brush Color
		{
			get { return this._color; }
			set { this._color = value; this.OnPropertyChanged("Color"); }
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
