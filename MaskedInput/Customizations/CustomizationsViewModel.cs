using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Telerik.Windows.Controls;

namespace MaskCustomizations
{
	public class CustomizationsViewModel : ViewModelBase
	{
		private decimal? bankAccount = null;
		private string fullName;
		private decimal? amount = null;

		public decimal? BankAccount
		{
			get { return bankAccount; }
			set
			{
				bankAccount = value;
				this.OnPropertyChanged("BankAccount");
			}
		}

		[Required(ErrorMessage = "Name is required.")]
		public string FullName
		{
			get { return fullName; }
			set
			{
				fullName = value;
				this.OnPropertyChanged("FullName");
			}
		}

		public decimal? Amount
		{
			get { return amount; }
			set
			{
				if (value == null)
				{
					throw new ValidationException("Amount cannot be null.");
				}
				amount = value;
				this.OnPropertyChanged("Amount");
			}
		}
	}
}