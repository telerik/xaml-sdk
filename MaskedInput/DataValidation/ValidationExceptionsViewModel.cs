using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace DataValidation
{
    public class ValidationExceptionsViewModel : ViewModelBase
    {
        private double? cardID;
        public double? CardID
        {
            get { return this.cardID; }
            set
            {
                if (this.cardID != value)
                {
                    this.cardID = value;
                    if (!IsCardIDValid())
                    {
                        throw new ValidationException("ID must be between 1 and 9999!");
                    }
                    this.OnPropertyChanged("CardID");
                }
            }
        }

        private double? temperature;

        [Range(-30d, 30d)]
        public double? Temperature
        {
            get { return this.temperature; }
            set
            {
                if (this.temperature != value)
                {
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Temperature" });
                    this.temperature = value;
                    this.OnPropertyChanged("Temperature");
                }
            }
        }

        private bool IsCardIDValid()
        {
            if (this.CardID < 1 || this.cardID > 9999 || this.CardID == null)
            {
                return false;
            }
            return true;
        }
    }
}
