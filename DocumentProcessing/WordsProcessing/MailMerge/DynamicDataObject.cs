using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace MailMerge
{
    public class DynamicDataObject : DynamicObject
    {
        private readonly Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public int Count
        {
            get
            {
                return this.dictionary.Count;
            }
        }

        public IEnumerable<String> GetColumnNames()
        {
            return this.dictionary.Keys;
        }

        public void Set(string propertyName, object value)
        {
            this.TrySetMember(new DynamicObjectSetMemberBinder(propertyName, false), value);
        }

        public object Get(string propertyName)
        {
            object result;
            
            this.TryGetMember(new DynamicObjectGetMemberBinder(propertyName, false), out result);

            return result;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string propertyName = binder.Name;
            return this.dictionary.TryGetValue(propertyName, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            string propertyName = binder.Name;
            this.dictionary[propertyName] = value;

            return true;
        }
    }
}
