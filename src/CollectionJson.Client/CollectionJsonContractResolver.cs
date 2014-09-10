using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace CollectionJson.Client
{
    public class CollectionJsonContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property.DeclaringType.Namespace == "CollectionJson")
            {
                property.ShouldSerialize =
                    instance =>
                    {
                        var val = property.ValueProvider.GetValue(instance);
                        var list = val as IList;
                        if (list != null)
                        {
                            return list.Count > 0;
                        }
                        return true;
                    };
            }
            return property;
        }

    }
}
