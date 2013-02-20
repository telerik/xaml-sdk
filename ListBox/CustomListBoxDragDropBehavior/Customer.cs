using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CustomListBoxDragDropBehavior
{
	public class Customer : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string _Name;
		private int _Id;

		/// <Summary>Gets or sets Name and notifies for changes</Summary>
		public string Name
		{
			get { return this._Name; }
			set
			{
				if (this._Name != value)
				{
					this._Name = value;
					this.OnPropertyChanged(() => this.Name);
				}
			}
		}

		/// <Summary>Gets or sets Id and notifies for changes</Summary>
		public int Id
		{
			get { return this._Id; }
			set
			{
				if (this._Id != value)
				{
					this._Id = value;
					this.OnPropertyChanged(() => this.Id);
				}
			}
		}

		public Customer Copy()
		{
			return new Customer
			{
				Name = this.Name,
				Id = this.Id
			};
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, args);
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			this.OnPropertyChanged(((MemberExpression)propertyExpression.Body).Member.Name);
		}
	}
}
