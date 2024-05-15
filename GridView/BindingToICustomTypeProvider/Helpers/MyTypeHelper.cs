using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BindingToICustomTypeProvider.Helpers
{
    public class MyTypeHelper<T>: ICustomTypeProvider, INotifyPropertyChanged
    {
        private MyType customType;
        private static readonly List<MyPropertyInfo> customProperties = new List<MyPropertyInfo>();
        private readonly Dictionary<string, object> customPropertyValues = new Dictionary<string, object>();

        public MyTypeHelper()
        {
            foreach (var property in GetCustomType().GetProperties())
            {
                customPropertyValues.Add(property.Name, null);
            }
        }

        public static void AddProperty(string name)
        {
            AddProperty(name, typeof(string));
        }

        public static void ClearProperties()
        {
            customProperties.Clear();
        }

        public static void RemoveProperty(string name)
        {
            var item = customProperties.FirstOrDefault(cp => cp.Name == name);
            if (item != null)
            {
                customProperties.Remove(item);
            }
        }

        public static void AddProperty(string name, Type propertyType)
        {
            if (!CheckIfNameExists(name))
            {
                customProperties.Add(new MyPropertyInfo(name, propertyType, typeof(T)));
            }
        }

        public static void AddProperty(string name, Type propertyType, List<Attribute> attributes)
        {
            if (!CheckIfNameExists(name))
            {
                customProperties.Add(new MyPropertyInfo(name, propertyType, attributes, typeof(T)));
            }
        }

        public void SetPropertyValue(string propertyName, object value)
        {
            MyPropertyInfo propertyInfo = customProperties.FirstOrDefault(prop => prop.Name == propertyName);
            if (propertyInfo == null || !customPropertyValues.ContainsKey(propertyName))
            {
                throw new Exception("There is no property with the name " + propertyName);
            }

            if (ValidateValueType(value, propertyInfo._type))
            {
                if (customPropertyValues[propertyName] != value)
                {
                    customPropertyValues[propertyName] = value;
                    RaisePropertyChanged(propertyName);
                }
            }
            else
            {
                throw new Exception("Value is of the wrong type or null for a non-nullable type.");
            }     
        }

        public object GetPropertyValue(string propertyName)
        {
            if (customPropertyValues.ContainsKey(propertyName))
            {
                return customPropertyValues[propertyName];
            }
            throw new Exception("There is no property " + propertyName); 
        }

        public TV GetPropertyValue<TV>(string propertyName)
        {
            return (TV) GetPropertyValue(propertyName);
        }

        public PropertyInfo[] GetProperties()
        {
            return GetCustomType().GetProperties();
        }

        public Type GetCustomType()
        {
            if (customType == null)
            {
                customType = new MyType(typeof(T), customProperties);
            }

            return customType;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool ValidateValueType(object value, Type type)
        {
            return value == null
                ? !type.IsValueType || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                : type.IsAssignableFrom(value.GetType());
        }

        private static bool CheckIfNameExists(string name)
        {
            if (customProperties.Any(p => 0 == string.Compare(p.Name, name, StringComparison.OrdinalIgnoreCase))
                || typeof(T).GetProperties().Any(p => 0 == string.Compare(p.Name, name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Property with this name already exists: " + name);
            }

            return false;
        }
    }
}
