using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Windows.Controls;

namespace INotifyDataErrorInfoSupport
{
    public abstract class NotifyingObject : ViewModelBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public event EventHandler<System.ComponentModel.DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnDataErrorsChanged(string propertyName)
        {
            if (this.ErrorsChanged != null)
            {
                this.ErrorsChanged(this, new System.ComponentModel.DataErrorsChangedEventArgs(propertyName));
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

        [Browsable(false)]
        public bool HasErrors
        {
            get { return this.errors.Count > 0; }
        }

    }
}
