using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
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
                this.ipVal = value;
                 string candidate = this.IpText == null ? this.ipVal.ToString() : this.IpText;
                this.Validate(this.ipVal, candidate, "IpValue", "Incorrect Ip Address");
                this.OnPropertyChanged("IpValue");
            }
        }

        private string ipText;
        public string IpText
        {
            get { return this.ipText; }
            set
            {
                if (this.ipText != value)
                {
                    this.ipText = value;
                    this.ParseIpString(value);
                    this.OnPropertyChanged("IpText");
                }
            }
        }

        private void ParseIpString(string ipText)
        {
            string[] parts = ipText.Split(new char[] { '.' });
            
            byte partA = 0, partB = 0, partC  = 0, partD = 0;

            if ((byte.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out partA) &&
            byte.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out partB) &&
            byte.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out partC) &&
            byte.TryParse(parts[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out partD)) == false)
            {
                throw new ValidationException("Incorrect format of Ip address!");
            };            
            
            this.IpValue = new IpAddressPresentation(partA, partB, partC, partD);            
        }    

        private void Validate(IpAddressPresentation field, string ipStringCandidate, string propertyName, string errorMessage)
        {
            IPAddress ipTestAddress;
            if (!IPAddress.TryParse(ipStringCandidate, out ipTestAddress))
            {
                MessageBox.Show(errorMessage, "Error !", MessageBoxButton.OK);
                throw new ValidationException(errorMessage);
            }
            else
            {
                string[] parts = ipTestAddress.ToString().Split(new char[] { '.' });
                field = new IpAddressPresentation(byte.Parse(parts[0]), byte.Parse(parts[1]), byte.Parse(parts[2]), byte.Parse(parts[3]));
               
                this.OnPropertyChanged(propertyName);
            }
        }
    }
}
