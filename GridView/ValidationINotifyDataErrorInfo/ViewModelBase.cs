using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Data;


namespace ValidateINotifyDataErrorInfo
{
#if !SILVERLIGHT
    public abstract class ViewModelBase : INotifyPropertyChanged, Telerik.Windows.Data.INotifyDataErrorInfo
#else
    public abstract class ViewModelBase : INotifyPropertyChanged, System.ComponentModel.INotifyDataErrorInfo
#endif
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnDataErrorsChanged(string propertyName)
        {
            if (this.ErrorsChanged != null)
            {
                this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        protected virtual void AddError(string propertyName, string errorMessage)
        {
            if (!this.errors.ContainsKey(propertyName))
            {
                this.errors.Add(propertyName, new List<String>());
            }

            if (!this.errors[propertyName].Contains(errorMessage))
            {
                this.errors[propertyName].Add(errorMessage);
                this.OnDataErrorsChanged(propertyName);
            }
        }

        protected virtual void RemoveError(string propertyName, string errorMessage)
        {
            if (this.errors.ContainsKey(propertyName))
            {
                if (this.errors[propertyName].Contains(errorMessage))
                {
                    this.errors[propertyName].Remove(errorMessage);
                    if (this.errors[propertyName].Count == 0)
                    {
                        this.errors.Remove(propertyName);
                    }

                    this.OnDataErrorsChanged(propertyName);
                }
            }
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !this.errors.ContainsKey(propertyName))
            {
                return null;
            }

            return this.errors[propertyName];
        }

        public bool HasErrors
        {
            get { return this.errors.Count > 0; }
        }

    }
}
