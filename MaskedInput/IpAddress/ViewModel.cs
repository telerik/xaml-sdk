using System;
using Telerik.Windows.Controls;

namespace IpAddress
{
    public class ViewModel : ViewModelBase
    {
        private IpAddressPresentation ipVal;
        public IpAddressPresentation IpValue
        {
            get { return this.ipVal; }
            set
            {
                if (this.ipVal != value)
                {
                    this.ipVal = value;
                    if (!value.IsValid)
                    {
                        throw new ArgumentException("Incorrect Format of IP Address!");
                    }
                    this.OnPropertyChanged("IpValue");
                }
            }
        }         
    }
}
