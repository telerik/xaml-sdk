using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace CopyPasteFunctionality
{
	public class MyViewModel : ViewModelBase
	{
		Northwind northwind;
		public Northwind Northwind
		{
			get
			{
				if (northwind == null)
				{
					northwind = new Northwind();
				}

				return northwind;
			}
		}

		IEnumerable<Product> products;
		public IEnumerable<Product> Products
		{
			get
			{
				if (products == null)
				{
					products = Northwind.ProductsCollection;
				}

				return products;
			}
		}

		IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> modes;
		public IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> Modes
		{
			get
			{
				if (modes == null)
				{
					modes = Telerik.Windows.Data.EnumDataSource.FromType<System.Windows.Controls.SelectionMode>();
				}

				return modes;
			}
		}

		IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> units;
		public IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> Units
		{
			get
			{
				if (units == null)
				{
					units = Telerik.Windows.Data.EnumDataSource.FromType<Telerik.Windows.Controls.GridView.GridViewSelectionUnit>();
				}

				return units;
			}
		}

		private bool shouldCopySelectColumn;
		public bool ShouldCopySelectColumn
		{
			get
			{
				return this.shouldCopySelectColumn;
			}
			set
			{
				if (this.shouldCopySelectColumn != value)
				{
					this.shouldCopySelectColumn = value;
					this.OnPropertyChanged("ShouldCopySelectColumn");
				}
			}
		}		
	}
}
