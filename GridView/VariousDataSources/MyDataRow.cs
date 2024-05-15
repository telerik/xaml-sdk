using System.Dynamic;
using System.Collections.Generic;
using System.ComponentModel;

namespace VariousDataSources
{
    public class MyDataRow : DynamicObject, INotifyPropertyChanged 
    {
        readonly IDictionary<string, object> data;

        public MyDataRow()
        {
            data = new Dictionary<string, object>();
        }

        public MyDataRow(IDictionary<string, object> source)
        {
            data = source;
        }
        
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return data.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this[binder.Name];

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this[binder.Name] = value;

            return true;
        }

        public object this[string columnName]
        {
            get
            {
                if (data.ContainsKey(columnName))
                {
                    return data[columnName];
                }

                return null;
            }
            set
            {
                if (!data.ContainsKey(columnName))
                {
                    data.Add(columnName, value);

                    OnPropertyChanged(columnName);
                }
                else
                {
                    if (data[columnName] != value)
                    {
                        data[columnName] = value;

                        OnPropertyChanged(columnName);
                    }
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
