using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Validation
{
    public class MyTestClass : IDataErrorInfo, INotifyPropertyChanged
    {

        private int intVar;
        private string requiredField;

        public int IntProp
        {
            get { return intVar; }
            set
            {
                intVar = value;
                this.OnPropertyChanged("IntProp");
            }
        }

        [Required(ErrorMessage = "This field is Required.")]
        public string RequiredField
        {
            get { return requiredField; }
            set
            {
                requiredField = value;
                ValidateProperty("RequiredField", value);
                this.OnPropertyChanged("RequiredField");
            }
        }

        private string stringVar;

        public string StringProp
        {
            get { return stringVar; }
            set
            {
                stringVar = value;
                this.OnPropertyChanged("StringProp");
            }
        }

        private DateTime dateTimeVar;

        public DateTime DateTimeProp
        {
            get { return dateTimeVar; }
            set
            {
                dateTimeVar = value;
                this.OnPropertyChanged("DateTimeProp");
            }
        }

        [Browsable(false)]
        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "IntProp")
                {
                    return this.IntProp < 100 && this.IntProp > 0 ? string.Empty : "Value should be in the range of (0, 100)";
                }
                if (columnName == "StringProp")
                {
                    return this.StringProp != null && Regex.IsMatch(this.StringProp, @"^[0-9]+[\p{L}]*") ? string.Empty : @"Value should math the regex: ^[0-9]+[\p{L}]*";
                }
                if (columnName == "DateTimeProp")
                {
                    return this.DateTimeProp.Year > 1900 ? string.Empty : "Date should be after 1/1/1900";
                }
                return string.Empty;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ValidateProperty(string propName, object value)
        {
            var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = propName }, result);

            if (result.Count > 0)
            {
                throw new ValidationException(result[0].ErrorMessage);
            }
        }
    }
}
