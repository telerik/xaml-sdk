using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Rendering;

namespace ProjectDeadline
{
	public class TimeLineDeadlineContainer : Control, IDataContainer, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private object _DataItem;

		public TimeLineDeadlineContainer()
		{
			this.DefaultStyleKey = typeof(TimeLineDeadlineContainer);
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
