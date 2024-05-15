using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Controls;
using Telerik.Windows.Rendering;

namespace HolidayEvents
{
    public class NationalEventContainer : Control, IDataContainer, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private object _DataItem;

        public NationalEventContainer()
		{
            this.DefaultStyleKey = typeof(NationalEventContainer);
		}

		/// <Summary>Gets or sets DataItem and notifies for changes</Summary>
		public object DataItem
		{
			get { return this._DataItem; }
			set
			{
				if (this._DataItem != value)
				{
					this._DataItem = value;
					this.OnPropertyChanged(() => this.DataItem);
				}
			}
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, args);
			}
		}

		protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			this.OnPropertyChanged(((MemberExpression)propertyExpression.Body).Member.Name);
		}

		protected void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}
	}
}
