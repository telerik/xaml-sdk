using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace DataValidation
{
    public class DataErrorViewModel : ViewModelBase, IDataErrorInfo
    {
        public DataErrorViewModel(bool isValidatingOnLoad, int cardId)
        {
            this.skipValidationOnLoad = isValidatingOnLoad;
            this.CardID = cardId;
        }

        private bool skipValidationOnLoad; 

        private double? cardID;        
        public double? CardID
        {
            get { return this.cardID; }
            set
            {
                if (this.cardID != value)
                {
                    this.cardID = value;
                    this.OnPropertyChanged("CardID");
                }
            }
        }

        public string Error
        {
            get { return this[string.Empty]; }
        }

        public string this[string propertyName]
        {
            get
            {
                string validationResult = null;
                if (this.skipValidationOnLoad == true)
                {
                    this.skipValidationOnLoad = false;
                    return validationResult;
                }
                switch (propertyName)
                {
                    case "CardID":
                        validationResult = Validate_CardID();
                        break;

                    default:
                        throw new Exception("Unknown Property being validated on Product.");
                }

                return validationResult;
            }
        }

        private string Validate_CardID()
        {
            string result = string.Empty;

            if (CardID == 0)
            {
                result = "Please Enter a valid Card ID in range [1-9999].";
            }

            if (CardID < 1 || CardID > 9999 || cardID == null)
            {
                result = "Only CardID Between 1 and 9999";
            }

            return result;
        }      
    }
}