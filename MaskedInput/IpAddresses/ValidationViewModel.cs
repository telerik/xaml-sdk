using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace IpAddresses
{
	public class ValidationViewModel : ViewModelBase
	{
		private string ipVal;
		public string IpValue
		{
			get { return this.ipVal; }
			set
			{
				if (this.ipVal != value)
				{
					this.Validate(this.ipVal, value, "IpValue", "Invalid IP Address !");		
				}
			}
		}
		private string submask;
		public string SubMask
		{
			get { return this.submask; }
			set
			{
				if (this.submask != value)
				{
					this.Validate(this.submask, value, "SubMask", "Invalid Subnet Mask !");		
				}
			}
		}

		private string gateway;
		public string GateWay
		{
			get { return this.gateway; }
			set
			{
				if (this.gateway != value)
				{
					this.Validate(this.gateway, value, "GateWay", "Invalid GateWay Address !");	
				}
			}
		}

		private string dns1;
		public string DNS1
		{
			get { return this.dns1; }
			set
			{
				if (this.dns1 != value)
				{
					this.Validate(this.dns1, value, "DNS1", "Invalid DNS Address !");	
				}
			}
		}

		private string dns2;
		public string DNS2
		{
			get { return this.dns2; }
			set
			{
				if (this.dns2 != value)
				{
					this.Validate(this.dns2, value, "DNS2", "Invalid DNS Address !");
				}
			}
		}	
		
		private void Validate(string field, string value, string propertyName, string errorMessage)
		{
			if (value.Length == 12)
			{
				IPAddress ipTestAddress;
				string dottedIP = value.Substring(0, 3) + "." + value.Substring(3, 3) + "." + value.Substring(6, 3) + "." + value.Substring(9, 3);
				if (!IPAddress.TryParse(dottedIP, out ipTestAddress))
				{
					MessageBox.Show(errorMessage, "Error !", MessageBoxButton.OK);
					throw new ValidationException(errorMessage);
				}
				else
				{
					field = value;
					this.OnPropertyChanged(propertyName);
				}
			}	
		}	
	}
}
