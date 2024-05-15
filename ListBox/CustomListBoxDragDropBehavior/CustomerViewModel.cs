using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace CustomListBoxDragDropBehavior
{
	public class CustomerViewModel : ViewModelBase
	{
		private ObservableCollection<Customer> _Customers2;
		private ObservableCollection<Customer> _Customers1;

		public CustomerViewModel()
		{
			this.Customers1 = new ObservableCollection<Customer>()
			{
				new Customer { Id = 1, Name = "Customer 1" },
				new Customer { Id = 2, Name = "Customer 2" },
				new Customer { Id = 3, Name = "Customer 3" },
				new Customer { Id = 4, Name = "Customer 4" },
				new Customer { Id = 5, Name = "Customer 5" } 
			};
			this.Customers2 = new ObservableCollection<Customer>();
		}

		/// <Summary>Gets or sets Customers2 and notifies for changes</Summary>
		public ObservableCollection<Customer> Customers2
		{
			get { return this._Customers2; }
			set
			{
				if (this._Customers2 != value)
				{
					this._Customers2 = value;
					this.OnPropertyChanged(() => this.Customers2);
				}
			}
		}

		/// <Summary>Gets or sets Customers1 and notifies for changes</Summary>
		public ObservableCollection<Customer> Customers1
		{
			get { return this._Customers1; }
			set
			{
				if (this._Customers1 != value)
				{
					this._Customers1 = value;
					this.OnPropertyChanged(() => this.Customers1);
				}
			}
		}
	}
}
