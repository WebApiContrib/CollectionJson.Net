using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    public abstract class ExtensibleObject : DynamicObject
    {
        public ExtensibleObject()
        {
            Members = new Dictionary<string, object>();
            DynamicMembers = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
        }
        
        protected IDictionary<string, object> Members { get; private set; }
        protected IDictionary<string, object> DynamicMembers { get; private set; } 

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name;
            var found = DynamicMembers.TryGetValue(name, out result);
            
            if (result == null)
                return false;

            return found;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            DynamicMembers[binder.Name] = value;
            return true;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            foreach (var entry in DynamicMembers)
            {
                if (entry.Value != null)
                    yield return entry.Key;
            }
        }

        public T GetValue<T>(string key)
        {
            object val;
            var found = Members.TryGetValue(key, out val);

            if (found)
                return (T) val;

            return default(T);
        }

        public void SetValue(string key, object value)
        {
            Members[key] = value;
        }

        public dynamic Extensions()
        {
            return this;
        }
    }
}
