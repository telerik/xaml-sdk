using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GridViewSerialization
{
	/// <summary>
	/// Represents a customer
	/// </summary>
	public class Customer
	{
		private string customerID;
		private string companyName;
		private string country;
		private string city;
		private string contactName;
		private bool boolean;

		/// <summary>
		/// Gets or sets the customer ID.
		/// </summary>
		/// <value>The customer ID.</value>
		public string CustomerID
		{
			get
			{
				return customerID;
			}
			set
			{
				customerID = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the company.
		/// </summary>
		/// <value>The name of the company.</value>
		public string CompanyName
		{
			get
			{
				return companyName;
			}
			set
			{
				companyName = value;
			}
		}

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		/// <value>The country.</value>
		public string Country
		{
			get
			{
				return country;
			}
			set
			{
				country = value;
			}
		}

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		/// <value>The city.</value>
		public string City
		{
			get
			{
				return city;
			}
			set
			{
				city = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the contact.
		/// </summary>
		/// <value>The name of the contact.</value>
		public string ContactName
		{
			get
			{
				return contactName;
			}
			set
			{
				contactName = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Customer"/> is bool.
		/// </summary>
		/// <value><c>true</c> if bool; otherwise, <c>false</c>.</value>
		public bool Bool
		{
			get
			{
				return this.boolean;
			}
			set
			{
				this.boolean = value;
			}
		}
	}
}
